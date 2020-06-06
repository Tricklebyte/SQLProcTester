using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace SQLProcTester.Models
{
    public class SpExecInput
    {
        public string ConnectionString { get; set; }
        public string SpName { get; set; }
        public int? CommandTimeout { get; set; }
        public  List<SqlParamInput> SqlParams { get; set; }
        public bool? NonQuery { get; set; }

    }
}
