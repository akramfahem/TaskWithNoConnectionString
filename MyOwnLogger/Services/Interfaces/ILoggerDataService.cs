using Microsoft.AspNetCore.Mvc;
using SharedLibrary;
namespace MyOwnLogger.Services
{
	public interface ILoggerDataService
	{
		public Task<IEnumerable<LogMessage>> Get(string user,int year,int month,int day ,LogType logType, int CurrentPage);
		public Task Add(string user,LogMessage logMessage);
		public Task<(DateTime From, DateTime To)> GetDate(string user, LogType logType);
		public Task<int> GetDirectoryCount(string user, LogType logType, int year, int month, int day);
		public Task<List<string>> GetUsers();

    }
}

