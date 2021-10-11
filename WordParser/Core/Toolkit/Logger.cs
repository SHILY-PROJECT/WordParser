using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WordParser.Core.Enums.Logger;

namespace WordParser.Core.Toolkit
{
    /// <summary>
    /// Класс для логирования сообщений.
    /// </summary>
    internal class Logger
    {
        /// <summary>
        /// Информация о файле лога.
        /// </summary>
        private static readonly FileInfo _log = new(Path.Combine("logs", $"log - {DateTime.Now:yyyy-MM-dd}.log"));

        /// <summary>
        /// Записать сообщение в файл лога.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="typeLog"></param>
        public static async void Write(string message, LogTypeEnum typeLog)
        {
            if (_log == null || _log.Directory == null) return;
            else if (!_log.Directory.Exists) _log.Directory.Create();

            var currentDatetime = $"{DateTime.Now:yyyy-MM-dd   HH-mm-ss}";

            var formatting = new Dictionary<LogTypeEnum, string>
            {
                [LogTypeEnum.Info] = $"[{currentDatetime}]      |INFO|      {message}",
                [LogTypeEnum.Warning] = $"[{currentDatetime}]      |WARNING|   {message}",
                [LogTypeEnum.Error] = $"[{currentDatetime}]      |ERROR|     {message}"
            };

            await File.AppendAllLinesAsync(_log.FullName, new[] { formatting[typeLog] }, Encoding.UTF8);
        }
    }
}
