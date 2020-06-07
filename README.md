# SQLProcTester
Unit test SQL Stored Procedures without EntityFramework
* Test the stored procedures in your existing database
* Faster to set up and easier to use than Microsoft SQL Server Data Tools

## SQLProcTester.SpSqlClient.Execute (SpExecInput input)
Static method executes a sql stored procedure and returns a results model that may be compared to an expected results model.


```C#
 public static SpExecResult Execute(SpExecInput input)
```

### Parameters
**input** SpExecInput
<br/> Execute input model - contains all required information to run the stored procedure


### SpExecInput
* **ConnectionString** - the connection string to the SQL Server database containing the stored procedure under test
* **CommandTImeout** - the number of seconds to wait before terminating the attempt to execute a command
* **SPName** - the name of the stored procedure to run.
* **SqlParams** - list of simplified SQL parameters. Will be converted to full SQL Parameter objects during PrepRun. 
* **NonQuery** (bool) 
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
