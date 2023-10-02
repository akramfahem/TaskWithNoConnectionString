using System;
using SharedLibrary;

namespace APII.Model
{
	public interface IUniversityRepositry
	{
        Task<IEnumerable<University>> GetAllAsync();
        Task<University?> GetByIdAsync(int id);
        Task AddAsync(University university);
        Task UpdateAsync(University university);
        Task DeleteAsync(int id);
    }
}

