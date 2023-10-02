using System;
using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using System.Text;
using System.Text.Json;

namespace APII.Model
{
	public class UniverstiyRepositry:IUniversityRepositry
	{
        public Logger Logger = Logger.GetLogger;

        private readonly AppDbContext appDbContext;
		public UniverstiyRepositry(AppDbContext appDbContext)
		{
            this.appDbContext = appDbContext;
		}
        public async Task<IEnumerable<University>> GetAllAsync()
        {
            //await Logger.Logging(new LogMessage { Message = " all Universities has been retrieved", LogType = LogType.SUCCESS, CreatedAt = DateTime.Now });
            return await appDbContext.Universities.Include(x=>x.Colleges).ToListAsync();
        }

        public async Task<University?> GetByIdAsync(int id)
        {
            

            var university = await appDbContext.Universities.Include(x => x.Colleges).FirstOrDefaultAsync(x=>x.Id == id);
            if(university is not null )
            {
                //await Logger.Logging(new LogMessage { Message = $"University with Id {id} has been retrieved", LogType = LogType.SUCCESS, CreatedAt = DateTime.Now });

            }
            else
            {
                //await Logger.Logging(new LogMessage { Message = $"Tried to retrieve non existent University", CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
            return university;
        }


        public async Task AddAsync(University university)
        {
           
            await appDbContext.Universities.AddAsync(university);
            try
            {
                await appDbContext.SaveChangesAsync();
                var universityJson = JsonSerializer.Serialize(university);
                //await Logger.Logging(new LogMessage { Message = $"Student has been Added:{universityJson} ", CreatedAt = DateTime.Now ,LogType = LogType.SUCCESS});
            }
            catch(Exception ex)
            {
                //await Logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
        }

        public async Task DeleteAsync(int id)
        {
            University? university = await appDbContext.Universities.Include(x => x.Colleges).FirstOrDefaultAsync(x => x.Id == id);
            if(university is null)
            {
                //await Logger.Logging(new LogMessage { Message = "Null Reference Exception tried to Delete non existent student", CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
            else
            { appDbContext.Universities.Remove(university);}
            try
            {
                await appDbContext.SaveChangesAsync();
                //await Logger.Logging(new LogMessage { Message = $"university with id : {id} has been deleted:", LogType = LogType.SUCCESS, CreatedAt = DateTime.Now });
            }
            catch(Exception ex)
            {
                //await Logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
        }

      
        public async Task UpdateAsync(University university)
        {
            appDbContext.Entry(university).State = EntityState.Modified;
            try
            {
                await appDbContext.SaveChangesAsync();
                //await Logger.Logging(new LogMessage { Message = $"university With Id {university.Id} Has been Updated ", CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            }
            catch(Exception ex)
            {
                //await Logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }

        }
    }
}

