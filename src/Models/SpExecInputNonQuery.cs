using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace SQLProcTester.Models
{
    public class SpExecInputNonQuery : SpExecInput
    {
        public string PostInspectSql { get; set; }
        public bool Rollback { get; set; }

    }
}
