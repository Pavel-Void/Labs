/// <file>
/// Содержит класс Logger для ведения журнала событий приложения.
/// </file>
using System;
using System.IO;

namespace ZooshopApp
{
    /// <summary>
    /// Предоставляет функциональность для ведения журнала событий приложения.
    /// </summary>
    public static class Logger
    {
        private static string _logFile;

        /// <summary>
        /// Инициализирует логгер и создаёт файл журнала, если он не существует.
        /// </summary>
        public static void Initialize()
        {
            _logFile = @"C:\Users\Pavel\repos\Labs\LAB5\logs.txt";
            if (!File.Exists(_logFile))
            {
                File.Create(_logFile).Close();
            }

            Log("Протоколирование начато.");
        }

        /// <summary>
        /// Записывает сообщение в файл журнала с отметкой времени.
        /// </summary>
        /// <param name="message">Сообщение для записи в журнал.</param>
        public static void Log(string message)
        {
            File.AppendAllText(_logFile, $"[{DateTime.Now}] {message}\n");
        }
    }
}
