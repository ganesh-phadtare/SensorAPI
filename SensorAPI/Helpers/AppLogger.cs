using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorIOT.Helpers
{
    public class AppLogger
    {
        static string _FileName = "D:/IOTLogger.txt";
        public static void LogError(Exception ex)
        {
            try
            {
                string logMessageToLog = string.Concat(">> Error occured : ", ex.Message, Environment.NewLine, ex.StackTrace, Environment.NewLine);
                File.AppendAllText(_FileName, logMessageToLog);
            }
            catch { }
        }

        public static void LogTimer(Stopwatch watch)
        {
            try
            {
                string logMessageToLog = string.Concat("Time Taken : ", watch.ElapsedMilliseconds, " MilliSeconds", Environment.NewLine);
                File.AppendAllText(_FileName, logMessageToLog);
            }
            catch { }
        }
    }
}
