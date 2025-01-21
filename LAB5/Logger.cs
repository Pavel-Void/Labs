using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooshopApp
{
    static class Logger
    {
        static string logFile;

        public static void Initialize()
        {

            logFile = @"C:\Users\Pavel\repos\Labs\LAB5\logs.txt";
            if (!File.Exists(logFile))
            {
                File.Create(logFile).Close();
            }

            Log("Протоколирование начато.");
        }

        public static void Log(string message)
        {
            File.AppendAllText(logFile, $"[{DateTime.Now}] {message}\n");
        }
    }
}
