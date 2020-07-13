using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniversityDemo.Models.DTO;
using UniversityDemo.Services;

namespace UniversityDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService studentService;
        private readonly IMapper mapper;

        public StudentsController(StudentService studentService, IMapper mapper)
        {
            this.studentService = studentService;
            this.mapper = mapper;
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await studentService.Get(id);
            var result = mapper.Map<StudentDTO>(entity);
            return new JsonResult(result) { StatusCode = 200 };
        }
    }
}