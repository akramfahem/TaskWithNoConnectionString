using System;
using System.Net.Http.Json;
using System.Text.Json;
using SharedLibrary;
namespace MyOwnLogger.Services
{
	public class UniversityDataService:IUniversityDataService
	{
		private readonly HttpClient httpClient;
		public UniversityDataService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
        }

        public async Task AddUniversity(UniversityDTO universityDTO)
        {
            await httpClient.PostAsJsonAsync("api/University",universityDTO);
        }

        public async Task DeleteUniversity(int id)
        {
            await httpClient.DeleteAsync($"api/University/{id}");
        }

        public async Task<IEnumerable<University>> GetUniversity()
        { 
            Stream Universities = await httpClient.GetStreamAsync("api/University");
            IEnumerable<University>? universities = await JsonSerializer.DeserializeAsync<IEnumerable<University>>(Universities, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new List<University>();

            return universities;
        }

        public async Task<University> GetUniversityById(int id)
        {
            Stream universitystream = await httpClient.GetStreamAsync($"api/University/{id}");
            University universityobj = await JsonSerializer.DeserializeAsync<University>(universitystream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })?? new University();
            return universityobj;
        }

        public async Task UpdateUniversity(int id ,University university)
        {
            await httpClient.PutAsJsonAsync($"api/University/{id}",university);
        }
    }
}

