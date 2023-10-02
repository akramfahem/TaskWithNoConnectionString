using System;
using SharedLibrary;
namespace APII.Model
{
	public interface IDoctorRepositry
	{
		Task<IEnumerable<Doctor>> GetDoctors();
        Task<Doctor> GetDoctorById(int id);
		Task AddDoctor(Doctor doctor);
		Task UpdateDoctor(Doctor doctor);
		Task DeleteDoctor(int id);
    }
}

