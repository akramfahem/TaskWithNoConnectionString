using System;
using SharedLibrary;
namespace MyOwnLogger.Services
{
	public interface IDoctorDataService
	{
        Task<IEnumerable<Doctor>> GetDoctors();
        Task<Doctor> GetDoctorById(int id);
        Task AddDoctor(DoctorDTO Doctor);
        Task UpdateDoctor(int id, Doctor doctor);
        Task DeleteDoctor(int id);
    }
}

