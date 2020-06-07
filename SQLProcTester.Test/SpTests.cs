using Newtonsoft.Json;
using System;
using Xunit;
using SQLProcTester;
using System.Collections.Generic;
using SQLProcTester.Models;
using System.IO;
using Xunit.Sdk;

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
            SqlSpClient.ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=SqlProcTest;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        // The good tests are for different parameter data types  (nvarchar, varchar, bit, int, datetime)
        [Theory]
        //TODO Add cases for null cmdto and nonQuery as well as 0
        [InlineData("Query\\spGetCurrentByName", "1")] //Parameter Type nvarchar
        [InlineData("Query\\spGetCurrentByName", "2")]
        [InlineData("Query\\spGetByAll", "1")]  //Parameter Types: nvarchar, varchar, bit, int, datetime, datetime2 
        [InlineData("Query\\spGetByAll", "2")]
        [InlineData("Query\\spGetById", "1")]  //Parameter Type int
        [InlineData("Query\\spGetById", "2")]
        [InlineData("Query\\spGetByNameAndDate", "1")]  //Parameter Types: nvarchar, varchar, bit, int, datetime, datetime2 
        [InlineData("Query\\spGetByNameAndDate", "2")]
        [InlineData("Query\\spGetByPosition", "1")]  //Parameter Types: varchar
        [InlineData("Query\\spGetByPosition", "2")]
        [InlineData("Query\\spGetByTypeAndDate", "1")]  //Parameter Types: nvarchar, varchar, bit, int, datetime, datetime2 
        [InlineData("Query\\spGetByTypeAndDate", "2")]
        [InlineData("Query\\spGetByTypeAndMinDateOfBirth", "1")]  //Parameter Types: nvarchar, varchar, bit, int, datetime, datetime2 
        [InlineData("Query\\spGetByTypeAndMinDateOfBirth", "2")]
        public void ExecuteGood(string procedure, string testCase)
        {
            

            //ARRANGE
            string basePath = "TestCases\\Execute\\Good";
            var input = JsonConvert.DeserializeObject<SpExecInput>(File.ReadAllText($"{basePath}\\{procedure}\\input{testCase}.json"));

            var expected = JsonConvert.DeserializeObject<SpExecResult>(File.ReadAllText($"{basePath}\\{procedure}\\expected{testCase}.json"));

            ////ACT
            SpExecResult actual = SqlSpClient.Execute(input);

            ////ASSERT
            /// SpExecOutput.IsEquivalent method performs deep compare and generates detailed error messages to ResultText property and the Debug Log
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
   
        public void ExecuteFail(string testPath, string testCase)
        {
            // ARRANGE
            string basePath = "TestCases\\Execute\\Fail";
            // Save the Connection String and Stored Procedure name in case they are set at the class level
            string savedConn = SqlSpClient.ConnectionString;
            string savedSPName = SqlSpClient.SpName;

            // Temporarily clear the connection string and spName from the class property so the input files can simulate missing values
            SqlSpClient.ConnectionString = "";
            SqlSpClient.SpName = "";


            // Prepare input and expected
            var input = JsonConvert.DeserializeObject<SpExecInput>(File.ReadAllText($"{basePath}\\{testPath}\\input{testCase}.json"));
            var expected = JsonConvert.DeserializeObject<SpExecResult>(File.ReadAllText($"{basePath}\\{testPath}\\expected{testCase}.json"));

            ////ACT
            SpExecResult actual = SqlSpClient.Execute(input);

            // restore class level settings for subsequent tests in this same run
            SqlSpClient.ConnectionString = savedConn;
            SqlSpClient.SpName = savedSPName;

            // ASSERT
            Assert.True(actual.ResultText == expected.ResultText);

        }

    }
}
