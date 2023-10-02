using System;
using SharedLibrary;

namespace APII.Model
{
	public interface IStudentRepositry
	{

        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
    }
}

