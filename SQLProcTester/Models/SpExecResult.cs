using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;


namespace SQLProcTester.Models
{
    public class SpExecResult
    {
        public long Duration { get; set; }

        public int? ReturnValue { get; set; }

        public string ResultText { get; set; }

        public List<DbRow> DbRows { get; set; }

        public bool IsEquivalent(SpExecResult expected)
        {
            bool retVal;
            // RETURN VALUE
            if (this.ReturnValue != expected.ReturnValue)
            {
                retVal = false;
                DebugLogger.LogEquivalencyError("ReturnValue", this.ReturnValue?.ToString(), expected.ReturnValue?.ToString());
            }
            else retVal = true;

            // RESULT TEXT 
            if (this.ResultText != expected.ResultText)
            {
                retVal = false;
                DebugLogger.LogEquivalencyError("ResultText", this.ResultText, expected.ResultText);
            }

            // If the expected.Duration is greater than zero, actual duration must not exceed expected duration. 
            if (expected.Duration > 0 && this.Duration > expected.Duration)
            {
                retVal = false;
                DebugLogger.LogError($"EQUIVALENCY CHECK FAIL: 'SpExecResut.Duration' Expected Duration is greater than zero and Actual Duration({this.Duration}) exceeded Expected Duration({expected.Duration}). Set expected.Duration to 0 to disable this check.");
            }



            // If the actual has rows, then compare to expected
            if (this.DbRows != null)
            {
                if (expected.DbRows != null)
                {
                    if (!IsListEquivalent(expected.DbRows))
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

        // List<DbRow> Comparator
        private bool IsListEquivalent(List<DbRow> expectedList)
        {

            bool retVal;
            // check if counts are equal - 
            bool chkVal = this.DbRows.Count == expectedList.Count;

            if (!chkVal)
            {
                DebugLogger.LogEquivalencyError($"EQUIVALENCY CHECK FAIL: 'SpExecResult.DbRows.Count' ", DbRows.Count.ToString(), expectedList.Count.ToString());
                retVal = false;
            }
            else
            {
                retVal = true;

                for (int i = 0; i < expectedList.Count; i++)
                {
                    if (!DbRows[i].IsEquivalent(expectedList[i]))
                    {
                        DebugLogger.LogError($"EQUIVALENCY CHECK FAIL: 'SpExecResult.DbRows' Actual DbRow at index '{i.ToString()}' not equivalent to Expected");
                        retVal = false;
                    }
                }
            }
            return retVal;

        }


    }


}
