using SQLProcTester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLProcTester
{
    public static class Extensions
    {
      
        public static int ParseInt(this string value, int defaultIntValue = 0)
        {
            int parsedInt;
            if (int.TryParse(value, out parsedInt))
            {
                return parsedInt;
            }

            return defaultIntValue;
        }
       
        
        public static int? ParseNullableInt(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return value.ParseInt();
        }


        // List<DbRow> Comparator
        public static bool IsListEquivalent(this List<DbRow> actualList, List<DbRow> expectedList)
        {

            bool retVal;
            // check if counts are equal - 
            bool chkVal = actualList.Count == expectedList.Count;

            if (!chkVal)
            {
                DebugLogger.LogEquivalencyError($"EQUIVALENCY CHECK FAIL: 'SpExecResult.DbRows.Count' ", actualList.Count.ToString(), expectedList.Count.ToString());
                retVal = false;
            }
            else
            {
                retVal = true;

                for (int i = 0; i < expectedList.Count; i++)
                {
                    if (!actualList[i].IsEquivalent(expectedList[i]))
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
