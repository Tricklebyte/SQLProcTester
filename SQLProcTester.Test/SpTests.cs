using Newtonsoft.Json;
using System;
using Xunit;
using SQLProcTester;
using System.Collections.Generic;
using SQLProcTester.Models;
using System.IO;


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


        [Theory]

        [InlineData("TestCases\\spGetCurrentByName\\Good", "1")] //Parameter Type nvarchar
        [InlineData("TestCases\\spGetCurrentByName\\Good", "2")]

        [InlineData("TestCases\\spGetHistoryByAll\\Good", "1")]  //Parameter Types: nvarchar, varchar, bit, int, datetime, datetime2 
        [InlineData("TestCases\\spGetHistoryByAll\\Good", "2")]

        [InlineData("TestCases\\spGetHistoryById\\Good", "1")]  //Parameter Type int
        [InlineData("TestCases\\spGetHistoryById\\Good", "2")]

        [InlineData("TestCases\\spGetHistoryByNameAndDate\\Good", "1")]  //Parameter Types: nvarchar, varchar, bit, int, datetime, datetime2 
        [InlineData("TestCases\\spGetHistoryByNameAndDate\\Good", "2")]

        [InlineData("TestCases\\spGetHistoryByPosition\\Good", "1")]  //Parameter Types: varchar
        [InlineData("TestCases\\spGetHistoryByPosition\\Good", "2")]

        [InlineData("TestCases\\spGetHistoryByTypeAndDate\\Good", "1")]  //Parameter Types: nvarchar, varchar, bit, int, datetime, datetime2 
        [InlineData("TestCases\\spGetHistoryByTypeAndDate\\Good", "2")]

        [InlineData("TestCases\\spGetHistoryByTypeAndMinDOB\\Good", "1")]  //Parameter Types: nvarchar, varchar, bit, int, datetime, datetime2 
        [InlineData("TestCases\\spGetHistoryByTypeAndMinDOB\\Good", "2")]

        public void ExecuteQueryGood(string basePath, string testCase)
        {
            //ARRANGE
   
            var input = JsonConvert.DeserializeObject<SpExecInput>(File.ReadAllText($"{basePath}\\input{testCase}.json"));
           
            var expected = JsonConvert.DeserializeObject<SpExecResult>(File.ReadAllText($"{basePath}\\expected{testCase}.json"));

            ////ACT
            SpExecResult actual = SqlSpClient.Execute(input);

            ////ASSERT
            /// SpExecOutput.IsEquivalent method performs deep compare and generates detailed error messages to the Debug Log
            /// NOTE!!!  
            /// Comparison for SpExecResult.Duration passes when expected Duration is greater than 0 and actual.Duration IS LESS THAN OR EQUAL TO expected.Duration
            /// To disable this check set expected.Duration to 0
            /// 

            Assert.True(actual.IsEquivalent(expected));
        }

     

    }
}
