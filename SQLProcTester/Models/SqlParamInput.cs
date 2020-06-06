using System;
using System.Collections.Generic;
using System.Text;

namespace SQLProcTester.Models
{
    public class SqlParamInput
    {
     
        public string Name { get; set; }


        /// <summary> Will be used determine datatype of SQL Parameter
        /// Acceptable Values
        ///     DateTime  -DateOfBirth
        ///     DateTIme2 -StartDate, EndDate
        ///     Int       -Ids
        ///     Nvarchar  -Names
        ///     Varchar   -Position.Description
        ///     Bit       -Employee.IsContractor
        /// </summary>
        public string Type { get; set; }



        /// <summary>
        /// Parameter value in string format
        /// </summary>
        public string Value { get; set; }
    }
}
