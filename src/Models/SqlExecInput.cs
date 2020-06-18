using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace SQLProcTester.Models
{
    public class SqlExecInput
    {
        public string ConnectionString { get; set; }
        public string SqlText { get; set; }
        
    }
}
