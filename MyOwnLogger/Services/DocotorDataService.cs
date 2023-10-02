using System;
using SharedLibrary;
using System.Text.Json;
namespace MyOwnLogger.Services
{
	public class DoctorDataService:IDoctorDataService
	{
        private readonly HttpClient httpClient;

        public DoctorDataService(HttpClient httpClient)
		{
            this.httpClient = httpClient;
        }

        public async Task AddDoctor(DoctorDTO doctor)
        {
            await httpClient.PostAsJsonAsync("api/Doctor", doctor);
        }

        public async Task DeleteDoctor(int id)
        {
            await httpClient.DeleteAsync($"api/Doctor/{id}");
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            Stream doctorsStream = await httpClient.GetStreamAsync("api/Doctor");
            IEnumerable<Doctor> doctorsObject = await JsonSerializer.DeserializeAsync<IEnumerable<Doctor>>(doctorsStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new List<Doctor>();
            return doctorsObject;
        }

        public async Task<Doctor> GetDoctorById(int id)
        {
            Stream doctorStream = await httpClient.GetStreamAsync($"api/Doctor/{id}");
            Doctor doctorObject = await JsonSerializer.DeserializeAsync<Doctor>(doctorStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new Doctor();
            return doctorObject;
        }

        public async Task UpdateDoctor(int id, Doctor doctor)
        {
            await httpClient.PutAsJsonAsync($"api/Doctor/{id}", doctor);
        }
    }
}

