using System;

namespace SharedLibrary
{
	public class Subject
	{
		public int Id { get; set; }
        public string Name { get; set; }
		public int TotalGrade { get; set; }
        public int? DoctorId { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public int CollegeId { get; set; }
        public virtual College? College { get; set; }
        public virtual ICollection<Student>? Students { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
      

    }
}
