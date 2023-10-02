using System;
using SharedLibrary;

namespace SharedLibrary
{
	public class Student
	{
        public int Id { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int CollegeId { get; set; }
        public int level { get; set; }
        public int Semester { get; set; }
        public virtual College? College { get; set; }
        public virtual ICollection<Subject>? Subjects { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }


    }
}

