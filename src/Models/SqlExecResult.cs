using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;


namespace SQLProcTester.Models
{
    public class SqlExecResult
    {

        public string ErrorMessage;
        public List<DbRow> DbRows { get; set; }

        public SqlExecResult()
        {
            DbRows = new List<DbRow>();
        }

        public bool IsEquivalent(SqlExecResult expected)
        {
            bool retVal;
            // RETURN VALUE


            // RESULT TEXT 
            if (this.ErrorMessage != expected.ErrorMessage)
            {
                retVal = false;
                DebugLogger.LogEquivalencyError("ResultText", this.ErrorMessage, expected.ErrorMessage);
            }
            else
                retVal = true;
           

            // If the actual has rows, then compare to expected
            if (this.DbRows != null)
            {
                if (expected.DbRows != null)
                {
                    if (!this.DbRows.IsListEquivalent(expected.DbRows))
                    {
                        retVal = false;
                    }
                }
                else // this.dbRows is not null, but expected is
                    retVal = false;
            }
            else // the actual is null, check if the expected is not null
            {
                if (expected.DbRows != null)
                    retVal = false;
            }
            return retVal;
        }
   
    }
}
