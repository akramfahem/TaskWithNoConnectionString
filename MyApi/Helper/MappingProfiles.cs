using System;
using SharedLibrary;
using AutoMapper;

namespace APII.Helper
{
	public class MappingProfiles: Profile
	{
		public MappingProfiles()
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

