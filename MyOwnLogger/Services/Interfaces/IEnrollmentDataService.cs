using System;
using SharedLibrary;

namespace MyOwnLogger.Services
{
	public interface IEnrollmentDataService
	{
        Task<IEnumerable<Enrollment>> GetEnrollment();
        Task<Enrollment> GetEnrollmentById(int SubjectId, int StudentId);
        Task AddEnrollment(Enrollment enrollment);
        Task UpdateEnrollment(int id,Enrollment enrollment);
        Task DeleteEnrollment(int Subjectid, int StudentId);
    }
}

