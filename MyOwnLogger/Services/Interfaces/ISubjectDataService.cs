using System;
using SharedLibrary;

namespace MyOwnLogger.Services
{
	public interface ISubjectDataService
	{
        Task<IEnumerable<Subject>> GetSubject();
        Task<Subject> GetSubjectById(int id);
        Task AddSubject(SubjectDTO SubjectDTO);
        Task UpdateSubject(int id, Subject doctor);
        Task DeleteSubject(int id);
    }
}

