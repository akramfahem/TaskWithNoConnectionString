using System;
using SharedLibrary;
namespace MyOwnLogger.Services
{
	public interface IStudentDataService
	{
        Task<IEnumerable<Student>> GetStudents();
		Task<Student> GetStudentById(int id);
		Task AddStudent(StudentDTO student);
		Task UpdateStudent(int id ,Student student);
		Task DeleteStudent(int id);
	}
}

