using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APII.Model;
using SharedLibrary;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace APII.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CollegeController : ControllerBase
	{
		private readonly ICollegeRepositry collegeRepositry;
		private readonly IMapper mapper;
		public CollegeController(ICollegeRepositry collegeRepositry, IMapper Mapper)
		{
			this.collegeRepositry = collegeRepositry;
			this.mapper = Mapper;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<College>>> Get()
		{
			var colleges = await collegeRepositry.GetCollegesAsync();
			if (colleges is null)
				return NotFound();
			return Ok(colleges);
		}
		[HttpGet("{id:int}")]
		public async Task<ActionResult<College>> Get(int id)
		{
			var college = await collegeRepositry.GetCollegeByIdAsync(id);
			if (college is null)
				return NotFound();
			return Ok(college);
		}
		[HttpPost]
		public async Task<ActionResult> Add(CollegeDTO collegedto)
		{
			College college = mapper.Map<College>(collegedto);
			await collegeRepositry.AddCollegeAsync(college);
			return Created("College is Created", college);
		}
		[HttpPut("{id:int}")]
		public async Task<ActionResult> Update(int id, [FromBody]College college)
		{
			if(id != college.Id)
            {
                return BadRequest("PK Is Wrong");
            }
            try
            {
                await collegeRepositry.UpdateCollegeAsync(college);
            }
            catch (Exception ex)
            {
                var s = collegeRepositry.GetCollegeByIdAsync(id);
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
        public async Task<ActionResult> Delete(int id)
		{
            var college = await collegeRepositry.GetCollegeByIdAsync(id);
            if (college is null)
            {
                return NotFound();
            }
            await collegeRepositry.DeleteCollegeAsync(college.Id);
            return Ok();
        }
    }
}

