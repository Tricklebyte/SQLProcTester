using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Newtonsoft.Json;
using SQLProcTester.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SQLProcTester
{
    public static class SpSqlClient
    {


        // Main method to execute stored procedure
        // The Execute method looks for input properties that are already set at the SqlSpClient level, then applies the values from the input model.
        //       The values in the input model have precedence.
        // Returns SpExecResultObject that contains the run details and the rows returned
        //       Use the SpExecResult.IsEquivalent method to Assert that the actual SpExecResult is equal to the expected result.
        public static SpExecResult ExecProcQuery(SpExecInput input)
        {
            Stopwatch stopWatch = new Stopwatch();
            SpExecResult result = new SpExecResult();
            try
            {
                // set local variables to prepare
                // PrepRun(input);
                // use local variables to execute proc
                using (SqlConnection con = new SqlConnection(input.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(input.SpName, con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = input.CommandTimeout ?? 0;

                    // The list of simple SqlParamInput models is set from the class Property or input model.
                    // If parameters are present, the SqlDataCommand.AddSqlParameters extension method will convert them to real Sql parameters and add them to the SqlDataCommand.
                    if (input.SqlParams != null && input.SqlParams.Count() > 0)
                        cmd.AddSqlParameters(input.SqlParams);

                    //Open the connection
                    con.Open();

                    //Start run timer to record elapsed time
                    stopWatch.Start();


                    SqlDataReader rdr = cmd.ExecuteReader();
                    result.DbRows = rdr.GetDbRows();


                    // TODO CHECK IF RET PARAM EXISTS
                    // RETURN VALUE
                    result.ReturnValue = (int)cmd.Parameters["@ReturnValue"].Value;

                    result.ResultText = "Success";
                }
            }
            catch (Exception e)
            {
                string errMsg = DebugLogger.CreateErrorDetail("SqlSpClient.Execute", e.Message, e.InnerException?.Message);
                result.ResultText = errMsg;
                DebugLogger.LogError(errMsg);
            }

            //Add duration
            if (stopWatch != null)
            {
                stopWatch.Stop();
                result.Duration = (stopWatch.ElapsedMilliseconds);
            }

            //Capture json output if needed to create test case "expected" records.
            string jsonString = JsonConvert.SerializeObject(result, Formatting.Indented);

            return result;

        }

        public static SpExecResultNonQuery ExecProcNonQuery(SqlCommand cmd, SpExecInputNonQuery input)
        {
            Stopwatch stopWatch = new Stopwatch();
            SpExecResultNonQuery result = new SpExecResultNonQuery();
            SqlTransaction transaction;
            SqlExecResult postTestResult;

            //Declare the reader to gather the post run results
            SqlDataReader rdr;
            

            //Start run timer to record elapsed time
            stopWatch.Start();
            try
            {
                result.ReturnValue = cmd.ExecuteNonQuery();
                //TODO add check for return value??
                stopWatch.Stop();
                result.ResultText = "Success";
                
            
            
            
            
            }
            catch (Exception e)
            {
                string errMsg = DebugLogger.CreateErrorDetail("SqlSpClient.Execute", e.Message, e.InnerException?.Message);
                DebugLogger.LogError(errMsg);
                result.ResultText = errMsg;
            }

            //Add duration
            if (stopWatch != null)
            {
                result.Duration = (stopWatch.ElapsedMilliseconds);
            }

            //Capture json output if needed to create test case "expected" records.
            string jsonString = JsonConvert.SerializeObject(result, Formatting.Indented);

            return result;

        }


        // TODO can change to just Command, the transaction can be accessed through the command for rollback, or just use the command without the transaction for commit
        public static SqlExecResult ExecSqlQuery(SqlCommand cmd, string sqlText)
        {
            string errMsg;
            SqlExecResult result = new SqlExecResult();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlText;
            try
            {
                SqlDataReader rdr = cmd.ExecuteReader();
                result.DbRows = rdr.GetDbRows();
            }
            catch (Exception e)
            {
                errMsg = DebugLogger.CreateErrorDetail("SqlSpClient.Execute", e.Message, e.InnerException?.Message);
                DebugLogger.LogError(errMsg);
                result.ErrorMessage = errMsg;
            }


            //Capture json output if needed to create test case "expected" records.
            string jsonString = JsonConvert.SerializeObject(result, Formatting.Indented);

            return result;

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
