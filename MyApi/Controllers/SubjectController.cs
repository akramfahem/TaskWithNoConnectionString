using System;
using System.Collections.Generic;
using System.Linq;
using APII.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SharedLibrary;
using System.Net;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APII.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : Controller
    {
        private readonly ISubjectRepositry subjectRepositry;
        private readonly IMapper mapper;
        public SubjectController(ISubjectRepositry subjectRepositry, IMapper mapper)
        {
            this.subjectRepositry = subjectRepositry;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>>Get()
        {
            var subjects = await subjectRepositry.GetSubjects();
            if (subjects is null)
                return NotFound();
            return Ok(subjects);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Subject>>Get(int id)
        {
            var subject = await subjectRepositry.GetSubjectById(id);
            if (subject is null)
                return NotFound();
            return Ok(subject);
        }
        [HttpPost]
        public async Task<ActionResult> Add(SubjectDTO subjectDTO)
        {
            Subject subject = mapper.Map<Subject>(subjectDTO);
            await subjectRepositry.AddSubject(subject);
            return Created("Subject is Created", subject);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult>Delete(int id)
        {
            var subject = await subjectRepositry.GetSubjectById(id);
            if (subject is null)
                return NotFound();
            await subjectRepositry.DeleteSubject(id);
            return Ok();
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult>Update(int id, [FromBody]Subject subject)
        {
            if (id != subject.Id)
                return BadRequest("PK is Wrong");
            try
            {
                await subjectRepositry.UpdateSubject(subject);
            }
            catch (Exception ex)
            {
                var s = subjectRepositry.GetSubjectById(id);
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

