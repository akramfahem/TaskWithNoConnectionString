using System;
using SharedLibrary;
using MyOwnLogger.Services;
using Microsoft.AspNetCore.Components;

namespace MyOwnLogger.Pages.UniversityRazor
{
	public partial class UniversityCreate
	{
		[Inject]
		IUniversityDataService universityDataService { get; set; }
        UniversityDTO universityDTO { get; set; } = new UniversityDTO();
        [Inject]
        NavigationManager navigationManager { get; set; }
        protected async override Task OnInitializedAsync()
        {

            await base.OnInitializedAsync();
        }
        public async Task HandleSubmit()
        {
            await universityDataService.AddUniversity(universityDTO);
            navigationManager.NavigateTo("/university");
        }

    }
}

