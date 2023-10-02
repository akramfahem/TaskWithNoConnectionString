using System;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using SharedLibrary;
using MyOwnLogger.Services;
namespace MyOwnLogger.Pages.SubjectsRazor
{
	public partial class SubjectCreate
	{
		[Parameter]
		public int CollegeId { get; set; }
		[Inject]
		public ISubjectDataService subjectDataService {get; set;}
        [Inject]
        public IDoctorDataService doctorDataService { get; set; }
        [Inject]
		public IMapper mapper { get; set; }
		[Inject]
		NavigationManager navigationManager { get; set; }
		public SubjectDTO subjectDTO { get; set; } = new SubjectDTO();
		public List<Doctor> doctors { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
			doctors =(List<Doctor>)await doctorDataService.GetDoctors();

            await base.OnInitializedAsync();
        }
        public async Task HandleSubmit()
		{
            subjectDTO.CollegeId = CollegeId;
            await subjectDataService.AddSubject(subjectDTO);
            navigationManager.NavigateTo($"/collegedetails/{CollegeId}");

        }

    }
}

