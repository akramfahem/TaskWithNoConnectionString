using System;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary;
using APII.Model;
using System.Net;
using System.Numerics;

namespace APII.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EnrollmentController:ControllerBase
	{
		private readonly iEnrollmentRepositry enrollmentRepositry;
		public EnrollmentController(iEnrollmentRepositry enrollmentRepositry)
		{
			this.enrollmentRepositry = enrollmentRepositry;


        }
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Enrollment>>> Get()
		{
			var enrollments = await enrollmentRepositry.GetEnrollments();
			if (enrollments is null)
				return NotFound();
			return Ok(enrollments);
		}
		[HttpGet("{SubjectId:int}/{StudentId:int}")]
		public async Task<ActionResult<Enrollment>> Get(int SubjectId,int StudentId)
		{
			var enrollment = await enrollmentRepositry.GetEnrollmentById(SubjectId, StudentId);
			if (enrollment is null)
				return NotFound();
			return Ok(enrollment);
		}
		[HttpPost]
		public async Task<ActionResult>Add(Enrollment enrollment)
        {
            var enrollment1 = await enrollmentRepositry.GetEnrollmentById(enrollment.StudentId,enrollment.StudentId);
            if (enrollment1 is not null)
                return Conflict();
            try
            {
                await enrollmentRepositry.AddEnrollment(enrollment);
            }
            catch
            {
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return Created("Doctor Created Successfully", enrollment);
        }
		[HttpPut("{id:int}")]
		public async Task<ActionResult> Update(int id , Enrollment enrollment)
		{
			if (id != enrollment.Id)
				return BadRequest("PK id Wrong");
			try
			{
				await enrollmentRepositry.UpdateEnrollment(enrollment);
			}
			catch
			{
				var enrollment1 = enrollmentRepositry.GetEnrollmentById(enrollment.SubjectId,enrollment.StudentId);
				if (enrollment1 is null)
					return NotFound();
				else
					return StatusCode((int)HttpStatusCode.InternalServerError);
			}
            return StatusCode((int)HttpStatusCode.NoContent);

        }
        [HttpDelete("{SubjectId:int}/{StudentId:int}")]
		public async Task<ActionResult>Delete(int SubjectId,int StudentId)
		{
			var enrollment = await enrollmentRepositry.GetEnrollmentById(SubjectId, StudentId);
			if (enrollment is null)
				return NotFound();
			await enrollmentRepositry.DeleteEnrollment(SubjectId, StudentId);
			return Ok();
		}
    }
}

