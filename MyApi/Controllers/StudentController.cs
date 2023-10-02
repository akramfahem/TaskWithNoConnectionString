using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APII.Model;
using SharedLibrary;
using AutoMapper;
namespace APII.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepositry studentRepositry;
        private readonly IMapper mapper;
        public StudentController(IStudentRepositry studentRepositry, IMapper mapper)
        {
            this.studentRepositry = studentRepositry;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            IEnumerable<Student>? students = await studentRepositry.GetAllAsync();
            if (students is null)
            {
                return NotFound();
            }
            return Ok(students);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            return Ok(await studentRepositry.GetByIdAsync(id));

        }
        [HttpPost]
        public async Task<ActionResult> AddStudent(StudentDTO studentdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Student Object is not Valid !!!");
            }
            Student student = mapper.Map<Student>(studentdto);
            try
            {
                await studentRepositry.AddAsync(student);
            }
            catch (Exception ex)
            {
                var s = await studentRepositry.GetByIdAsync(student.Id);
                if (s is not null)
                {
                    if (s.Id == student.Id)
                        return Conflict(); // 409
                }
                else
                    return StatusCode((int)HttpStatusCode.InternalServerError);

            }
            return Created("Student is Created", student);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await studentRepositry.GetByIdAsync(id);
            if (student is null)
            {
                return NotFound();
            }
            await studentRepositry.DeleteAsync(student.Id);
            return Ok();

        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateStudent(int id, [FromBody] Student student)
        {
            if (id != student.Id)
            {
                return BadRequest("PK Is Wrong");
            }
            try
            {
                await studentRepositry.UpdateAsync(student);
            }
            catch (Exception ex)
            {
                var s = studentRepositry.GetByIdAsync(id);
                if (s is null) 
                {
                    return NotFound();
                }
                else
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return StatusCode((int)HttpStatusCode.NoContent);
        }



    }
}

