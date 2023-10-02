using System;
using SharedLibrary;

namespace MyOwnLogger.Services
{
	public interface IUniversityDataService
	{
        Task<IEnumerable<University>> GetUniversity();
        Task<University> GetUniversityById(int id);
        Task AddUniversity(UniversityDTO universityDTO);
        Task UpdateUniversity(int id , University university);
        Task DeleteUniversity(int id);
    }
}

