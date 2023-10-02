using System;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using SharedLibrary;

namespace APII.Model
{
	public class StudentRepositry:IStudentRepositry
	{
        public Logger Logger = Logger.GetLogger;
        private readonly AppDbContext _appDbContext;
        public readonly IDataProtector dataProtector;
        public StudentRepositry(AppDbContext appDbContext, IDataProtector dataProtectionProvider)
		{
            this._appDbContext = appDbContext;
            this.dataProtector = dataProtectionProvider;
        }

        public async Task AddAsync(Student student)
        {
           
            await _appDbContext.AddAsync(student);
            try
            {
                await _appDbContext.SaveChangesAsync();
                string StudentJson = JsonSerializer.Serialize(student);
                //await Logger.Logging(new LogMessage { Message = "Student has been added :" + StudentJson, CreatedAt = DateTime.Now,LogType = LogType.SUCCESS});
            }
            catch (Exception ex)
            {
                //await Logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _appDbContext.Students.FindAsync(id);
            if (student is null)
            {
                //await Logger.Logging(new LogMessage { Message = "Null Reference Exception tried to Delete non existent student",CreatedAt = DateTime.Now,LogType = LogType.EXCEPTION});
            }
            if (student != null)
            {
                _appDbContext.Students.Remove(student);
                try
                {
                    await _appDbContext.SaveChangesAsync();
           
                    //await Logger.Logging(new LogMessage { Message = $"Student with id : {id} has been deleted:",LogType = LogType.SUCCESS, CreatedAt = DateTime.Now });
                }
                catch (Exception ex)
                {
                    //await Logger.Logging(new LogMessage { Message = ex.Message ,CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION});
                }
            }
        }
        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            //await Logger.Logging(new LogMessage { Message = "All Student has been Retrieved From DataBase",CreatedAt = DateTime.Now,LogType = LogType.SUCCESS });
            var students = await _appDbContext.Students.Include(x=>x.College).Include(x=>x.Subjects).ToListAsync();
            
            return students;
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
          
            var student= await _appDbContext.Students.Include(x => x.College).Include(x => x.Subjects).FirstOrDefaultAsync(st => st.Id == id);
            if (student is not null)
            {
                //await Logger.Logging(new LogMessage { Message = $"Student With Id {id} has been Retrieved from DataBase", CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            }
            else
            {
                //await Logger.Logging(new LogMessage { Message = $"Tried to retrieve non existent student", CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
            return student;
        }

        public async Task UpdateAsync(Student student)
        {
           
            _appDbContext.Entry(student).State = EntityState.Modified;
            try
            {
                await _appDbContext.SaveChangesAsync();
                //await Logger.Logging(new LogMessage { Message = $"Student With Id {student.Id} Has been Updated ",CreatedAt = DateTime.Now , LogType = LogType.SUCCESS });
            }
            catch (Exception ex)
            {
                //await Logger.Logging(new LogMessage { Message = ex.Message,CreatedAt = DateTime.Now,LogType = LogType.EXCEPTION });
            }
        }
    }
}

