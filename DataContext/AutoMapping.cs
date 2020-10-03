using AutoMapper;
using UniversityDemo.Models;
using UniversityDemo.Models.DTO;

namespace UniversityDemo.Data
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<StudentDTO, Student>().ForMember(x => x.Enrollments, opt => opt.Ignore());
            CreateMap<Student, StudentDTO>();
        }
    }
}