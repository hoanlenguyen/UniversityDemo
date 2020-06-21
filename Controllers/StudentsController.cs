using Microsoft.AspNetCore.Mvc;
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

        //private readonly StudentsService studentsService;

        //public StudentsController(StudentsService studentsService)
        //{
        //    this.studentsService = studentsService;
        //}

        [HttpGet]
        [Route("All")]
        public IActionResult GetAllStudents()
        {
            return Ok(studentsService.GetAllStudents());
        }

        [HttpGet]
        [Route("AllEnroll")]
        public IActionResult GetAllEnrollments()
        {
            return Ok(studentsService.GetAllEnrollments());
        }
    }
}