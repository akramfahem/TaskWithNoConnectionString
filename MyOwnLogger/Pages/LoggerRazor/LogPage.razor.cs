using System;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using MyOwnLogger.Services;
using SharedLibrary;

namespace MyOwnLogger.Pages.LoggerRazor
{
    public partial class LogPage
    {
        public string user { get; set; }
        public int CurrentPage { get; set; } = 1;
        public LogType logType { get; set; }
        public List<List<LogMessage>> ShowedlogMessages { get; set; } = new();
        public List<List<List<LogMessage>>> logMessages { get; set; } = new();
        public int TotalPages { get; set; }
        private bool loadingLogs = false;
        public DateTime? selectedStartDate { get; set; }
        public DateTime? selectedEndDate { get; set; }
        public DateTime MinimumDate { get; set; }
        public DateTime MaxDate { get; set; }
        public List<string> users { get; set; } = new();
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Inject]
        public ILoggerDataService loggerDataService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            users = await loggerDataService.GetUsers();
            await base.OnInitializedAsync();
        }
        public async Task UpdateFilteredDataAsync(ChangeEventArgs CurrentLogType)
        {

            if (CurrentLogType != null && Enum.TryParse<LogType>(CurrentLogType.Value?.ToString(), out var logtype))
            {
                logType = logtype;
                await HandleDate();
            }
        }
        public async Task UpdateUser(ChangeEventArgs CurrentUser)
        {
            if (CurrentUser != null)
            {
                user = CurrentUser.Value?.ToString();
                HandleDate();
            }
        }
        private async Task PreviousPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
            }
            ShowedlogMessages = logMessages[CurrentPage - 1];
          
        }
        private async Task NextPage()
        {
            if (CurrentPage < logMessages.Count)
            {
                CurrentPage++;
            }
            ShowedlogMessages = logMessages[CurrentPage - 1];
            
        }
        private async Task GetData()
        {

            if (selectedStartDate.HasValue && selectedEndDate.HasValue)
            {
                logMessages = new();
                int currentcounter = 0;

                for (DateTime? date = selectedStartDate; date <= selectedEndDate; date = date.Value.AddDays(1))
                {
                    logMessages.Add(new());
                    TotalPages = await loggerDataService.GetDirectoryCount(user, logType, date.Value.Year, date.Value.Month, date.Value.Day);
                    for (int Page = 1; Page <= TotalPages-1; Page++)
                    {
                        logMessages[currentcounter].Add((List<LogMessage>)await loggerDataService.Get(user, selectedStartDate.Value.Year, selectedStartDate.Value.Month, selectedStartDate.Value.Day, logType, Page));
                    }
                    currentcounter++;
                }
                if(logMessages.Any())
                    ShowedlogMessages = logMessages[0];
               
            }
        }
        private async Task ResetFilteration()
        {
            selectedStartDate = null;
            selectedEndDate = null;
            CurrentPage = 1;
        }
        private async Task HandleDate()
        {
            (DateTime, DateTime) Date = await loggerDataService.GetDate(user, logType);
        MinimumDate = Date.Item1;
        MaxDate = Date.Item2;
            await InvokeAsync(StateHasChanged);
        }
}
}

