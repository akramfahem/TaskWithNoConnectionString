using System;
namespace SharedLibrary
{
	public class SubjectDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalGrade { get; set; }
        public int StudentGrade { get; set; }
        public int? DoctorId { get; set; }
        public int CollegeId { get; set; }
    }
}

