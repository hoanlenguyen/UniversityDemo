using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityDemo.Models;
using UniversityDemo.Service;

namespace UniversityDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            this.studentsService = studentsService;
        }

        
        [HttpGet]
        [Route("All")]
        public Task<IEnumerable<Student>> GetAllStudents()
        {
            return studentsService.GetAllStudents();
        }

        [HttpGet]
        [Route("{id}")]
        public Task<Student> GetStudentById(int id)
        {
            return studentsService.GetStudentByIdAsync(id);
        }

        [HttpPut]
        [Route("Update")]
        public Task UpdateStudent(Student student)
        {
            return studentsService.UpdateStudentAsync(student);
        }

        [HttpPost]
        [Route("Create")]
        public Task CreateStudent(Student student)
        {
            return studentsService.CreateStudentAsync(student);
        }

    }
}