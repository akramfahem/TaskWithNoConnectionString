using System;

namespace SharedLibrary
{
	public class University
	{	
		public int Id { get; set; }
		public string Name { get; set;}
        public virtual ICollection<College>? Colleges { get; set; }


    }
}

