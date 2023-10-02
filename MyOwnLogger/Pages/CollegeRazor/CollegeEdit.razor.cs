using System;
using Microsoft.AspNetCore.Components;
using SharedLibrary;
using AutoMapper;
using MyOwnLogger.Services;
namespace MyOwnLogger.Pages.CollegeRazor
{
	public partial class CollegeEdit
	{
		[Parameter]
		public int Id { get; set; }
		[Inject]
		public ICollegeDataService collegeDataService { get; set; }
		[Inject]
		public IMapper mapper { get; set; }
		public CollegeDTO collegeDTO { get; set; } = new CollegeDTO();
        protected override async Task OnInitializedAsync()
        {
			College college = await collegeDataService.GetCollegeById(Id);
			collegeDTO = mapper.Map<CollegeDTO>(college);
            await base.OnInitializedAsync();
        }
		public async Task HandleSubmit()
		{
			var College = await collegeDataService.GetCollegeById(Id);
			College.Name = collegeDTO.Name;
			await collegeDataService.UpdateCollege(Id,College);
		}
    }
}

