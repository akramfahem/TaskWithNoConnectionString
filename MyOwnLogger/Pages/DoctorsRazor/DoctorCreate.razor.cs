using System;
using SharedLibrary;
using MyOwnLogger.Services;
using AutoMapper;
using Microsoft.AspNetCore.Components;

namespace MyOwnLogger.Pages.DoctorsRazor
{
	public partial class DoctorCreate
	{
        [Parameter]
        public int Id { get; set; }
        [Inject]
		public IDoctorDataService doctorDataService { get; set; }
		[Inject]
		public NavigationManager navigationManager { get; set; }
        [Inject]
        public IMapper mapper { get; set; }
        public DoctorDTO doctorDTO { get; set; } = new DoctorDTO();
        protected override async Task OnInitializedAsync()
        {

            await base.OnInitializedAsync();
        }
        public async Task HandleSubmit()
        {
            doctorDTO.CollegeId = Id;
            await doctorDataService.AddDoctor(doctorDTO);
            navigationManager.NavigateTo($"collegedetails/{Id}");
        }

    }
}

