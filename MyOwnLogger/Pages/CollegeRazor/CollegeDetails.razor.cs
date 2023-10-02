using System;
using Microsoft.AspNetCore.Components;
using MyOwnLogger.Services;
using SharedLibrary;
namespace MyOwnLogger.Pages.CollegeRazor
{
	public partial class CollegeDetails
	{
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public ICollegeDataService collegeDataService { get; set; }
        [Inject]
        public IDoctorDataService DoctorDataService { get; set; }
        [Inject]
        public IStudentDataService StudentDataService { get; set; }
        [Inject]
        public ISubjectDataService SubjectDataService { get; set; }
        public College college { get; set; } = new College();
        protected override async Task OnInitializedAsync()
        {
            college = await collegeDataService.GetCollegeById(Id);
            await base.OnInitializedAsync();
        }
        public async Task DeleteDoctor(int id )
        {
            await DoctorDataService.DeleteDoctor(id);
            college = await collegeDataService.GetCollegeById(Id);
        }
        public async Task DeleteStudent(int id)
        {
            await StudentDataService.DeleteStudent(id);
            college = await collegeDataService.GetCollegeById(Id);
        }
        public async Task DeleteSubject(int id)
        {
            await SubjectDataService.DeleteSubject(id);
            college = await collegeDataService.GetCollegeById(Id);
        }

    }
}