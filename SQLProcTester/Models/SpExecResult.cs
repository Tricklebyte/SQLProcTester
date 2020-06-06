using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;


namespace SQLProcTester.Models
{
    public class SpExecResult
    {
        public long? Duration { get; set; }

        public int? ReturnValue { get; set; }

        public string ResultText { get; set; }

        public int? RowsAffected { get; set; }

        public List<DbRow> DbRows { get; set; }

        public bool IsEquivalent(SpExecResult expected) {
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
                DebugLogger.LogError($"SpExecResut.Duration)",$"Expected Duration is greater than zero and Actual Duration({this.Duration}) exceeded Expected Duration({expected.Duration}. Set expected.Duration to 0 to disable this check.");
            }

            // ROWS AFFECTED
            if (this.RowsAffected != expected.RowsAffected)
            {
                retVal = false;
                DebugLogger.LogEquivalencyError("RowsAffected", this.RowsAffected?.ToString(), expected.RowsAffected?.ToString());
            }

            // DBRow Query Results

            if (!IsListEquivalent(expected.DbRows))
            {
                retVal = false;
                DebugLogger.LogError("SpExecResutl.DbRows",$"EQUIVALENCY CHECK FAIL");
            }

            return retVal;

        }

        // List<DbRow> Comparator
        private  bool IsListEquivalent(List<DbRow> expectedList)
        {

            bool retVal;
            // check if counts are equal - 
            bool chkVal =  this.DbRows.Count == expectedList.Count;

            if (!chkVal)
            {
                DebugLogger.LogEquivalencyError($"SpExecResult.DbRows.Count", DbRows.Count.ToString(), expectedList.Count.ToString());
                retVal = false;
            }
            else
            {
                retVal = true;

                for (int i = 0; i < expectedList.Count; i++)
                {
                    if (!DbRows[i].IsEquivalent(expectedList[i]))
                    {
                        DebugLogger.LogError("SpExecResult.DbRows",$"Actual DbRow at index '{i.ToString()}' not equivalent to Expected");
                        retVal = false;
                    }
                }
            }
            return retVal;

        }


    }


}
