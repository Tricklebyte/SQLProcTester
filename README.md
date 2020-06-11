# SQLProcTester
* Unit test SQL Server Stored Procedures 
* Test stored procedures in your existing database without using EntityFramework
* Faster to set up and easier to use than Microsoft SQL Server Data Tools

## SQLProcTester.SpSqlClient.Execute (SpExecInput input)
Static method executes a sql stored procedure and returns a results model that may be compared to an expected results model.

```C#
 public static SpExecResult Execute(SpExecInput input)
```

### Input Model
 **SQLProcTester.SpExecInput**
<br/> Execute input model - contains all required information to run the stored procedure

* **ConnectionString (string)** - the connection string to the SQL Server database containing the stored procedure under test
* **CommandTImeout (int)** - the number of seconds to wait before terminating the attempt to execute a command (0=infinite)
* **SPName (string)** - the name of the stored procedure to run.
* **SqlParams (List\<SqlParamInput>)**  - list of simplified SQL parameters. Will be converted to full SQL Parameter objects during Execute. 
* **NonQuery** *bool*    
   * When true, no row results will be returned from the database and the number of records affected will be written to SpExecResult.ReturnValue.
   * When false, row results will be written to SpExecResult.DbRows and the ReturnValue of the stored procedure will be written to SpExecResult.ReturnValue.


## Default Input Values
### All Input properties may also be set at the SpSqlClient class level.
<br/>**Values of the input model override the values of the Class Properties**
<br/>This allows you to set an input (like Connection String) once for all tests and not have to repeat that value in all test cases.
<br/> 

## Result Model
  **SQLProcTester.SpExecResult**
<br/> Execution Result Model Properties - Contains execution information and row results when applicable
* **Duration : long** - Elapsed time of execution (milliseconds)
* **ReturnValue : int** - Will be the returnValue for query procedures, and the number of rows affected for non-query procedures.
* **ResultText : string** - Execution information written by SpSqlClient including error info.
* **DBRows : List\<DbRow>** - Each DBRow model represents one row of data returned from a procedure that queries data
<Br/> Execution Result Model Comparator Method 
* **IsEquivalent(SpExecResult) : bool** - - Performs deep compare for easy test assertions.  
<br/> **NOTE** on the IsEquivalent method and the **Duration** field


## Example Unit Test
The example is an XUnit Test project created in Visual Studio 2019

### Setup Demo Database
Example table, data, and stored procedures are provided in SQL Script [SQLProcTester-Sample.sql](https://github.com/Tricklebyte/SQLProcTester/blob/master/samples/SQL/SQLProcTester-Sample.sql) in the samples\SQL folder.
<br/> Run this script on an existing SQL Server database to set up the sample stored procedures for the example

### Set Common Input Properties
Static class SqlSpClient has static properties for each field of the input model.
This enables you to set test input properties globally at the class level without including the value in every test input.
When specified in both places, the input file takes precedence.

#### Connection String
Here in our example we are going to set the connection string at the Class Level in the Constructor of the test class. Then we won't need to repeat it in the test input model for every test. 
```c#
public SpTests()
        {
            // Same connection string for all tests, overidden from input model
            SqlSpClient.ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=SqlProcTest;Trusted_Connection=True;MultipleActiveResultSets=true";
        }
```

### Create the Input Model for the first test (ARRANGE)
**The input model contains the data required to execute the stored procedure (except for the ConnectionString which has already been globally set in the Constructor).**
<br/> The example test uses a Json file to create the **SpExecInput** input model. 
<br/> It supplies input values for the stored procedure name and parameters.
<br/>  The json below will execute a stored procedure named **spGetById**
<br/>  The Stored Procedure will be executed with a single parameter of type **int**, with the name **id**, and the value **1** 

```json
{
 "SpName": "spGetById",
 "SqlParams": [
    {
      "Name": "id",
      "Type": "Int",
      "Value": "1"
    }
  ]
}
```

### Create the Expected Model for the first test (ARRANGE)
**The expected model will mimic the actual result of the SpSqlClient.Execute Method.**
<br/> The example test uses a Json file to create the expected **SpExecResult** model
```json
<br/> {
  "Duration": 6500000,
  "ReturnValue": 0,
  "ResultText": null,
  "RowsAffected": null,
  "DbRows": [
    {
      "DbFields": [
        {
          "Key": "Id",
          "Value": "1"
        },
        {
          "Key": "LastName",
          "Value": "Barker"
        },
        {
          "Key": "FirstName",
          "Value": "Bob"
        },
        {
          "Key": "DateOfBirth",
          "Value": "1/2/1968 12:00:00 AM"
        },
        {
          "Key": "IsContractor",
          "Value": "True"
        },
        {
          "Key": "Position",
          "Value": "Space Travel Agent"
        },
        {
          "Key": "StartDate",
          "Value": "7/25/2018 12:00:00 AM"
        },
        {
          "Key": "EndDate",
          "Value": "3/5/2019 12:00:00 AM"
        }
      ]
    }
  ]
}
```
### Complete Test Method

```c#
 [InlineData("Query\\spGetById", "1")]
        public void ExecuteGood(string testPath, string testCase)
        {
            //ARRANGE
              // Create the input model
            var input = JsonConvert.DeserializeObject<SpExecInput>(File.ReadAllText($"{basePath}\\{procedure}\\input{testCase}.json"));
           
              // Create the expected model
            var expected = JsonConvert.DeserializeObject<SpExecResult>(File.ReadAllText($"{basePath}\\{procedure}\\expected{testCase}.json"));

            //ACT
            // execute the test method
            SpExecResult actual = SqlSpClient.Execute(input);

            //ASSERT
            /// SpExecOutput.IsEquivalent method performs deep compare and generates detailed error messages to ResultText property and the Debug Log
            /// NOTE!!!  
            /// Comparison for SpExecResult.Duration passes when expected Duration is greater than 0 and actual.Duration IS LESS THAN OR EQUAL TO expected.Duration
            /// To disable this check set expected.Duration to 0
            /// 
            Assert.True(actual.IsEquivalent(expected));
        }
        ```
