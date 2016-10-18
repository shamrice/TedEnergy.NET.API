using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TedEnergy.Logger
{
    public interface ILogger
    {
        void LogDebug(string message);
        void LogException(Exception exception);
        void LogException(string message, Exception exception);
        
    }
}
