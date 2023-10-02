using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.JSInterop;
using MyOwnLogger.Services;
using SharedLibrary;
using AutoMapper;

namespace MyOwnLogger.Pages.StudentsRazor
{
    public partial class StudentEdit
    {
        [Parameter]
        public int id { get; set; }
        [Parameter]
        public int CollegeId { get; set; }
        public bool isNew { get; set; }
        public bool Saved { get; set; }
        public string? IsDone { get; set; }
        public string? status { get; set; }
        [Inject]
        public NavigationManager nav { get; set; }
        [Inject]
        public IStudentDataService studentDataService { get; set; }
        [Inject]
        public IMapper mapper { get; set; }
        public StudentDTO CurrentStudent { get; set; }
        protected async override Task OnInitializedAsync()
        {
            if (id != 0)
            {
                isNew = false;
                Student student = await studentDataService.GetStudentById(id);
                CurrentStudent = mapper.Map<StudentDTO>(student);

            }
            else
            {
                isNew = true;
                CurrentStudent = new StudentDTO();
            }
        }
        public async Task HandleSubmit()
            {
            if(isNew)
            {
                CurrentStudent.CollegeId = CollegeId;
                await studentDataService.AddStudent(CurrentStudent);
                nav.NavigateTo($"collegedetails/{CollegeId}");
            }
            else
            {
                Student student = await studentDataService.GetStudentById(id);
                student.FName = CurrentStudent.FName;
                student.LName = CurrentStudent.LName;
                student.Age = CurrentStudent.Age;
                student.PhoneNumber = CurrentStudent.PhoneNumber;
                student.Email = CurrentStudent.Email;
                student.level = CurrentStudent.level;
                student.Semester = CurrentStudent.Semester;
                student.CollegeId = CurrentStudent.CollegeId;
                await studentDataService.UpdateStudent(student.Id, student);
                nav.NavigateTo($"collegedetails/{student.CollegeId}");
            }
        }
        public void ConfigureCulture()
        {
            nav.NavigateTo($"api/Culture/{id}", true);
        }
    }
}