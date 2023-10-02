using System;
using SharedLibrary;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace APII.Model
{
	public class DoctorRepositry:IDoctorRepositry
	{
        public Logger Logger = Logger.GetLogger;
        private readonly AppDbContext appDbContext;
		public DoctorRepositry(AppDbContext appDbContext)
		{
            this.appDbContext = appDbContext;
		}
        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            //await Logger.Logging(new LogMessage { Message = "All Doctors has been Retrieved From DataBase", CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            return await appDbContext.Doctors.Include(c => c.College).Include(S => S.Subjects).ToListAsync();
            
        }
        public async Task<Doctor> GetDoctorById(int id)
        {
            var doctor = await appDbContext.Doctors.Include(c => c.College).Include(S => S.Subjects).FirstOrDefaultAsync(d => d.Id == id);
            if (doctor is not null)
            {
                //await Logger.Logging(new LogMessage { Message = $"doctor With Id {id} has been Retrieved from DataBase", CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            }
            else
            {
                //await Logger.Logging(new LogMessage { Message = $"Tried to retrieve non existent doctor", CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
            return doctor;
        }
        public async Task AddDoctor(Doctor doctor)
        {
            await appDbContext.Doctors.AddAsync(doctor);
            try
            {
                await appDbContext.SaveChangesAsync();
                string doctorJson = JsonSerializer.Serialize(doctor);
                //await Logger.Logging(new LogMessage { Message = "Doctor has been added :" + doctorJson, CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            }
            catch (Exception ex)
            {
                //await Logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }

        }

        public async Task DeleteDoctor(int id)
        {
            var doctor = await GetDoctorById(id);
            if (doctor is null)
            {
                //await Logger.Logging(new LogMessage { Message = "Null Reference Exception tried to Delete non existent Doctor", CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
            if (doctor != null)
            {
                appDbContext.Doctors.Remove(doctor);
                try
                {
                    await appDbContext.SaveChangesAsync();

                    //await Logger.Logging(new LogMessage { Message = $"doctor with id : {id} has been deleted:", LogType = LogType.SUCCESS, CreatedAt = DateTime.Now });
                }
                catch (Exception ex)
                {
                    //await Logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
                }
            }
        }

        public async Task UpdateDoctor(Doctor doctor)
        {
            appDbContext.Entry(doctor).State = EntityState.Modified;
            try
            {
                await appDbContext.SaveChangesAsync();
                //await Logger.Logging(new LogMessage { Message = $"Doctor With Id {doctor.Id} Has been Updated ", CreatedAt = DateTime.Now, LogType = LogType.SUCCESS });
            }
            catch (Exception ex)
            {
                //await Logger.Logging(new LogMessage { Message = ex.Message, CreatedAt = DateTime.Now, LogType = LogType.EXCEPTION });
            }
        }
    }
}

