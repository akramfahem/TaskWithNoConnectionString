using System;
using System.Text.Json;
using SharedLibrary;

namespace MyOwnLogger.Services
{
	public class CollegeDataService:ICollegeDataService
	{
        private readonly HttpClient httpClient;
        public CollegeDataService(HttpClient httpClient)
		{
            this.httpClient = httpClient;
        }

        public async Task AddCollege(CollegeDTO collegeDTO)
        {
            await httpClient.PostAsJsonAsync("api/College", collegeDTO);
        }

        public async Task DeleteCollege(int id)
        {
            await httpClient.DeleteAsync($"api/College/{id}");
        }

        public async Task<College> GetCollegeById(int id)
        {
            Stream collegeStream = await httpClient.GetStreamAsync($"api/College/{id}");
            College? collegeObject = await JsonSerializer.DeserializeAsync<College>(collegeStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new College();
            return collegeObject;
        }

        public async Task<IEnumerable<College>> GetColleges()
        {
            Stream collegesStream = await httpClient.GetStreamAsync("api/College");
            IEnumerable<College> colleges = await JsonSerializer.DeserializeAsync<IEnumerable<College>>(collegesStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new List<College>();
            return colleges;
        }

        public async Task UpdateCollege(int id, College college)
        {
            await httpClient.PutAsJsonAsync($"api/College/{id}", college);
        }
    }
}

