using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SQLProcTester
{
    public static class DebugLogger
    {
        public static string CreateErrorDetail(string objectName, string exception, string innerException = null)
        {
            string errMsg = $"ERROR! Object: {objectName} | Message: {exception}\n ";
            if (innerException != null)
            {
                errMsg += $"Inner Exception: {innerException}";
            }
            return errMsg;
        }
        
        public static void LogError(string error)
        {
            Debug.WriteLine(error);
        }

        public static void LogEquivalencyError(string objName, string actualValue, string expectedValue)
        {
            Debug.WriteLine($"EQUIVALENCY CHECK FAIL for {objName}");
            Debug.WriteLine($"            ACTUAL({actualValue}) | EXPECTED({expectedValue})");
        }

        public static void LogListEquivalencyError(string objName, int listIndex, string actualKey, string expectedKey, string actualValue, string expectedValue)
        {
            Debug.WriteLine($"EQUIVALENCY CHECK FAIL for {objName}");
            Debug.WriteLine($"            AT LIST INDEX  ({listIndex})");
            if (actualKey != expectedKey)
                Debug.WriteLine($"            Actual Key     ({actualKey})   | Expected Key ({expectedKey}))");
            if (actualValue != expectedValue)
                Debug.WriteLine($"            Actual Value   ({actualValue}) | Expected Value ({expectedValue})");

        }

    }
}
