using System;
using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using System.Text.Json;
namespace APII.Model
{
    public class EnrollmentRepositry : iEnrollmentRepositry
    {
        private readonly AppDbContext appDbContext;
        public Logger logger = Logger.GetLogger;
        public EnrollmentRepositry(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task AddEnrollment(Enrollment enrollment)
        {
            await appDbContext.Enrollments.AddAsync(enrollment);
            try
            {
                await appDbContext.SaveChangesAsync();
                var EnrollmentJson = JsonSerializer.Serialize(enrollment);
                //await logger.Logging(new LogMessage { Message = $"New Enrollment has been added {EnrollmentJson}",CreatedAt = DateTime.Now,LogType = LogType.SUCCESS});
            }
             catch (Exception ex)
            {
                //await logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
        }

        public async Task DeleteEnrollment(int SubjectId,int StudentId)
        {
            Enrollment enrollment = appDbContext.Enrollments.FirstOrDefault(x => x.SubjectId == SubjectId && x.StudentId == StudentId) ?? new Enrollment();
            if (enrollment is null)
            {
                //await logger.Logging(new LogMessage { Message = "Null Reference Exception tried to Delete non existent student", CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
            else
            {
                appDbContext.Enrollments.Remove(enrollment);
                try
                {
                    await appDbContext.SaveChangesAsync();
                    //await logger.Logging(new LogMessage { Message = $"Enrollment with id : {enrollment.Id} has been deleted:", LogType = LogType.SUCCESS, CreatedAt = DateTime.Now });
                }
                catch (Exception ex)
                {
                    //await logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
                }
            }
            
        }

        public async Task<Enrollment> GetEnrollmentById(int SubjectId,int StudentId)
        {
            var enrollment = await appDbContext.Enrollments.FirstOrDefaultAsync(e => e.SubjectId == SubjectId && e.StudentId == StudentId);
            if(enrollment is not null)
            {
                //await logger.Logging(new LogMessage { Message = $"Enrollment With Id {enrollment.Id} has been Retrieved from DataBase", CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            }
            else
            {
                //await logger.Logging(new LogMessage { Message = $"Tried to Enroll Twice in same Enrollment", CreatedAt = DateTime.Now, LogType = LogType.WARNING });
            }
            return enrollment ?? new Enrollment(); 
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollments()
        {
            //await logger.Logging(new LogMessage { Message = "All Enrollments has been Retrieved From DataBase", CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            return await appDbContext.Enrollments.ToListAsync();
        }

        public async Task UpdateEnrollment(Enrollment enrollment)
        {
            appDbContext.Entry(enrollment).State = EntityState.Modified;
            try
            { 
                await appDbContext.SaveChangesAsync();
                //await logger.Logging(new LogMessage { Message = $"Enrollment With Id {enrollment.Id} Has been Updated ", CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            }
            catch (Exception ex)
            {
                //await logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
        }
    }
}

