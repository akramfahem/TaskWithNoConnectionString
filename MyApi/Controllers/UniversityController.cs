using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APII.Model;
using SharedLibrary;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace APII.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IMapper Mapper;
        private readonly IUniversityRepositry UniversityRepositry;
        public UniversityController(IUniversityRepositry UniversityRepositry,IMapper mapper)
        {
            this.UniversityRepositry = UniversityRepositry;
            this.Mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<University>>> GetUniversity()
        {
            IEnumerable<University>? universities = await UniversityRepositry.GetAllAsync();
            if (universities is null)
            {
                return NotFound();
            }
            return Ok(universities);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<University>>> GetUniversity(int id)
        {
            return Ok(await UniversityRepositry.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<ActionResult> AddUniversity(UniversityDTO universityDTO)
        {
            //var university = new University { Id = universityDTO.Id,Name = universityDTO.Name,Colleges = new List<College>()};
            var university = Mapper.Map<University>(universityDTO);
            await UniversityRepositry.AddAsync(university);
            return Created("University is Created", university);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateUniversity(int id,[FromBody]University university)
        {
            if (id != university.Id)
            {
                return BadRequest("PK Is Wrong");
            }
            try
            {
                await UniversityRepositry.UpdateAsync(university);
            }
            catch (Exception ex)
            {
                var s = UniversityRepositry.GetByIdAsync(id);
                if (s is null)
                {
                    return NotFound();
                }
                else
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return StatusCode((int)HttpStatusCode.NoContent);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUniversity(int id)
        {
            var uni = await UniversityRepositry.GetByIdAsync(id);
            if (uni is null)
                return NotFound();
            await UniversityRepositry.DeleteAsync(uni.Id);
            return Ok();
        }


    }

}

