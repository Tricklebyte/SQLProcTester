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

            // check Equivalency of base class first by passing the base class type into the base class IsEquivalent method.
            //   
            if (!this.IsEquivalent((SpExecResult)expected))
            {
                retVal = false;
                string msg = DebugLogger.CreateErrorDetail("SpExecResultNonQuery", $"Equivalency check failed in base class SpExecResult.");
                DebugLogger.LogError(msg);
            }
            else
            {
                retVal = true;
            }


            // check equivalency of this child
            // PostInspectResultText
            if (this.PostInspectResultText != expected.PostInspectResultText)
            {
                retVal = false;
                DebugLogger.LogEquivalencyError("PostInspectResultText", this.PostInspectResultText, expected.PostInspectResultText);
            }
            else
                retVal = true;

            // TODO- clean up use of base classes
            // Once all the child properties have been checked call the IsEquivalent Method using the base result classes

            return retVal;
        }


    }
}
