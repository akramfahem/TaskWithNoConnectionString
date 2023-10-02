using System;
using SharedLibrary;
using MyOwnLogger.Services;
using Microsoft.AspNetCore.Components;
using AutoMapper;
namespace MyOwnLogger.Pages.SubjectsRazor
{
	public partial class SubjectEdit
	{
		[Parameter]
		public int Id { get; set; }
		[Inject]
		public ISubjectDataService subjectDataService { get; set; }
		[Inject]
		public IMapper mapper { get; set; }
		SubjectDTO subjectDTO { get; set; } = new SubjectDTO();
        public List<Doctor> doctors { get; set; } = new();
        [Inject]
        public IDoctorDataService doctorDataService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            doctors = (List<Doctor>)await doctorDataService.GetDoctors();
            Subject subject = await subjectDataService.GetSubjectById(Id);
			subjectDTO = mapper.Map<SubjectDTO>(subject);
            await base.OnInitializedAsync();
        }
		public async Task HandleSubmit()
		{
            Subject subject = await subjectDataService.GetSubjectById(Id);
			subject.Name = subjectDTO.Name;
			subject.TotalGrade = subjectDTO.TotalGrade;
			subject.DoctorId = subjectDTO.DoctorId;
			await subjectDataService.UpdateSubject(Id,subject);
        }
    }
}

