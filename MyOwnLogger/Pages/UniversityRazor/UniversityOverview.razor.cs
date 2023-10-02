using System;
using Microsoft.AspNetCore.Components;
using MyOwnLogger.Services;
using SharedLibrary;
namespace MyOwnLogger.Pages.UniversityRazor
{
	public partial class UniversityOverview
	{
		[Inject]
		IUniversityDataService UniversityDataService { get; set; }
        public IEnumerable<University> Universities { get; set; } = new List<University>();
        [Inject]
        public NavigationManager nav { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Universities = await UniversityDataService.GetUniversity();
            await base.OnInitializedAsync();
        }
        public async Task DeleteUniversity(int id)
        {
            await UniversityDataService.DeleteUniversity(id);
            Universities = await UniversityDataService.GetUniversity();
        }
    }
}

