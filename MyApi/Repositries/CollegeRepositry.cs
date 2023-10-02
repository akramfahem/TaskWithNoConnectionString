using System;
using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using AutoMapper;
using System.Text.Json;

namespace APII.Model
{
    public class CollegeRepositry : ICollegeRepositry
    {
        private readonly AppDbContext appDbContext;
        public Logger Logger = Logger.GetLogger;
        public CollegeRepositry(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IEnumerable<College>> GetCollegesAsync()
        {
            //await Logger.Logging(new LogMessage { Message = "All College has been Retrieved From DataBase", CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            var Colleges = await appDbContext.Colleges.Include(x => x.University).Include(x => x.Students).Include(x => x.Subjects).ThenInclude(d => d.Doctor).Include(x => x.Doctors).ToListAsync();
            return Colleges;
        }

        public async Task<College> GetCollegeByIdAsync(int id)
        {
            var college = await appDbContext.Colleges.Include(x => x.University).Include(x => x.Students).Include(d=>d.Doctors).Include(s=>s.Subjects).ThenInclude(x=>x.Doctor).FirstOrDefaultAsync(c => c.Id == id);
            if (college is not null)
            {
                //await Logger.Logging(new LogMessage { Message = $"college With Id {id} has been Retrieved from DataBase", CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            }
            else
            {
                //await Logger.Logging(new LogMessage { Message = $"Tried to retrieve non existent college", CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
            return college;
        }
        public async Task DeleteCollegeAsync(int id)
        {
            var college = await GetCollegeByIdAsync(id);
            if (college is null)
            {
                //await Logger.Logging(new LogMessage { Message = "Null Reference Exception tried to Delete non existent college", CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
            if (college != null)
            {
                appDbContext.Colleges.Remove(college);
                try
                {
                    await  appDbContext.SaveChangesAsync();

                    //await Logger.Logging(new LogMessage { Message = $"college with id : {id} has been deleted:", LogType = LogType.SUCCESS, CreatedAt = DateTime.Now });
                }
                catch (Exception ex)
                {
                    //await Logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
                }
            }
        }


        public async Task UpdateCollegeAsync(College college)
        {
            appDbContext.Entry(college).State = EntityState.Modified;
            try
            {
                await appDbContext.SaveChangesAsync();
                //await Logger.Logging(new LogMessage { Message = $"College With Id {college.Id} Has been Updated ", CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            }
            catch (Exception ex)
            {
                //await Logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
        }

        public async Task AddCollegeAsync(College college)
        {
            await appDbContext.Colleges.AddAsync(college);
            try
            {
                await appDbContext.SaveChangesAsync();
                string CollegeJson = JsonSerializer.Serialize(college);
                //await Logger.Logging(new LogMessage { Message = "College has been added :" + CollegeJson, CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            }
            catch (Exception ex)
            {
                //await Logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
        }
    }

}

