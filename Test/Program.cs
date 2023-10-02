namespace Test;
using SharedLibrary;
class Program
{
    public static async Task Main(string[] args)
    {
        //var s = Console.ReadLine();
        //Console.WriteLine($"Hello, {s}");
        //LogMessage logmessage = new LogMessage { CreatedAt = DateTime.Now, LogType = LogType.SUCCESS, Message = "Created Succesfully at first" };
        //LogMessage logmessage1 = new LogMessage { CreatedAt = DateTime.Now, LogType = LogType.SUCCESS, Message = "Created Succesfully at second " };
        //var log = Logger.GetLogger;

        //Task task1 = Task.Run(async () =>
        //{
        //    for (int j = 0; j < 100; j++)
        //        await log.Logging(logmessage);
        //});
        //Task task2 = Task.Run(async () =>
        //{
        //    for (int i = 0; i < 100; i++)
        //        await log.Logging(logmessage1);
        //});
        //await Task.WhenAll(task1, task2);
        //Console.WriteLine("All tasks completed");
        LogType logType = LogType.SUCCESS;
        DirectoryInfo dir = new System.IO.DirectoryInfo($"/Users/akram/Projects/INNOTask2/MyOwnLogger/MyOwnLogger/log/{logType.ToString().ToLower()}");
        Console.WriteLine($"/Users/akram/Projects/INNOTask2/MyOwnLogger/MyOwnLogger/log/{logType.ToString().ToLower()}/{logType.ToString()}_{1}");
        int count = dir.GetFiles().Length;
        Console.WriteLine(count);
        Console.ReadKey();
    }
}

