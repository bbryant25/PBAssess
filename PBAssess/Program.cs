#region     HEADER DOC
/*-----------------------------------------------------------------------
 *  Program:     Property Brands Assessment
 *  Written By:  Bobby Bryant
 *  Date:        2/7/2017
 *  Description: Reads data from flat-file, sorts by Unit Number and prints
 *               to STDOUT.
 * ----------------------------------------------------------------------*/
#endregion      HEADER DOC

#region     DETAIL DOC
/*-----------------------------------------------------------------------
 * Assign#  Date        Changed By          Description
 * ----------------------------------------------------------------------
 * 1        2/7/2017    Bobby Bryant        Created program.
 * 
 * 
 * 
 * ----------------------------------------------------------------------*/
#endregion      DETAIL DOC

using System;
using System.Text.RegularExpressions;
using System.Data;
using Common.DataAccess;
using PBAssess.BusinessLogic;

namespace PBAssess
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
             * Note: For simplicity, I put the units.txt file in the debug path so no specific file path would be needed
             */
            
            //////////////////////////////////////////////////////////////////
            // Inputs
            //////////////////////////////////////////////////////////////////
            // 1.  Get data from text file into array
            //////////////////////////////////////////////////////////////////

            var pbRAL = new Common.DataAccess.Access();
            string[] rawData = pbRAL.getAssessData("units.txt");

            //////////////////////////////////////////////////////////////////
            // Process Data
            //////////////////////////////////////////////////////////////////
            // 2.  Create datatable
            //////////////////////////////////////////////////////////////////

            var pbDT = new BusinessLogic.PBAssessBO();
            DataTable dt = pbDT.createPBAssessDataTable();

            //////////////////////////////////////////////////////////////////
            // 3.  Build datatable
            //////////////////////////////////////////////////////////////////
            // Parse each string into a new array of unit number and resident name
            for (int i = 0; i < rawData.Length; i++)
            {
                DataRow dtRow = dt.NewRow();  // Allocate a row
                dtRow[0] = rawData[i];        // Put original data in column 0 

                rawData[i] = rawData[i].TrimStart('#');  // Remove leading # from Unit Number

                string[] cols = rawData[i].Split('-');   // Split string into separate Unit Number and Residents Name

                for (int j = 0; j < cols.Length; j++)
                {
                    cols[j] = cols[j].Trim(' '); //Remove leading and trailer spaces from unit and resident

                    if (j == 0)
                    {
                        //Parse Unit Number into Primary and Sub (Example:  100A) so they can be sorted accurately
                        string[] unit = Regex.Split(cols[j], @"\D+");              // Get Unit Number

                        Regex rgx = new Regex("[^a-zA-Z -]");                      // Get Unit Subnumber
                        string subunit = rgx.Replace(cols[j], "");

                        // After split, column 0 (j) in cols array is Unit Number / column 1 is Resident Name
                        dtRow[1] = unit[0];  // Put Unit Number in Column 1 of Data Table
                        dtRow[2] = subunit;  // Put Unit Subnumber in Column 2 of Data Table
                    }
                }
                
                dt.Rows.Add(dtRow);         // Add row to data table so it can sorted
            }

            ////////////////////////////////////////////////////////////////////
            // 4.  Sort datatable
            ////////////////////////////////////////////////////////////////////

            DataRow[] sortedRows = dt.Select("", "COLUMN2 ASC, COLUMN3 ASC");   // Sort data table by Unit Number (column2) and Subnumber (column3)

            ////////////////////////////////////////////////////////////////////
            // Output
            ////////////////////////////////////////////////////////////////////
            // 5.  Print datatable
            ////////////////////////////////////////////////////////////////////

            for (int lcnt=0; lcnt < sortedRows.Length; lcnt++)
             {
                for (int iaCnt=0; iaCnt < sortedRows[lcnt].ItemArray.Length; iaCnt++)
                {
                    if (iaCnt == 0)
                    {
                        Console.WriteLine(sortedRows[lcnt].ItemArray[iaCnt]);
                    }
                }
             }

            Console.Out.WriteLine("Press anykey to continue...");
            Console.ReadKey();
        }
    }
}
