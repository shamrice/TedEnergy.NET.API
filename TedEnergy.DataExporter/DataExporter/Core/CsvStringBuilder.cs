using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TedEnergy.DataExporter.Core
{
    public class CsvStringBuilder
    {
        public string Build(params object[] fields)
        {
            string result = string.Empty;
            for (int i = 0; i < fields.Length; i++)
            {
                result += fields[i];
                //if (i != fields.Length - 1)
                    result += ",";
            }            
            return result;
        }
    }
}
