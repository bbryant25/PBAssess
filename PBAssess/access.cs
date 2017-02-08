using System;

namespace Common.DataAccess
{
    public class Access
    {
        /// <summary>
        /// Get data for Property Brands Assessment.
        /// </summary>
        /// <param name="cmd">Filename containing data.</param>
        public string[] getAssessData(string FileName)
        {
            string[] lines = System.IO.File.ReadAllLines(FileName);

            return lines;
        }
    }
}
