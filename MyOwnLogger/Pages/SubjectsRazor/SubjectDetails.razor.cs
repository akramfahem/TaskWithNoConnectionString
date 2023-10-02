using System;
using Microsoft.AspNetCore.Components;
using MyOwnLogger.Services;
using SharedLibrary;
namespace MyOwnLogger.Pages.SubjectsRazor
{
	public partial class SubjectDetails
	{
		[Parameter]
		public int Id { get; set;  }
		[Inject]
		public ISubjectDataService subjectDataService { get; set; }
		public Subject subject { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
			subject = await subjectDataService.GetSubjectById(Id);
            await base.OnInitializedAsync();
        }
    }
}

