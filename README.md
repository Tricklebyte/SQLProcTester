# SQLProcTester
Unit test SQL Stored Procedures without EntityFramework
* Test the stored procedures in your existing database
* Faster to set up and easier to use than Microsoft SQL Server Data Tools

## SQLProcTester.SpSqlClient.Execute (SpExecInput input)
Static method executes a sql stored procedure and provides a results model that may be compared to an expected results model.


```C#
 public static SpExecResult Execute(SpExecInput input)
```

### Parameters
**input** SpExecInput
<br/> Execute input model - contains all required information to run the stored procedure




```
