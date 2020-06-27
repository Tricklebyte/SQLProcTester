using Microsoft.Data.SqlClient;
using SQLProcTester.Models;
using System;
using System.Collections.Generic;
using System.Data;
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


        public static void AddSqlParameters(this SqlCommand cmd, IEnumerable<SqlParamInput> paramsIn)
        {
            if (paramsIn == null)
            {
                paramsIn = new List<SqlParamInput>();
            }

            foreach (var input in paramsIn)
            {
                string type = input.Type.ToUpper();
                string name = input.Name.Replace("@", "");
                name = "@" + input.Name;
                SqlParameter sqlParam = new SqlParameter(name, null);
                try
                {
                    switch (type)
                    {
                        case "DATETIME":
                        case "DATETIME2":
                            sqlParam.SqlDbType = SqlDbType.DateTime;
                            sqlParam.Value = Convert.ToDateTime(input.Value);
                            break;
                        case "NCHAR":
                            sqlParam = new SqlParameter(name, System.Data.SqlDbType.NChar)
                            { Value = input.Value.ToString() };
                            break;
                        case "CHAR":
                            sqlParam = new SqlParameter(name, System.Data.SqlDbType.Char)
                            { Value = input.Value.ToString() };
                            break;
                        case "NVARCHAR":
                            sqlParam = new SqlParameter(name, System.Data.SqlDbType.NVarChar)
                            { Value = input.Value.ToString() };
                            break;
                        case "VARCHAR":
                            sqlParam = new SqlParameter(name, System.Data.SqlDbType.VarChar)
                            { Value = input.Value.ToString() };
                            break;
                        case "INT":
                            sqlParam = new SqlParameter(name, System.Data.SqlDbType.Int)
                            { Value = Convert.ToInt32(input.Value) };
                            break;
                        case "BIT":
                            sqlParam = new SqlParameter(name, System.Data.SqlDbType.Bit)
                            { Value = Convert.ToInt32(input.Value) };
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("SqlParameter.Type", $"Invalid type specified for parameter: {name}");
                    }

                }
                catch (Exception e)
                {
                    throw new Exception($"Parameter Exception - '{name}' : {e.Message}");
                }
                cmd.Parameters.Add(sqlParam);
            }
            // Add the ReturnValue Parameter last
            var param = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(param);

        }


        
    }






}
