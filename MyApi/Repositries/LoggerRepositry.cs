using Newtonsoft.Json;
using SharedLibrary;
using System.Collections.Concurrent;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APII.Data;

public class LoggerRepositry
{
    private static readonly Lazy<LoggerRepositry> _instance = new Lazy<LoggerRepositry>(() => new LoggerRepositry());
    public static ConcurrentQueue<(string, LogMessage)> logMessages = logMessages = new ConcurrentQueue<(string, LogMessage)>();
    private static readonly object _lock = new();
    private readonly static string LogPath = "/Users/akram/Projects/INNOTask2/MyOwnLogger/MyOwnLogger/log";
    const int MAXFILESIZE = 10240;
    public static LoggerRepositry GetLogger
    {
        get
        {
            return _instance.Value;
        }
    }

    public LoggerRepositry()
    {
        ProcessLogs();
    }

    public async Task Logging(string user, LogType logType, string message)
    {
        LogMessage logMessage = new() { CreatedAt = DateTime.Now, LogType = logType, Message = message };

        logMessages.Enqueue((user, logMessage));

    }

    private async Task ProcessLogs()
    {
        while (true)
        {
            await DequeueLogs();
            await Task.Delay(5000);
        }
    }
    private async Task DequeueLogs()
    {
        while (logMessages.TryDequeue(out var logdata))
        {
            if (logdata.Item2.LogType == LogType.SUCCESS)
            {
                await LogSuccess(logdata.Item1, logdata.Item2);
            }
            if (logdata.Item2.LogType == LogType.WARNING)
            {
                await LogWarning(logdata.Item1, logdata.Item2);
            }
            if (logdata.Item2.LogType == LogType.EXCEPTION)
            {
                await LogException(logdata.Item1, logdata.Item2);
            }

        }
    }


    public async Task<IEnumerable<LogMessage>> GetLogs(string user, LogType logType, int year, int month, int day, int? CurrentPage)
    {
        if (CurrentPage == 0 || CurrentPage is null)
            CurrentPage = 1;
        if (File.Exists($"{LogPath}/{user}/{logType.ToString().ToLower()}/{year}/{month}/{day}/{logType.ToString()}_{CurrentPage}.json"))
        {
            string? content = File.ReadAllText($"{LogPath}/{user}/{logType.ToString().ToLower()}/{year}/{month}/{day}/{logType.ToString()}_{CurrentPage}.json");
            return JsonConvert.DeserializeObject<List<LogMessage>>(content) ?? new List<LogMessage>();
        }
        return new List<LogMessage>();


    }


