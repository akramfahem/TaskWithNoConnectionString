using System;
using SharedLibrary;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace MyOwnLogger.Services
{
	public class LoggerDataService: ILoggerDataService
	{
        private readonly HttpClient httpClient;

        public LoggerDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task Add(string user, LogMessage logMessage)
        {
           await httpClient.PostAsJsonAsync($"api/Logger/{user}",logMessage);
        }

        public async Task<IEnumerable<LogMessage>> Get(string user,int year,int month,int day,LogType logType, int CurrentPage)
        {
           Stream JsonLogs = await httpClient.GetStreamAsync($"api/Logger/{user}/{year}/{month}/{day}/{logType.ToString()}/{CurrentPage}");
            IEnumerable<LogMessage> LoggerObjects = await JsonSerializer.DeserializeAsync<IEnumerable<LogMessage>>(JsonLogs, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new List<LogMessage>();
            return LoggerObjects;
        }

        public async Task<(DateTime From, DateTime To)> GetDate(string user, LogType logType)
        {
            Stream JsonLogs = await httpClient.GetStreamAsync($"api/Logger/{user}/{logType.ToString().ToLower()}");
            DateTuple DateObject = new();
            DateObject = await JsonSerializer.DeserializeAsync<DateTuple>(JsonLogs, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return (DateObject.Item1, DateObject.Item2);
        }

        public async Task<int> GetDirectoryCount(string user, LogType logType, int year, int month, int day)
        {
            Stream JsonPages = await httpClient.GetStreamAsync($"api/Logger/{user}/{year}/{month}/{day}/{logType.ToString()}");
            int TotalPages = await JsonSerializer.DeserializeAsync<int>(JsonPages, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return TotalPages;
        }

        public async Task<List<string>> GetUsers()
        {
            Stream JsonUsers = await httpClient.GetStreamAsync($"api/Logger");
            List<string> Users = await JsonSerializer.DeserializeAsync< List<string>>(JsonUsers, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new List<string>();
            return Users;
        }

        public class DateTuple
        {
            public DateTime Item1 { get; set; }
            public DateTime Item2 { get; set; }
        }
    }
}

