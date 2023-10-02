using System;
using SharedLibrary;
namespace APII.Model
{
	public interface iEnrollmentRepositry
	{
		Task<IEnumerable<Enrollment>> GetEnrollments();
		Task<Enrollment> GetEnrollmentById(int SubjectId, int StudentId);
        Task AddEnrollment(Enrollment enrollment);
		Task UpdateEnrollment(Enrollment enrollment);
		Task DeleteEnrollment(int Subjectid,int StudentId);
    }
}

