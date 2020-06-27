using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;


namespace SQLProcTester.Models
{
    public class SpExecResultNonQuery : SpExecResult
    {

        public string PostInspectResultText { get; set; }

        public bool IsEquivalent(SpExecResultNonQuery expected)
        {

            bool retVal;

            // check Equivalency of base class first
            //   
            if (!base.IsEquivalent(expected))
            {
                retVal = false;
                string msg = DebugLogger.CreateErrorDetail("SpExecResultNonQuery", $"Equivalency check failed in base class SpExecResult.");
                DebugLogger.LogError(msg);
            }
            else
            {
                retVal = true;
            }


            // check equivalency of the child properties
            // PostInspectResultText
            if (this.PostInspectResultText != expected.PostInspectResultText)
            {
                retVal = false;
                DebugLogger.LogEquivalencyError("PostInspectResultText", this.PostInspectResultText, expected.PostInspectResultText);
            }
            

            return retVal;
        }


    }
}
