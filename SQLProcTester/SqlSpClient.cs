using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SQLProcTester.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SQLProcTester
{
    public static class SqlSpClient
    {
        // Local variables are passed to the the private methods to execute the stored procedure
        // These local variables will be set to either: 
        // a.) value present in the input object
        // b.) value assigned to class properties if input file is not present.
        // In the absence of all parameter values, the stored procedure will be called with no parameters    
        private static string runConnection;
        private static int runCommandTimoeout;
        private static string runProcedureName;
        private static List<SqlParamInput> runParams;
        private static bool runNonQuery;


        
        // When Non-Query is true, the SqlDataCommand.ExecuteNonQuery method will be used instead of the SqlDataReader
        //                          and SpExecResult.RowsAffected will populated.
        public static bool NonQuery
        {
            get => runNonQuery; set => runNonQuery = value;
        }

        // Stored Procedure Name
        public static string SpName
        {
            get => runProcedureName; set => runProcedureName = value;
        }

        // SqlDataCommand.CommandTimeout
        public static int CommandTimeout
        {
            get => runCommandTimoeout; set => runCommandTimoeout = value;
        }


        public static string ConnectionString { get; set; }

        // simple SqlParamInput models are use for input.
        // they will be converted to actual SqlCommandParameters in PrepRun method
        public static List<SqlParamInput> SqlParamInputs
        {
            get => runParams; set => runParams = value;
        }


        // Main method to execute stored procedure
        // The Execute method looks for input properties that are already set at the SqlSpClient level, then applies the values from the input model.
        //       The values in the input model have precedence.
        // Returns SpExecResultObject that contains the run details and the rows returned
        //       Use the SpExecResult.IsEquivalent method to Assert that the actual SpExecResult is equal to the expected result.
        public static SpExecResult Execute(SpExecInput input)
        {
            Stopwatch stopWatch = new Stopwatch();
            SpExecResult result = new SpExecResult();
            try
            {
                // set local variables to prepare
                PrepRun(input);
                // use local variables to execute proc
                using (SqlConnection con = new SqlConnection(runConnection))
                {
                    SqlCommand cmd = new SqlCommand(runProcedureName, con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = runCommandTimoeout;

                    // The list of simple SqlParamInput models is set from the class Property or input model.
                    // If parameters are present, the SqlDataCommand.AddSqlParameters extension method will convert them to real Sql parameters and add them to the SqlDataCommand.
                    if (runParams != null && runParams.Count() > 0)
                        cmd.AddSqlParameters(runParams);

                    //Open the connection
                    con.Open();

                    //Start run timer to record elapsed time
                    stopWatch.Start();

                    if (runNonQuery)
                    {
                        result.RowsAffected = cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlDataReader rdr = cmd.ExecuteReader();
                        result.DbRows = rdr.GetDbRows();

                    }

                    // TODO CHECK IF RET PARAM EXISTS
                    // RETURN VALUE
                    result.ReturnValue = (int)cmd.Parameters["@ReturnValue"].Value;
                    // TODO CHECK IF CONNECTION AUTO-CLOSES
                  //  con.Close();

                }
            }
            catch (Exception e)
            {
                result.ResultText = $"Error: \n{e.Message} \n\nInner Exception:\n{e.InnerException}";
            }
            //Add duration
            if (stopWatch != null)
            {
                stopWatch.Stop();
                result.Duration = (stopWatch.ElapsedMilliseconds);
            }
            //           Capture json output if needed to create test case "expected" records.
                    //Alternately, you could return just the json string and perform Asserts using JSON
                    //but you wouldn't get the detailed error logging from the IsEquivalent methods
             string jsonString = JsonConvert.SerializeObject(result, Formatting.Indented);
                 // Save jsonString to expectedxxx.json
                   // Save JsonString to database test case expected results
            return result;
        //    return jsonString;
        }

        private static void AddSqlParameters(this SqlCommand cmd, IEnumerable<SqlParamInput> paramsIn)
        {
            foreach (var input in paramsIn)
            {
                string type = input.Type.ToUpper();
                string name = input.Name.Replace("@", "");
                name = "@" + input.Name;
                SqlParameter sqlParam = new SqlParameter(name, null);
                switch (type)
                {
                    case "DATETIME":
                    case "DATETIME2":
                        //convert value from String to DT find in emma unit tests, or config core inline date string theory
                        sqlParam.SqlDbType = SqlDbType.DateTime;
                        sqlParam.Value = Convert.ToDateTime(input.Value);
                        break;
                    case "NVARCHAR":
                        sqlParam = new SqlParameter(name, System.Data.SqlDbType.NVarChar)
                        { Value = input.Value.ToString() };
                        break;
                    case "VARCHAR":
                        sqlParam = new SqlParameter(name, System.Data.SqlDbType.VarChar)
                        { Value = input.Value.ToString() };
                        break;
                    case "INT":
                        sqlParam = new SqlParameter(name, System.Data.SqlDbType.Int)
                        { Value = Convert.ToInt32(input.Value) };
                        break;
                    case "BIT":
                        sqlParam = new SqlParameter(name, System.Data.SqlDbType.Bit)
                        { Value = Convert.ToInt32(input.Value) };
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("SqlParameter.Type", $"Invalid type specified for parameter: {name}");
                }
                cmd.Parameters.Add(sqlParam);
            }
            // Add the ReturnValue Parameter last
            var param = new SqlParameter("@returnValue", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(param);

        }


        private static void PrepRun(SpExecInput input)
        {
            //  Assign inputs to local variables if present.
            //   after settings and inputs have been checked, local input variables will be ready for final validation

            // set the local input variable to the file value if supplied or class property if not supplied with input.
            runConnection = input.ConnectionString ?? ConnectionString;
            // if there is still no connection string, throw error
            if (String.IsNullOrEmpty(runConnection))
            {
                throw new ArgumentNullException("ConnectionString", $"Set the Connection String Property of the class or include the value in the test input.");
            }

            runProcedureName = input.SpName ?? SpName;
            if (String.IsNullOrEmpty(runProcedureName))
            {
                throw new ArgumentNullException("SpName", $"Set the SpName Property of the class or include the value in the test input.");
            }

            // if the input command timeout is greater than 0, use it. Otherwise use class property (default 0)
            runCommandTimoeout = input.CommandTimeout ?? CommandTimeout;

            // If SpExecInpute.ParamInputs is not null, it will override the Class Property for SqlParamInputs - even when emtpy. 
            runParams = input.SqlParams ?? SqlParamInputs;


            runNonQuery = input.NonQuery ?? NonQuery;

        }

        // Populates result rows (called from Execute when NonQuery is false)
        private static List<DbRow> GetDbRows(this SqlDataReader rdr)
        {
            List<DbRow> dbRows = new List<DbRow>();
            while (rdr.Read())
            {
                var row = new DbRow();
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    string colName = rdr.GetName(i).ToString();
                    string key = colName;
                    string val = rdr[i].ToString();
                    var colVal = new KeyValuePair<string, string>(key, val);
                    row.DbFields.Add(colVal);
                }
                dbRows.Add(row);
            }
            rdr.Close();
            return dbRows;
        }

        // List<DbRow> Comparator
        public static bool IsListEquivalent(this List<DbRow> actualList, List<DbRow> expectedList)
        {
            bool retVal;
            // check if counts are equal - 
            bool chkVal = actualList.Count() == expectedList.Count();


            if (!chkVal)
            {
                Debug.WriteLine($"DbRow count not equal. Actual count: {actualList.Count()}, Expected: {expectedList.Count()}");
                retVal = false;
            }
            else
            {
                retVal = true;

                for (int i = 0; i < expectedList.Count(); i++)
                {
                    if (!actualList[i].IsEquivalent(expectedList[i]))
                    { 
                        Debug.WriteLine($"Actual DbRow at index '{i.ToString()}' not equivalent to Expected");
                    retVal = false;
                    }
                }
            }
            return retVal;

        }



    }
}
