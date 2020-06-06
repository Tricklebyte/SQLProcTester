using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SQLProcTester.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
namespace SQLProcTester
{
   public static class TestHelper
    {
        
        public static IConfiguration GetFileConfig(string path)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(path, false);
            return builder.Build();
        }

       // public static bool IsEquivalent(this List<DbRow> actualList, List<DbRow> expectedList)
        //{

        //    bool retVal = false;
        //    // check if counts are equal - 
        //    bool chkVal = actualList.Count() == expectedList.Count();

        //    if (!chkVal)
        //    {
        //        Debug.WriteLine($"List counts not equal. Actual: {actualList.Count()}, Expected: {expectedList.Count()}");
        //        retVal = false;

        //    }
        //    else
        //    {
        //        retVal = true;
        //        for (int i = 0; i < expectedList.Count(); i++)
        //        {
        //            if (!actualList[i].IsEquivalent(expectedList[i]))
        //                retVal = false;
        //        }
        //    }
        //    return retVal;

        //}

        public static void Save(this List<DbRow> saveList, string path)
        {
            string jsonString = JsonConvert.SerializeObject(saveList);


        }


    }
}
