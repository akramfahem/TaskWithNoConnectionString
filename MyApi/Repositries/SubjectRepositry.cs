using System;
using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using System.Text.Json;
namespace APII.Model
{
	public class SubjectRepositry:ISubjectRepositry
	{
        public Logger Logger = Logger.GetLogger;
        private readonly AppDbContext appDbContext;
		public SubjectRepositry(AppDbContext appDbContext)
		{
            this.appDbContext = appDbContext;
        }
       
        public async Task<IEnumerable<Subject>> GetSubjects()
        {
            //await Logger.Logging(new LogMessage { Message = "All Subjects has been Retrieved From DataBase", CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            return await appDbContext.Subjects.Include(s=>s.Doctor).Include(s=>s.Students).ToListAsync();
        }

        public async Task<Subject> GetSubjectById(int id)
        {
            var subject = await appDbContext.Subjects.Include(s => s.Doctor).Include(s => s.Students).Include(c=>c.College).FirstOrDefaultAsync(s=>s.Id == id);
            if (subject is not null)
            {
                //await Logger.Logging(new LogMessage { Message = $"Subject With Id {id} has been Retrieved from DataBase", CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            }
            else
            {
                //await Logger.Logging(new LogMessage { Message = $"Tried to retrieve non existent Subject", CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }

            return subject;
        }

        public async Task AddSubject(Subject subject)
        {
            await appDbContext.Subjects.AddAsync(subject);
            try
            {
                await appDbContext.SaveChangesAsync();
                string SubjectJson = JsonSerializer.Serialize(subject);
                //await Logger.Logging(new LogMessage { Message = "Student has been added :" + SubjectJson, CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            }
            catch (Exception ex)
            {
                //await Logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
        }

        public async Task DeleteSubject(int id)
        {
            var subject = await GetSubjectById(id);
            if (subject is null)
            {
                //await Logger.Logging(new LogMessage { Message = "Null Reference Exception tried to Delete non existent Subject", CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
            else
            {
                appDbContext.Subjects.Remove(subject);
                try
                {
                    await appDbContext.SaveChangesAsync();
                    //await Logger.Logging(new LogMessage { Message = $"Subject with id : {id} has been deleted:", LogType = LogType.SUCCESS, CreatedAt = DateTime.Now });
                }
                catch (Exception ex)
                {
                    //await Logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
                }
            }
            
        }

        public async Task UpdateSubject(Subject subject)
        {
            appDbContext.Entry(subject).State = EntityState.Modified;
            try
            {
                await appDbContext.SaveChangesAsync();
                //await Logger.Logging(new LogMessage { Message = $"Subject With Id {subject.Id} Has been Updated ", CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            }
            catch (Exception ex)
            {
                //await Logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
        }
    }
}

