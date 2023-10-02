using System;
using Microsoft.AspNetCore.Components;
using SharedLibrary;
using MyOwnLogger.Services;
namespace MyOwnLogger.Pages.StudentsRazor
{
	public partial class StudentDetails
	{
		[Parameter]
		public int Id { get; set; }
		[Inject]
		public IStudentDataService studentDataService {get; set; }
		[Inject]
		public IEnrollmentDataService enrollmentDataService { get; set; }
		public Student student { get; set; } = new Student();
        protected override async Task OnInitializedAsync()
        {
			student = await studentDataService.GetStudentById(Id);
        }
		public async Task DeleteSubject(int SubjectId,int StudentId)
		{
			await enrollmentDataService.DeleteEnrollment(SubjectId, StudentId);
            student = await studentDataService.GetStudentById(Id);
        }

    }
}

