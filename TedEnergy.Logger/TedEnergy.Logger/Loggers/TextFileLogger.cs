using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TedEnergy.Logger.Loggers
{
    public class TextFileLogger : ILogger
    {
        private string fileName;

        public TextFileLogger()
        {
            SetFileName();  
        }
        public void LogDebug(string message)
        {
            WriteToFile("DEBUG", message);
        }

        public void LogException(Exception exception)
        {
            WriteToFile("EXCEPTION", exception.ToString());
        }

        public void LogException(string message, Exception exception)
        {
            WriteToFile("EXCEPTION", message, exception.ToString());
        }

        private void SetFileName()
        {
            fileName = DateTime.Now.ToString("yyyy-MM-dd") + "-ErrorLog.log";
        }

        private void WriteToFile(params string[] dataToWrite)
        {
            SetFileName();
            using (StreamWriter sw = new StreamWriter(fileName, append: true))
            {
                string output = DateTime.Now.ToString();
                foreach (string data in dataToWrite)
                {
                    output += "," + data;
                }
                sw.WriteLine(output);
                sw.Close();
            }
        }
    }
}
