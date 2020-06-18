using Newtonsoft.Json;
using SQLProcTester.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace SQLProcTester
{
    public static class SpRunner
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


        // Main method to execute stored procedure for query results
        // The Execute method looks for input properties that are already set at the SqlSpClient level, then applies the values from the input model.
        //       The values in the input model have precedence.
        // Returns SpExecResultObject that contains the run details and the rows returned
        //       Use the SpExecResult.IsEquivalent method to Assert that the actual SpExecResult is equal to the expected result.
        public static SpExecResult RunProcQuery(SpExecInput input)
        {
            Stopwatch stopWatch = new Stopwatch();
            SpExecResult result = new SpExecResult();
            try
            {
                // set local variables to prepare
                PrepRun(input);
                // use local variables to execute proc
                result = SpSqlClient.ExecProcQuery(input);
            }
            catch (Exception e)
            {
                string errMsg = DebugLogger.CreateErrorDetail("SqlSpClient.Execute", e.Message, e.InnerException?.Message);

                DebugLogger.LogError(errMsg);
                result.ResultText = errMsg;
            }

            return result;
        }


        public static SpExecResultNonQuery RunProcNonQuery(SpExecInputNonQuery input)
        {
            string errMSg = "";
            Stopwatch stopWatch = new Stopwatch();
            SqlExecResult sqlResult = new SqlExecResult();
            SpExecResultNonQuery actualResult = new SpExecResultNonQuery();


            //TODO change to TRANSACTION
            // Start connection using statement here, pass connection to methods
            try
            {
                PrepRun(input);

                using (SqlConnection con = new SqlConnection(input.ConnectionString))
                {
                    //TODO 
                    con.Open();
                    SqlCommand cmdProc = con.CreateCommand();

                    SqlTransaction transaction;

                    // Start a local transaction
                    transaction = con.BeginTransaction("SQLProcTesterTransaction");

                    // Assign command properties;
                    cmdProc.CommandType = CommandType.StoredProcedure;
                    cmdProc.Connection = con;
                    cmdProc.Transaction = transaction;
                    cmdProc.CommandTimeout = input.CommandTimeout ?? 0;


                    // The list of simple SqlParamInput models is set from the class Property or input model.
                    // The PrepRun method only assigns the sqlParamInput objects to the list.
                    // We still need to call the AddSqlParameters extension method
                    // If parameters are present, the SqlDataCommand.AddSqlParameters extension method will convert them to real Sql parameters and add them to the SqlDataCommand.
                    if (input.SqlParams != null && input.SqlParams.Count > 0)
                        cmdProc.AddSqlParameters(input.SqlParams);

                    // Run Stored Proc to fill base Result Object
                    // This is the main call to the Stored Procedure under test
                    stopWatch.Start();
                    actualResult = SpSqlClient.ExecProcNonQuery(cmdProc, input);


                    // Check base results for error before continuing
                    if (!(actualResult.ResultText=="Success"))
                    {
                        throw new Exception($"SpExecNonQueryResultText: {actualResult.ResultText}");
                    }

                    // If a post-inspect script is specified, run it - save error messages to the PostInspectResultText field
                    if (!String.IsNullOrEmpty(input.PostInspectSql))
                    {
                        string err;
                        // Check if connection was left open by an error in the previous call to the stored procedure under test
                        // Open the connection again if necessary
                        if (con.State != ConnectionState.Open)
                            con.Open();


                        //Run the post-inspect SQL to validate the database state after running the procedure under test
                        //  Rows returned from this query will be added to the DbRows List.
                        sqlResult = SpSqlClient.ExecSqlQuery(transaction, input.PostInspectSql);

                        // If there is an error message
                        if (!String.IsNullOrEmpty(sqlResult.ErrorMessage))
                        {
                            err = DebugLogger.CreateErrorDetail("SqlSpClient.ExecProcNonQuery.PostInspectSql", sqlResult.ErrorMessage);
                            DebugLogger.LogError(err);
                            actualResult.PostInspectResultText = err;
                        }
                        else
                        {
                            actualResult.DbRows = sqlResult.DbRows;
                            actualResult.PostInspectResultText = "Success";
                        }

                    }
                    // Try to rollback the Transaction if input.Rollback is true
                  
                    if (input.Rollback)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception e2)
                        {
                            // This catch block will handle rollback failure
                            Debug.WriteLine("Rollback Exception Type: {0}", e2.GetType());
                            Debug.WriteLine("  Message: {0}", e2.Message);
                        }
                    }
                    // else try to commit the transaction
                    else
                    {
                        try
                        {

                            transaction.Commit();

                        }
                        catch (Exception e2)
                        {
                            // This catch block will handle rollback failure
                            string err = $"Commit Exception Type:{e2.GetType()} Message: {e2.Message}";
                            Debug.WriteLine(err);
                            actualResult.ResultText = err;
                        }
                    }

                    if (con.State != ConnectionState.Closed)
                        con.Close();

                }

            }
            catch (Exception e)
            {
                string errMsg = DebugLogger.CreateErrorDetail("SqlSpClient.ExecuteNonQuery", e.Message, e.InnerException?.Message);

                DebugLogger.LogError(errMsg);
                actualResult.ResultText = errMsg;
            }
            //Add duration
            if (stopWatch != null)
            {
                stopWatch.Stop();
                actualResult.Duration = (stopWatch.ElapsedMilliseconds);
            }




            //Capture json output if needed to create test case "expected" records.
            string jsonString = JsonConvert.SerializeObject(actualResult, Formatting.Indented);

            return actualResult;

        }

        private static void AddSqlParameters(this SqlCommand cmd, IEnumerable<SqlParamInput> paramsIn)
        {
            if (paramsIn == null)
            {
                paramsIn = new List<SqlParamInput>();
            }

            foreach (var input in paramsIn)
            {
                string type = input.Type.ToUpper();
                string name = input.Name.Replace("@", "");
                name = "@" + input.Name;
                SqlParameter sqlParam = new SqlParameter(name, null);
                try
                {
                    switch (type)
                    {
                        case "DATETIME":
                        case "DATETIME2":
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

                }
                catch (Exception e)
                {
                    throw new Exception($"Parameter Exception - '{name}' : {e.Message}");
                }
                cmd.Parameters.Add(sqlParam);
            }
            // Add the ReturnValue Parameter last
            var param = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(param);

        }


        private static void PrepRun(SpExecInput input)
        {
            // override input values with Class properties if input values are empty
            // if input.connection string is null, use the to the class property instead         input.ConnectionString = input.ConnectionString ?? ConnectionString;
            // if there is still no connection string, throw error

            input.ConnectionString = input.ConnectionString ?? ConnectionString;
            if (String.IsNullOrEmpty(input.ConnectionString))
            {
                throw new ArgumentNullException("ConnectionString", $"Set the Connection String Property of the class or include the value in the test input.");
            }

            input.SpName = input.SpName ?? SpName;
            if (String.IsNullOrEmpty(input.SpName))
            {
                throw new ArgumentNullException("SpName", $"Set the SpName Property of the class or include the value in the test input.");
            }

            // if the input command timeout is greater than 0, use it. Otherwise use class property (default 0)
            input.CommandTimeout = input.CommandTimeout ?? CommandTimeout;

            // If SpExecInpute.ParamInputs is not null, it will override the Class Property for SqlParamInputs - even when emtpy. 
            input.SqlParams = input.SqlParams ?? SqlParamInputs;


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

    }

}

