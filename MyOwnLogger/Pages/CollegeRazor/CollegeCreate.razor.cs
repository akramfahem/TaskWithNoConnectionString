using System;
using SharedLibrary;
using MyOwnLogger.Services;
using Microsoft.AspNetCore.Components;

namespace MyOwnLogger.Pages.CollegeRazor
{
	public partial class CollegeCreate
	{
		[Parameter]
		public int Id { get; set; }
		[Inject]
		public ICollegeDataService collegeDataService { get; set; }
		public CollegeDTO collegeDTO { get; set; } = new CollegeDTO();
		[Inject]
		NavigationManager navigationManager { get; set; }
		public async Task HandleSubmit()
		{
			collegeDTO.UniversityId = Id;
			await collegeDataService.AddCollege(collegeDTO);
			navigationManager.NavigateTo($"/university/{Id}");
		}
	}
}

