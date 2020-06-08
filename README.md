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
* **SqlParams (List\<SqlParamInput>)**  - list of simplified SQL parameters. Will be converted to full SQL Parameter objects during PrepRun. 
* **NonQuery** *bool*    
   * When true, no row results will be returned from the database and the number of records affected will be written to SpExecResult.ReturnValue.
   * When false, row results will be written to SpExecResult.DbRows and the ReturnValue of the stored procedure will be written to SpExecResult.ReturnValue.


```json
{
  "ConnectionString": "Server=(localdb)\\MSSQLLocalDB;Database=SqlProcTest;Trusted_Connection=True;MultipleActiveResultSets=true",
  "CommandTimeout": 0,
  "NonQuery": false,
  "SpName": "spGetCurrentByName",
  "SqlParams": [
    {
      "Name": "LastName",
      "Type": "NvarChar",
      "Value": "Bobbin"
    },
    {
      "Name": "FirstName",
      "Type": "NvarChar",
      "Value": "Babaduk"
    }
  ]
}
```

## Default Input Values
### All Input properties may also be set at the SpSqlClient class level.
<br/>**Values of the input model override the values of the Class Properties**
<br/>This allows you to set an input (like Connection String) once for all tests and not have to repeat that value in all test cases.
<br/> 

## Result Model
  **SQLProcTester.SpExecResult**
<br/> Execute Result Model - Contains execution information and row results when applicable

* **Duration (long)** - Elapsed time of execution (milliseconds)
* **ReturnValue (int)** - Will be the returnValue for query procedures, and the number of rows affected for non-query procedures.
* **ResultText (string)** - Execution information written by SpSqlClient - usually error info.
* **DBRows (List\<DbRow>)** - Each DBRow model represents one row of data returned from a procedure that queries data


```json
{
  "Duration": 6500000,
  "ReturnValue": 0,
  "ResultText": null,
  "DbRows": [
    {
      "DbFields": [
        {
          "Key": "Id",
          "Value": "2"
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
