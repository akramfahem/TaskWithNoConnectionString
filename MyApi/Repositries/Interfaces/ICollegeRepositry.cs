using System;
using SharedLibrary;
namespace APII.Model
{
	public interface ICollegeRepositry
	{
		Task<IEnumerable<College>> GetCollegesAsync();
        Task<College> GetCollegeByIdAsync(int id);
		Task AddCollegeAsync(College college);
		Task UpdateCollegeAsync(College college);
		Task DeleteCollegeAsync(int id);
    }
}

