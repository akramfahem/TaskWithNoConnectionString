using System;
using AutoMapper;
using SharedLibrary;

namespace MyOwnLogger.Helper
{
	public class MappingProfile :Profile
	{
		public MappingProfile()
		{
            CreateMap<University, UniversityDTO>();
            CreateMap<UniversityDTO, University>();
            CreateMap<College, CollegeDTO>();
            CreateMap<CollegeDTO, College>();
            CreateMap<DoctorDTO, Doctor>();
            CreateMap<Doctor, DoctorDTO>();
            CreateMap<SubjectDTO, Subject>();
            CreateMap<Subject, SubjectDTO>();
            CreateMap<StudentDTO, Student>();
            CreateMap<Student, StudentDTO>();
        }
	}
}

