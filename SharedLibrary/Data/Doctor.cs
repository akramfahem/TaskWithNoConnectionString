using System;
namespace SharedLibrary
{
	public class Doctor
	{
        public int Id { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int CollegeId { get; set; }

        public virtual College? College { get; set; }
        public virtual ICollection<Subject>? Subjects { get; set; }
    }
}

