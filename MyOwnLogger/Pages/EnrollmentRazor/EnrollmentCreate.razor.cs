using System;
using Microsoft.AspNetCore.Components;
using SharedLibrary;
using MyOwnLogger.Services;
namespace MyOwnLogger.Pages.EnrollmentRazor
{
	public partial class EnrollmentCreate
	{
		[Parameter]
		public int StudentId { get; set; }
		[Inject]
		ISubjectDataService subjectDataService { get; set; }
		[Inject]
		IEnrollmentDataService enrollmentDataService { get; set; }
		public List<Subject> subjects { get; set; } = new();
		[Inject]
		NavigationManager navigationManager { get; set; }
		public Enrollment enrollment { get; set; } = new Enrollment();
        protected override async Task OnInitializedAsync()
        {
            subjects = (List<Subject>)await subjectDataService.GetSubject();
            await base.OnInitializedAsync();
        }
		public async Task HandleSubmit()
		{
            enrollment.StudentId = StudentId;
            await enrollmentDataService.AddEnrollment(enrollment);
			navigationManager.NavigateTo($"/studentdetails/{StudentId}");
        }
    }
}

