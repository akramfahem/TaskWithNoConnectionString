using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using static System.Reflection.Metadata.BlobBuilder;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace SharedLibrary
{
    public class Logger
    {
        private static readonly Lazy<Logger> _instance = new Lazy<Logger>(() => new Logger());
        public static ConcurrentQueue<LogMessage> logMessages;
        private static readonly object _lock = new();
        private readonly static string FullPath = "/Users/akram/Projects/INNOTask2/MyOwnLogger/MyOwnLogger/log/";
        const int MAXFILESIZE = 10240;
        private Logger() { logMessages = new ConcurrentQueue<LogMessage>(); }

        public static Logger GetLogger
        {
            get
            {
                return _instance.Value;
            }
        }
        public async Task Logging(LogType logType, string message)
        {
            LogMessage logMessage = new() { CreatedAt = DateTime.Now, LogType = logType, Message = message };

            logMessages.Enqueue(logMessage);
            lock (_lock)
            {
               while(logMessages.TryDequeue(out var logdata))
                {
                    if(logdata.LogType == LogType.SUCCESS)
                    {
                        LogSucces(logdata);
                    }
                    if (logdata.LogType == LogType.WARNING)
                    {
                        LogWarning(logdata);
                    }
                    if (logdata.LogType == LogType.EXCEPTION)
                    {
                        LogException(logdata);
                    }
                }
            }
        }
        private async Task LogSucces(LogMessage successData)
        {
            int Counter = 1;
            if (!Directory.Exists($"{FullPath}/success"))
            {
                ///Users/akram/Projects/INNOTask2/MyOwnLogger/MyOwnLogger/log
                Directory.CreateDirectory($"{FullPath}/success");
            }
            if (File.Exists($"{FullPath}/success/counter.txt"))
            {
                Counter = int.Parse(File.ReadAllText($"{FullPath}/success/counter.txt"));
            }
            else
            {
                File.WriteAllText($"{FullPath}/success/counter.txt", Counter.ToString());
            }
            string currentfile = $"SUCCESS_{Counter}.json";
            string filePath = Path.Combine($"{FullPath}/success/", currentfile);

            try
            {
                List<LogMessage> logMessages = new List<LogMessage>();
                string? jsonContent = "";

                if (File.Exists(filePath))
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    long maxsize = fileInfo.Length;
                    if (maxsize < MAXFILESIZE)
                    {
                        jsonContent = File.ReadAllText(filePath);

                    }
                    else
                    {
                        Counter++;
                        File.WriteAllText($"{FullPath}/success/counter.txt", Counter.ToString());
                        currentfile = $"SUCCESS_{Counter}.json";
                        filePath = Path.Combine($"{FullPath}/success", currentfile);
                        //jsonContent = File.ReadAllText(filePath);

                    }

                }

                logMessages = JsonConvert.DeserializeObject<List<LogMessage>>(jsonContent) ?? new List<LogMessage>();
                logMessages.Add(successData);
                string? json = JsonConvert.SerializeObject(logMessages, Newtonsoft.Json.Formatting.Indented);
                await File.WriteAllTextAsync(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

        }
        private async Task LogException(LogMessage ExceptionData)
        {
            int Counter = 1;
            if (!Directory.Exists($"{FullPath}/exception"))
            {
                Directory.CreateDirectory($"{FullPath}/exception");
            }
            if (File.Exists($"{FullPath}/exception/counter.txt"))
            {
                Counter = int.Parse(File.ReadAllText($"{FullPath}/exception/counter.txt"));
            }
            else
            {
                File.WriteAllText($"{FullPath}/exception/counter.txt", Counter.ToString());
            }
            string currentfile = $"EXCEPTION_{Counter}.json";
            string filePath = Path.Combine($"{FullPath}/exception", currentfile);
            try
            {
                List<LogMessage> logMessages = new List<LogMessage>();
                string? jsonContent = "";

                if (File.Exists(filePath))
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    long maxsize = fileInfo.Length;
                    if (maxsize < MAXFILESIZE)
                    {
                        jsonContent = File.ReadAllText(filePath);

                    }
                    else
                    {
                        Counter++;
                        File.WriteAllText($"{FullPath}/exception/counter.txt", Counter.ToString());
                        currentfile = $"EXCEPTION_{Counter}.json";
                        filePath = Path.Combine($"{FullPath}/exception", currentfile);
                        jsonContent = File.ReadAllText(filePath);

                    }

                }

                logMessages = JsonConvert.DeserializeObject<List<LogMessage>>(jsonContent) ?? new List<LogMessage>();
                logMessages.Add(ExceptionData);
                string? json = JsonConvert.SerializeObject(logMessages, Newtonsoft.Json.Formatting.Indented);
                await File.WriteAllTextAsync(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

        }
        private async Task LogWarning(LogMessage ExceptionData)
        {
            int Counter = 1;
            if (!Directory.Exists($"{FullPath}/warning"))
            {
                Directory.CreateDirectory($"{FullPath}/warning");
            }
            if (File.Exists($"{FullPath}/warning/counter.txt"))
            {
                Counter = int.Parse(File.ReadAllText($"{FullPath}/warning/counter.txt"));
            }
            else
            {
                File.WriteAllText($"{FullPath}/warning/counter.txt", Counter.ToString());
            }
            string currentfile = $"WARNING_{Counter}.json";
            string filePath = Path.Combine($"{FullPath}/warning/", currentfile);

            try
            {
                List<LogMessage> logMessages = new List<LogMessage>();
                string? jsonContent = "";

                if (File.Exists(filePath))
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    long maxsize = fileInfo.Length;
                    if (maxsize < MAXFILESIZE)
                    {
                        jsonContent = File.ReadAllText(filePath);

                    }
                    else
                    {
                        Counter++;
                        File.WriteAllText($"{FullPath}/warning/counter.txt", Counter.ToString());
                        currentfile = $"WARNING_{Counter}.json";
                        filePath = Path.Combine($"{FullPath}/warning", currentfile);
                        jsonContent = File.ReadAllText(filePath);

                    }

                }

                logMessages = JsonConvert.DeserializeObject<List<LogMessage>>(jsonContent) ?? new List<LogMessage>();
                logMessages.Add(ExceptionData);
                string? json = JsonConvert.SerializeObject(logMessages, Newtonsoft.Json.Formatting.Indented);
                await File.WriteAllTextAsync(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

        }
        public static int GetDirectoryCount(LogType logType)
        {
            DirectoryInfo dir = new System.IO.DirectoryInfo($"{FullPath}/{logType.ToString().ToLower()}");
            int count;
            try
            {
              count  = dir.GetFiles().Length;
            }
            catch
            {
                count = 0;
            }
            return count;
        }

    }
}