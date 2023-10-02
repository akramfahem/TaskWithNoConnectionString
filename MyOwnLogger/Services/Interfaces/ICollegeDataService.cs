using System;
using SharedLibrary;
namespace MyOwnLogger.Services
{
	public interface ICollegeDataService
	{
		Task<IEnumerable<College>> GetColleges();
		Task<College> GetCollegeById(int id);
		Task AddCollege(CollegeDTO collegeDTO);
		Task UpdateCollege(int id, College college);
		Task DeleteCollege(int id);
	}
}

