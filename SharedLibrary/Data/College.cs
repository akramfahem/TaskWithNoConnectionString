using System;
namespace SharedLibrary
{
	public class College
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int UniversityId { get; set; }
		public virtual University? University { get; set; }
        public virtual ICollection<Student>? Students { get; set; }
        public virtual ICollection<Doctor>? Doctors { get; set; }
        public virtual ICollection<Subject>? Subjects { get; set; }


    }
}

