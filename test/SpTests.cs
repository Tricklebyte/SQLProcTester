using Newtonsoft.Json;
using System;
using Xunit;
using SQLProcTester;
using System.Collections.Generic;
using SQLProcTester.Models;
using System.IO;
using Xunit.Sdk;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SQLProcTester.Test
{/// <summary>
///  This tests the sample stored procedures provided in the SqlProcTest.sql file 
///  Each test uses different combinations of parameter types
/// </summary>
    public class SpTests
    {
        //  NOTE:   The connection string, and all other input properties, may be set at the  SpSqlClient class level or included in the test input. 
        //          When it is set in both places, the input value takes precedence.
        //          Here in this example, the Connection String is set in the Constructor of SpSqlClient and all other input values are specified via the input model.
        public SpTests()
        {
            // Same connection string for all tests, overidden from input model
            SpRunner.ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=SqlProcTest;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        // The good tests are for different parameter data types  (nvarchar, varchar, bit, int, datetime)
        //  Test Case 1 is a simple example with minimum input values specified in the test case
        //  Test Case 2 uses all settings in the input
        [Theory]
        [InlineData("spGetCurrentByName", "1")] // Param Types:nvarchar 
        [InlineData("spGetCurrentByName", "2")] // Param Types:nvarchar 
        [InlineData("spGetByAll", "1")]  //Param Types: nvarchar, varchar, bit, int, datetime, datetime2  
        [InlineData("spGetByAll", "2")]  
        [InlineData("spGetById", "1")]  //Parameter Type int
        [InlineData("spGetById", "2")]  
        [InlineData("spGetByNameAndDate", "1")]  //Parameter Types: nvarchar, datetime2 
        [InlineData("spGetByNameAndDate", "2")]
        [InlineData("spGetByPosition", "1")]  //Parameter Types: varchar
        [InlineData("spGetByPosition", "2")]
        [InlineData("spGetByTypeAndDate", "1")]  //Parameter Types:  bit,  datetime2 
        [InlineData("spGetByTypeAndDate", "2")]
        [InlineData("spGetByTypeAndMinDateOfBirth", "1")]  //Parameter Types:  bit, datetime
        [InlineData("spGetByTypeAndMinDateOfBirth", "2")]
     //ToDo move to non query   [InlineData("NonQuery\\spInsert","1")]
        public void SpTestRunner_RunProcQueryGood(string procedure, string testCase)
        {
            //ARRANGE
            string basePath = "TestCases\\ExecProcQuery\\Good";
            
            // Create the input model
            var input = JsonConvert.DeserializeObject<SpExecInput>(File.ReadAllText($"{basePath}\\{procedure}\\input{testCase}.json"));
           
            // Create the expected model
            var expected = JsonConvert.DeserializeObject<SpExecResult>(File.ReadAllText($"{basePath}\\{procedure}\\expected{testCase}.json"));

            ////ACT
            SpExecResult actual = SpRunner.RunProcQuery(input);

            ////ASSERT
            /// SpExecOutput.IsEquivalent method performs deep compare and generates detailed error messages to ResultText property and the Console Log
            /// NOTE!!!  
            /// Comparison for SpExecResult.Duration passes when expected Duration is greater than 0 and actual.Duration IS LESS THAN OR EQUAL TO expected.Duration
            /// To disable this check set expected.Duration to 0
            /// 
            Assert.True(actual.IsEquivalent(expected));
        }

        [Theory]
        [InlineData("ConnFail", "1")] // Connection Fail = Bad Sql Instance Name
        [InlineData("ConnFail", "2")] // Connection Fail = Bad Database Name
        [InlineData("CommandTimeout", "1")] // Procedure Timed Out
        [InlineData("CommandTimeout", "2")] // Procedure Timed Out
        [InlineData("InputFail\\ConnString", "1")] //  missing connection string
        [InlineData("InputFail\\SpName", "1")] //  missing stored Procedure name
        [InlineData("InputFail\\ParameterType", "1")] //  Invalid Parameter Type
        [InlineData("InputFail\\ParameterValue", "1")] //  Invalid Parameter Value
        public void SpTestRunner_RunProcQueryFail(string testPath, string testCase)
        {
            // ARRANGE
            string basePath = "TestCases\\ExecProcQuery\\Fail";
            // Save the Connection String and Stored Procedure name in case they are set at the class level
            string savedConn = SpRunner.ConnectionString;
            string savedSPName = SpRunner.SpName;

            // Temporarily clear the connection string and spName from the class property so the input files can simulate missing values
            SpRunner.ConnectionString = "";
            SpRunner.SpName = "";


            // Prepare input and expected
            var input = JsonConvert.DeserializeObject<SpExecInput>(File.ReadAllText($"{basePath}\\{testPath}\\input{testCase}.json"));
            var expected = JsonConvert.DeserializeObject<SpExecResult>(File.ReadAllText($"{basePath}\\{testPath}\\expected{testCase}.json"));

            ////ACT
            SpExecResult actual = SpRunner.RunProcQuery(input);

            // restore class level settings for subsequent tests in this same run. Do this before Assert in case an error is thrown during Assert.
            SpRunner.ConnectionString = savedConn;
            SpRunner.SpName = savedSPName;

            // ASSERT
            Assert.True(actual.ResultText == expected.ResultText);

        }


        [Theory]
        [InlineData("spCreate", "1")]
        [InlineData("spInsert", "1")]
        [InlineData("spDeleteById", "1")]
        public void SpTestRunner_RunProcNonQueryGood(string testPath, string testCase)
        {
            //ARRANGE
            string basePath = "TestCases\\ExecProcNonQuery\\Good";

            // Create the input model
            var input = JsonConvert.DeserializeObject<SpExecInputNonQuery>(File.ReadAllText($"{basePath}\\{testPath}\\input{testCase}.json"));


            // Create the expected model
            var expected = JsonConvert.DeserializeObject<SpExecResultNonQuery>(File.ReadAllText($"{basePath}\\{testPath}\\expected{testCase}.json"));


            ////ACT
            SpExecResultNonQuery actual = SpRunner.RunProcNonQuery(input);
            //TODO Add json trap

            ////ASSERT
            /// SpExecOutput.IsEquivalent method performs deep compare and generates detailed error messages to ResultText property and the Console Log
            /// NOTE!!!  
            /// Comparison for SpExecResult.Duration passes when expected Duration is greater than 0 and actual.Duration IS LESS THAN OR EQUAL TO expected.Duration
            /// To disable this check set expected.Duration to 0
            /// 
            Assert.True(actual.IsEquivalent(expected));
        }

        //TODO ExecProcNonQueryFail





        [Theory]
        [InlineData("Good", "1")]
        [InlineData("Fail", "1")]   //Incorrect table name
        public void ExecSqlQuery(string testPath, string testCase)
        {
            SqlExecResult actual;
            //ARRANGE
            string basePath = "TestCases\\ExecSqlQuery";

            // Create the input model
            var input = JsonConvert.DeserializeObject<SqlExecInput>(File.ReadAllText($"{basePath}\\{testPath}\\input{testCase}.json"));

            // Create the expected model
            var expected = JsonConvert.DeserializeObject<SqlExecResult>(File.ReadAllText($"{basePath}\\{testPath}\\expected{testCase}.json"));

            using (SqlConnection con = new SqlConnection(input.ConnectionString))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                SqlTransaction transaction;

                ////ACT
                transaction = con.BeginTransaction("PostInspectQueryTransaction");
                 actual = SpSqlClient.ExecSqlQuery(transaction, input.SqlText);
            }

            ////ASSERT
            /// SpExecOutput.IsEquivalent method performs deep compare and generates detailed error messages to ResultText property and the Console Log
            /// NOTE!!!  
            /// Comparison for SpExecResult.Duration passes when expected Duration is greater than 0 and actual.Duration IS LESS THAN OR EQUAL TO expected.Duration
            /// To disable this check set expected.Duration to 0
            /// 
            Assert.True(actual.IsEquivalent(expected));
        }


    }
}
