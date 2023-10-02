using System;
using Microsoft.AspNetCore.Components;
using MyOwnLogger.Services;
using SharedLibrary;

namespace MyOwnLogger.Pages.UniversityRazor
{
	public partial class UniversityDetails
	{
        [Parameter]
        public int Id { get; set; }
        [Inject]
        IUniversityDataService UniversityDataService { get; set; }
        [Inject]
        ICollegeDataService CollegeDataService { get; set; }
        public University university { get; set; } = new University();
        [Inject]
        public NavigationManager nav { get; set; }
        protected override async Task OnInitializedAsync()
        {
            university = await UniversityDataService.GetUniversityById(Id);
            await base.OnInitializedAsync();
        }
        public async Task DeleteCollege(int id)
        {
            await CollegeDataService.DeleteCollege(id);
            university = await UniversityDataService.GetUniversityById(Id);
        }
    }
}

