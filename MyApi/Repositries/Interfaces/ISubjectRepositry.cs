using System;
using SharedLibrary;
namespace APII.Model
{
	public interface ISubjectRepositry
	{
		Task<IEnumerable<Subject>> GetSubjects();
		Task<Subject> GetSubjectById(int id);
		Task AddSubject(Subject subject);
		Task UpdateSubject(Subject subject);
		Task DeleteSubject(int id);
    }
}

