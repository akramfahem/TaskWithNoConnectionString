using System;
using System.Text.Json;
using SharedLibrary;

namespace MyOwnLogger.Services
{
	public class SubjectDataService:ISubjectDataService
	{
        private readonly HttpClient httpClient;

        public SubjectDataService(HttpClient httpClient)
		{
            this.httpClient = httpClient;
        }

        public async Task AddSubject(SubjectDTO SubjectDTO)
        {
            await httpClient.PostAsJsonAsync("api/Subject", SubjectDTO);
        }

        public async Task DeleteSubject(int id)
        {
            await httpClient.DeleteAsync($"api/Subject/{id}");
        }

        public async Task<IEnumerable<Subject>> GetSubject()
        {
            Stream subjectStream = await httpClient.GetStreamAsync("api/Subject");
            IEnumerable<Subject>? subjects = await JsonSerializer.DeserializeAsync<IEnumerable<Subject>>(subjectStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new List<Subject>();

            return subjects;
        }

        public async Task<Subject> GetSubjectById(int id)
        {
            Stream Subjectstream = await httpClient.GetStreamAsync($"api/Subject/{id}");
            Subject Subjectobj = await JsonSerializer.DeserializeAsync<Subject>(Subjectstream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new Subject();
            return Subjectobj;
        }

        public async Task UpdateSubject(int id, Subject subject)
        {
            await httpClient.PutAsJsonAsync($"api/Subject/{id}", subject);
        }
    }
}

