using System;
using APII.Model;
using SharedLibrary;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Net;

namespace APII.Controllers
{
	[ApiController]
    [Route("api/[controller]")]
	public class DoctorController:ControllerBase
	{
		private readonly IDoctorRepositry doctorRepositry;
		private readonly IMapper mapper;
		public DoctorController(IDoctorRepositry doctorRepositry, IMapper mapper)
		{
			this.doctorRepositry = doctorRepositry;
			this.mapper = mapper;
        }
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Doctor>>> Get()
		{
			var doctors = await doctorRepositry.GetDoctors();
			if (doctors is null)
				return NotFound();
			return Ok(doctors);
		}
		[HttpGet("{id:int}")]
		public async Task<ActionResult<Doctor>> GetById(int id)
		{
			var doctor = await doctorRepositry.GetDoctorById(id);
			if (doctor is null)
				return NotFound();
			return Ok(doctor);
		}
		[HttpPost]
		public async Task<ActionResult<Doctor>>Add(DoctorDTO doctorDTO)
		{
			Doctor doctor = mapper.Map<Doctor>(doctorDTO);
            
            try
			{
				await doctorRepositry.AddDoctor(doctor);
			}
			catch
			{
				var doc = doctorRepositry.GetDoctorById(doctorDTO.Id);
					if(doc is not null)
						return Conflict();
                    else
						return StatusCode((int)HttpStatusCode.InternalServerError);
            }
			return Created("Doctor Created Successfully", doctor);

        }
		[HttpPut("{id:int}")]
		public async Task<ActionResult>Update(int id, [FromBody]Doctor doctor)
		{
			if(id != doctor.Id)
			{
				return BadRequest("PK is Wrong");
			}
			try
			{
				await doctorRepositry.UpdateDoctor(doctor);
			}
			catch
			{
				var doc = doctorRepositry.GetDoctorById(id);
				if (doc is null)
					return NotFound();
				else
					return StatusCode((int)HttpStatusCode.InternalServerError);
			}
			return StatusCode((int)HttpStatusCode.NoContent);
		}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
		{
			var doctor = await doctorRepositry.GetDoctorById(id);
			if (doctor is null)
				return NotFound();
			else
			{
				await doctorRepositry.DeleteDoctor(doctor.Id);
			}
			return Ok();
		}
	}
}

