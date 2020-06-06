using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLProcTester.Models
{
    public class DbRow
    {
        public List<KeyValuePair<string, string>> DbFields { get; set; }

        public DbRow()
        {
            DbFields = new List<KeyValuePair<string, string>>();
        }

        public bool IsEquivalent(DbRow expectedRow)
        {
            bool retVal = DbFields.Count == expectedRow.DbFields.Count();

            if (!retVal)
            {
                Debug.WriteLine($"Field counts not equal. {DbFields.Count().ToString()} | {expectedRow.DbFields.Count().ToString()}");
                retVal = false;
                return retVal;
            }


            for (int i = 0; i < DbFields.Count(); i++)
            {
                if (DbFields[i].Key != expectedRow.DbFields[i].Key)
                {
                    retVal = false;
                    Debug.WriteLine($"DbField Row index {i.ToString()}:  Actual Key: {DbFields[i].Key} | Expected Key: {expectedRow.DbFields[i].Key}  Value: {expectedRow.DbFields[i].Value} ");
                    
                }
                if (DbFields[i].Value != expectedRow.DbFields[i].Value)
                {
                    retVal = false;
                    Debug.WriteLine($"DbField Row index {i.ToString()}:  Actual Key: {DbFields[i].Key} Actual Value: {DbFields[i].Value}, Expected Value: {expectedRow.DbFields[i].Value} ");
                   
                }
            }

            return retVal;
        }
   
    
    }
}
