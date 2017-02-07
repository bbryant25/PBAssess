using System;
using System.Text.RegularExpressions;
using System.Data;

namespace PBAssess
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Ideally, I would create discrete interfaces for UI, data and business rules
             * 
             * Note: For simplicity, I put the units.txt file in the debug path so no specific file path would be needed
             */

            // Input
            // 1.  Get data from text file into array

            // Process
            // 2.  Create datatable
            // 3.  Build datatable
            // 4.  Sort datatable

            // Output
            // 5.  Print datatable


            // Get data into a string array
            string[] lines1 = System.IO.File.ReadAllLines("units.txt");

            DataTable dt = new DataTable();
            dt.Columns.Add(); // Original string in column 0
            dt.Columns.Add(); // Unit number for sorting in column 1
            dt.Columns[1].DataType = Type.GetType("System.Int32");
            dt.Columns.Add(); // Unit Subnumber 

            // Parse each string into a new array of unit number and resident name
            for (int i=0; i < lines1.Length; i++)
            {
                DataRow dtrow = dt.NewRow();  // Allocate a row
                dtrow[0] = lines1[i];           // Put original data in column 0 

                lines1[i] = lines1[i].TrimStart('#');  // Remove leading #

                string[] cols = lines1[i].Split('-');

                for (int j=0; j < cols.Length; j++)
                {
                    cols[j] = cols[j].Trim(' '); //Remove leading and trailer spaces from unit and resident

                    if (j == 0)
                    {
                        //Parse Unit Number into Primary and Sub (Example:  100A)
                        string[] unit = Regex.Split(cols[j], @"\D+");

                        Regex rgx = new Regex("[^a-zA-Z -]");
                        string subunit = rgx.Replace(cols[j], "");

                        // After split, column 0 (j) in cols array is Unit Number / column 1 is Resident Name
                        dtrow[1] = unit[0];  // Put Unit Number in Column 1 of Data Table
                        dtrow[2] = subunit;
                    }
                }
                dt.Rows.Add(dtrow);
            }

            DataRow[] sortedrows = dt.Select("", "COLUMN2 ASC, COLUMN3 ASC");

             for (int lcnt=0; lcnt < sortedrows.Length; lcnt++)
             {
                for (int IAcnt=0; IAcnt < sortedrows[lcnt].ItemArray.Length; IAcnt++)
                {
                    if (IAcnt == 0)
                    {
                        Console.WriteLine(sortedrows[lcnt].ItemArray[IAcnt]);
                    }
                }
             }

            Console.Out.WriteLine("Press anykey to continue...");
            Console.ReadKey();
        }
    }
}
