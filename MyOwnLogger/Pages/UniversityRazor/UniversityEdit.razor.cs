using System;
using SharedLibrary;
using Microsoft.AspNetCore.Components;
using MyOwnLogger.Services;
using AutoMapper;
namespace MyOwnLogger.Pages.UniversityRazor
{
	public partial class UniversityEdit
	{
		[Parameter]
		public int Id { get; set; }
        [Inject]
        public IUniversityDataService UniversityDataService { get; set; }
        [Inject]
        public IMapper mapper { get; set; }
        public UniversityDTO universityDTO { get; set; } = new UniversityDTO();
        [Inject]
        NavigationManager navigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            University university = await UniversityDataService.GetUniversityById(Id);
            universityDTO = mapper.Map<UniversityDTO>(university);

            await base.OnInitializedAsync();
        }
        public async Task HandleSubmit()
        {
            var uni = mapper.Map<University>(universityDTO);
            await UniversityDataService.UpdateUniversity(Id, uni);
            navigationManager.NavigateTo("/university");
        }
    }
}

