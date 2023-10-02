using System;
using SharedLibrary;
using System.Text.Json;
namespace MyOwnLogger.Services
{
	public class EnrollmentDataService:IEnrollmentDataService
	{
        private readonly HttpClient httpClient;

        public EnrollmentDataService(HttpClient httpClient)
		{
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<Enrollment>> GetEnrollment()
        {
            Stream enrollmentsStream = await httpClient.GetStreamAsync("api/Enrollment");
            IEnumerable<Enrollment> enrollmentsObject = await JsonSerializer.DeserializeAsync<IEnumerable<Enrollment>>(enrollmentsStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new List<Enrollment>();
            return enrollmentsObject;
        }

        public async Task<Enrollment> GetEnrollmentById(int SubjectId,int StudentId)
        {
            Stream enrollmentStream = await httpClient.GetStreamAsync($"api/Enrollment/{SubjectId}/{StudentId}");
            Enrollment enrollmentObject = await JsonSerializer.DeserializeAsync<Enrollment>(enrollmentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new Enrollment();
            return enrollmentObject;
        }

        public async Task AddEnrollment(Enrollment enrollment)
        {
            await httpClient.PostAsJsonAsync("api/Enrollment", enrollment);
        }

        public async Task DeleteEnrollment(int SubjectId, int StudentId)
        {
            await httpClient.DeleteAsync($"api/Enrollment/{SubjectId}/{StudentId}");
        }

        public async Task UpdateEnrollment(int id, Enrollment enrollment)
        {
            await httpClient.PutAsJsonAsync($"api/Enrollment/{id}", enrollment);        
        }
    }
}

