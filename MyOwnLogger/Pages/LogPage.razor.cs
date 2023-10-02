using System;
using Newtonsoft.Json;
using System.Net.Http.Json;
using SharedLibrary;
using Microsoft.AspNetCore.Components;

namespace MyOwnLogger.Pages
{
    public partial class LogPage
    {
        public int CurrentPage { get; set; } = 1;
        public LogType logType { get; set; }
        public List<LogMessage> data { get; set; }
        public int TotalPages { get; set; }
        private bool loadingLogs = false;
        private DateTime? selectedStartDate;
        private DateTime? selectedEndDate;
        [Inject]
        NavigationManager navigationManager { get; set; }
        protected override Task OnInitializedAsync()
        {
          
            TotalPages = Logger.GetDirectoryCount(logType) - 1;
            if (TotalPages < 1)
            {
                data = new List<LogMessage>();
            }
            else
            {
                var content = File.ReadAllText($"/Users/akram/Projects/INNOTask2/MyOwnLogger/MyOwnLogger/log/{logType.ToString().ToLower()}/{logType.ToString()}_{CurrentPage}.json");
                data = JsonConvert.DeserializeObject<List<LogMessage>>(content) ?? new List<LogMessage>();
            }

            return base.OnInitializedAsync();
        }
        public void UpdateFilteredData(ChangeEventArgs e)
        {
            if (e != null && Enum.TryParse<LogType>(e.Value?.ToString(), out var logtype))
            {
                logType = logtype;
                TotalPages = Logger.GetDirectoryCount(logtype) -1;
            }
            if (TotalPages < 1)
            {
                data = new List<LogMessage>();
            }
            else
            {
                var content = File.ReadAllText($"/Users/akram/Projects/INNOTask2/MyOwnLogger/MyOwnLogger/log/{logType.ToString().ToLower()}/{logType.ToString()}_{1}.json");
                data = JsonConvert.DeserializeObject<List<LogMessage>>(content) ?? new List<LogMessage>();
            }
            CurrentPage = 1;
        }
        private void PreviousPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
            }
            var content = File.ReadAllText($"/Users/akram/Projects/INNOTask2/MyOwnLogger/MyOwnLogger/log/{logType.ToString().ToLower()}/{logType.ToString()}_{CurrentPage}.json");
            data = JsonConvert.DeserializeObject<List<LogMessage>>(content) ?? new List<LogMessage>();
        }
        private void NextPage()
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
            }
            var content = File.ReadAllText($"/Users/akram/Projects/INNOTask2/MyOwnLogger/MyOwnLogger/log/{logType.ToString().ToLower()}/{logType.ToString()}_{CurrentPage}.json");
            data = JsonConvert.DeserializeObject<List<LogMessage>>(content) ?? new List<LogMessage>();
        }
        private void FilterData()
        {
            if (selectedStartDate.HasValue && selectedEndDate.HasValue)
            {
                data = data
                    .Where(item => item.CreatedAt.Date >= selectedStartDate.Value.Date && item.CreatedAt.Date <= selectedEndDate.Value.Date)
                    .ToList();
            }
           
        }
    }
}

