using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using SharedLibrary;

namespace MyOwnLogger.Services
{
    public class StudentDataService : IStudentDataService
    {
        private readonly HttpClient httpClient;
        public StudentDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task AddStudent(StudentDTO student)
        {
            var studentobj = new StringContent(JsonSerializer.Serialize(student),Encoding.UTF8,"application/json");
           
            await httpClient.PostAsync("api/Student", studentobj);
           
        }

        public async Task DeleteStudent(int id)
        {
            await httpClient.DeleteAsync($"api/Student/{id}");

        }

        public async Task<Student> GetStudentById(int id)
        {

            var studentobj = await httpClient.GetStreamAsync($"api/Student/{id}");
            if(studentobj is not null)
            {
                var student =await JsonSerializer.DeserializeAsync<Student>
                (studentobj, new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true }) ?? new Student();
                return student;
            }
            return new Student();

            
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            var students = await JsonSerializer.DeserializeAsync<IEnumerable<Student>>
                           (await httpClient.GetStreamAsync("api/Student"), new JsonSerializerOptions()
                           { PropertyNameCaseInsensitive = true }) ?? new List<Student>();

            return students;
          
        }

        public async Task UpdateStudent(int id, Student student)
        {
            await httpClient.PutAsJsonAsync($"api/Student/{id}", student);
        }
    }
}