    private async Task LogSuccess(string user, LogMessage successData)
    {
        string LogSuccessPath = $"{LogPath}/{user}/success/{successData.CreatedAt.Year}/{successData.CreatedAt.Month}/{successData.CreatedAt.Day}";
        string SuccessCounterPath = $"{LogPath}/{user}/success/{successData.CreatedAt.Year}/{successData.CreatedAt.Month}/{successData.CreatedAt.Day}/counter.txt";
        int Counter = 1;
        if (!Directory.Exists(LogSuccessPath))
        {
            Directory.CreateDirectory(LogSuccessPath);
        }
        if (File.Exists(SuccessCounterPath))
        {
            Counter = int.Parse(File.ReadAllText(SuccessCounterPath));
        }
        else
        {
            File.WriteAllText(SuccessCounterPath, Counter.ToString());
        }
        string currentfile = $"SUCCESS_{Counter}.json";
        string filePath = Path.Combine(LogSuccessPath, currentfile);
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
                    File.WriteAllText(SuccessCounterPath, Counter.ToString());
                    currentfile = $"SUCCESS_{Counter}.json";
                    filePath = Path.Combine(LogSuccessPath, currentfile);
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
    private async Task LogException(string user, LogMessage ExceptionData)
    {
        string LogExceptionPath = $"{LogPath}/{user}/exception/{ExceptionData.CreatedAt.Year}/{ExceptionData.CreatedAt.Month}/{ExceptionData.CreatedAt.Day}";
        string ExceptionCounterPath = $"{LogPath}/{user}/exception/{ExceptionData.CreatedAt.Year}/{ExceptionData.CreatedAt.Month}/{ExceptionData.CreatedAt.Day}/counter.txt";

        int Counter = 1;
        if (!Directory.Exists(LogExceptionPath))
        {
            Directory.CreateDirectory(LogExceptionPath);
        }
        if (File.Exists(ExceptionCounterPath))
        {
            Counter = int.Parse(File.ReadAllText(ExceptionCounterPath));
        }
        else
        {
            File.WriteAllText(ExceptionCounterPath, Counter.ToString());
        }
        string currentfile = $"EXCEPTION_{Counter}.json";
        string filePath = Path.Combine(LogExceptionPath, currentfile);
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
                    File.WriteAllText(ExceptionCounterPath, Counter.ToString());
                    currentfile = $"EXCEPTION_{Counter}.json";
                    filePath = Path.Combine(LogExceptionPath, currentfile);
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
    private async Task LogWarning(string user, LogMessage WarningData)
    {
        string LogWarningPath = $"{LogPath}/{user}/warning/{WarningData.CreatedAt.Year}/{WarningData.CreatedAt.Month}/{WarningData.CreatedAt.Day}";
        string WarningCounterPath = $"{LogPath}/{user}/warning/{WarningData.CreatedAt.Year}/{WarningData.CreatedAt.Month}/{WarningData.CreatedAt.Day}/counter.txt";
        int Counter = 1;
        if (!Directory.Exists(LogWarningPath))
        {
            Directory.CreateDirectory(LogWarningPath);
        }
        if (File.Exists(WarningCounterPath))
        {
            Counter = int.Parse(File.ReadAllText(WarningCounterPath));
        }
        else
        {
            File.WriteAllText(WarningCounterPath, Counter.ToString());
        }
        string currentfile = $"WARNING_{Counter}.json";
        string filePath = Path.Combine(LogWarningPath, currentfile);

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
                    File.WriteAllText(WarningCounterPath, Counter.ToString());
                    currentfile = $"WARNING_{Counter}.json";
                    filePath = Path.Combine(LogWarningPath, currentfile);
                    jsonContent = File.ReadAllText(filePath);

                }

            }

            logMessages = JsonConvert.DeserializeObject<List<LogMessage>>(jsonContent) ?? new List<LogMessage>();
            logMessages.Add(WarningData);
            string? json = JsonConvert.SerializeObject(logMessages, Newtonsoft.Json.Formatting.Indented);
            await File.WriteAllTextAsync(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }

    }
    public int GetDirectoryCount(string user, LogType logType, int year, int month, int day)
    {
        DirectoryInfo dir = new System.IO.DirectoryInfo($"{LogPath}/{user}/{logType.ToString().ToLower()}/{year}/{month}/{day}");
        int count;
        try
        {
            count = dir.GetFiles().Length;
        }
        catch
        {
            count = 0;
        }
        return count;
    }
    public (DateTime from, DateTime To) GetDate(string user, LogType logType)
    {
        string YeardirectoryPath = $"{LogPath}/{user}/{logType.ToString().ToLower()}";
        int year = 0;
        int month = 0;
        int day = 0;
        if (Directory.Exists(YeardirectoryPath))
        {
            DirectoryInfo YearDirectory = new DirectoryInfo(YeardirectoryPath);

            DirectoryInfo[] YearSubdirectories = YearDirectory.GetDirectories();
            string firstYearSubdirectoryName = "";

            if (YearSubdirectories.Length > 0)
            {
                firstYearSubdirectoryName = YearSubdirectories[0].Name;
                year = Int32.Parse(firstYearSubdirectoryName);
            }
            string MonthDirectoryPath = $"{LogPath}/{user}/{logType.ToString().ToLower()}/{firstYearSubdirectoryName}";
            if (Directory.Exists(MonthDirectoryPath))
            {
                DirectoryInfo MonthDirectory = new DirectoryInfo(MonthDirectoryPath);

                DirectoryInfo[] MonthSubdirectories = MonthDirectory.GetDirectories();
                string firstMonthSubdirectoryName = "";
                if (MonthSubdirectories.Length > 0)
                {
                    firstMonthSubdirectoryName = MonthSubdirectories[0].Name;
                    month = Int32.Parse(firstMonthSubdirectoryName);
                }

                string dayDirectoryPath = $"{LogPath}/{user}/{logType.ToString().ToLower()}/{firstYearSubdirectoryName}/{firstMonthSubdirectoryName}";
                if (Directory.Exists(dayDirectoryPath))
                {
                    DirectoryInfo DayDirectoryInfo = new DirectoryInfo(dayDirectoryPath);

                    DirectoryInfo[] DaySubdirectories = DayDirectoryInfo.GetDirectories();
                    string firstDaySubdirectoryName = "";
                    if (DaySubdirectories.Length > 0)
                    {
                        firstDaySubdirectoryName = DaySubdirectories[0].Name;
                        day = Int32.Parse(firstDaySubdirectoryName);
                    }
                }
            }
        }
        string LastYeardirectoryPath = $"{LogPath}/{user}/{logType.ToString().ToLower()}";
        int LastYear = 0;
        int LastMonth = 0;
        int LastDay = 0;
        if (Directory.Exists(LastYeardirectoryPath))
        {
            DirectoryInfo LastYearDirectory = new DirectoryInfo(YeardirectoryPath);

            DirectoryInfo[] LastYearSubdirectories = LastYearDirectory.GetDirectories();
            string LastYearSubdirectoryName = "";

            if (LastYearSubdirectories.Length > 0)
            {
                LastYearSubdirectoryName = LastYearSubdirectories[LastYearSubdirectories.Length - 1].Name;
                LastYear = Int32.Parse(LastYearSubdirectoryName);
            }
            string LastMonthDirectoryPath = $"{LogPath}/{user}/{logType.ToString().ToLower()}/{LastYearSubdirectoryName}";
            if (Directory.Exists(LastMonthDirectoryPath))
            {
                DirectoryInfo LastMonthDirectory = new DirectoryInfo(LastMonthDirectoryPath);

                DirectoryInfo[] LastMonthSubdirectories = LastMonthDirectory.GetDirectories();
                string LastMonthSubdirectoryName = "";
                if (LastMonthSubdirectories.Length > 0)
                {
                    LastMonthSubdirectoryName = LastMonthSubdirectories[LastMonthSubdirectories.Length - 1].Name;
                    LastMonth = Int32.Parse(LastMonthSubdirectoryName);
                }

                string lastdayDirectoryPath = $"{LogPath}/{user}/{logType.ToString().ToLower()}/{LastYearSubdirectoryName}/{LastMonthSubdirectoryName}";
                if (Directory.Exists(lastdayDirectoryPath))
                {
                    DirectoryInfo LastDayDirectoryInfo = new DirectoryInfo(lastdayDirectoryPath);

                    DirectoryInfo[] LastDaySubdirectories = LastDayDirectoryInfo.GetDirectories();
                    string lastDaySubdirectoryName = "";
                    if (LastDaySubdirectories.Length > 0)
                    {
                        lastDaySubdirectoryName = LastDaySubdirectories[LastDaySubdirectories.Length - 1].Name;
                        LastDay = Int32.Parse(lastDaySubdirectoryName);
                    }
                }
            }


        }
        return (new DateTime(year, month, day), new DateTime(LastYear, LastMonth, LastDay));

    }
    public List<string> GetUsers()
    {
        List<string> users = new List<string>();
        if (Directory.Exists(LogPath))
        {
            DirectoryInfo LogDirectoryInfo = new DirectoryInfo(LogPath);

            DirectoryInfo[] LogSubDirectories = LogDirectoryInfo.GetDirectories();
            foreach (DirectoryInfo user in LogSubDirectories)
            {
                users.Add(user.Name);
            }
        }
        return users;
    }
}