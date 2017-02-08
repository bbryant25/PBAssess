using System;
using System.Data;

namespace PBAssess.BusinessLogic
{
    public class PBAssessBO
    {
        /// <summary>
        /// create DataTable used to sort Property Brands Assessment data.
        /// </summary>
        /// <param name="cmd">None.</param>
        public DataTable createPBAssessDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(); // Original string in column 0
            dt.Columns.Add(); // Unit number for sorting in column 1
            dt.Columns[1].DataType = Type.GetType("System.Int32");
            dt.Columns.Add(); // Unit Subnumber 

            return dt;
        }
    } 
}
