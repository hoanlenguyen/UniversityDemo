using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.Identity;
using UniversityDemo.Models.DTO;

namespace UniversityDemo.Models
{
    public class ModelAutoMapper : Profile
    {
        public ModelAutoMapper()
        {
            CreateMap<Student, StudentDTO>()
            .ReverseMap()
            .ForMember(x => x.Id, options => { options.Ignore(); });

            CreateMap<ApplicationRole, RoleDTO>()
            .ReverseMap();
        }       
    }
}
