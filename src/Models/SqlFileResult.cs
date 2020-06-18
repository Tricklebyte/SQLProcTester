using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;


namespace SQLProcTester.Models
{
    public class SqlFileResult
    {
        public int ReturnValue;
        public string ResultText;        

        public bool IsEquivalent(SqlFileResult expected)
        {
            bool retVal;
            // RETURN VALUE


            // RESULT TEXT 
            if (this.ResultText != expected.ResultText)
            {
                retVal = false;
                DebugLogger.LogEquivalencyError("ResultText", this.ResultText, expected.ResultText);
            }
            else
                retVal = true;

            if (this.ResultText != expected.ResultText)
            {
                retVal = false;
                DebugLogger.LogEquivalencyError("ResultText", this.ResultText, expected.ResultText);
            }
            else
                retVal = true;

            return retVal;

        }
    
    
    
    
    
    }


}
