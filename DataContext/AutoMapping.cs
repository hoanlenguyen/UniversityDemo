using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.Models;
using UniversityDemo.Models.DTO;

namespace UniversityDemo.Data
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            CreateMap<StudentDTO, Student>().ForMember(x => x.Enrollments, opt => opt.Ignore());
            CreateMap<Student, StudentDTO>();
        }
    }
}
