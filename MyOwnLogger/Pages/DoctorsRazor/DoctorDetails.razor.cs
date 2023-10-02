using System;
using Microsoft.AspNetCore.Components;
using MyOwnLogger.Services;
using SharedLibrary;
namespace MyOwnLogger.Pages.DoctorsRazor
{
	public partial class DoctorDetails
	{
		[Parameter]
		public int Id { get; set; }
		[Inject]
		public IDoctorDataService doctorDataService { get; set; }
        [Inject]
        public ISubjectDataService subjectDataService { get; set; }
        public Doctor doctor { get; set; } = new Doctor();
        protected override async Task OnInitializedAsync()
        {
			doctor = await doctorDataService.GetDoctorById(Id);
			await base.OnInitializedAsync();
        }
		public async Task DeleteSubject(int id )
		{			
			await subjectDataService.DeleteSubject(id);
            doctor = await doctorDataService.GetDoctorById(Id);
        }

    }
}

