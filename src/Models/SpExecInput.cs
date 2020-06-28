using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace SQLProcTester.Models
{
    public class SpExecInput
    {
        //The SqlServer database connection string, including database name
        public string ConnectionString { get; set; }

        //The name of the stored procedure to execute
        public string SpName { get; set; }

        //The wait time (in seconds) before terminating the attempt to execute a command and generating an error
        public int? CommandTimeout { get; set; }

        //List of simplified SQL Parameters to be passed to the stored procedure.Will be converted to actual Sql parameters during execution
        public  List<SqlParamInput> SqlParams { get; set; }

    }
}
