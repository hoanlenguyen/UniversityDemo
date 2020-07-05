using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityDemo.Models;
using UniversityDemo.Services;

namespace UniversityDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(List<Course> courses)
        {
            await courseService.Create(courses);
            return Ok();
        }

        private JsonResult Error(string message)
        {
            return new JsonResult(message) { StatusCode = 400 };
        }
    }
}