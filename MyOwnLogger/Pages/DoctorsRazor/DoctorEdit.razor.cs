 using System;
using Microsoft.AspNetCore.Components;
using SharedLibrary;
using AutoMapper;
using MyOwnLogger.Services;
namespace MyOwnLogger.Pages.DoctorsRazor
{
	public partial class DoctorEdit
	{
		[Parameter]
		public int Id { get; set; }
		[Inject]
		public IDoctorDataService doctorDataService { get; set; }
		[Inject]
		public IMapper mapper { get; set;}
        [Inject]
		public NavigationManager navigationManager { get; set; }
        public DoctorDTO doctorDTO { get; set; } = new DoctorDTO();
        protected async override Task OnInitializedAsync()
        {
            Doctor doctor = await doctorDataService.GetDoctorById(Id);
            doctorDTO = mapper.Map<DoctorDTO>(doctor);
            await base.OnInitializedAsync();
        }
		public async Task HandleSubmit()
		{
            Doctor doctor = await doctorDataService.GetDoctorById(Id);
            doctor.FName = doctorDTO.FName;
            doctor.LName = doctorDTO.LName;
            doctor.Age = doctorDTO.Age;
            doctor.Email = doctorDTO.Email;
            doctor.PhoneNumber = doctorDTO.PhoneNumber;
            await doctorDataService.UpdateDoctor(Id, doctor);
            navigationManager.NavigateTo($"/collegedetails/{doctor.CollegeId}");

        }
    }
}

