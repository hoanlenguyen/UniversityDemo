using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniversityDemo.Models;
using UniversityDemo.Services;

namespace UniversityDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly BlogService blogService;

        public BlogsController(BlogService blogService)
        {
            this.blogService = blogService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Blog blog)
        {
            return Ok(await blogService.CreateAsync(blog));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return Ok(await blogService.GetAsync(id));
        }

        [HttpPost("all/ids")]
        public async Task<IActionResult> GetByIdsAsync(params string[] ids)
        {
            return Ok(await blogService.GetByIdsAsync(ids));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Blog blog)
        {
            return Ok(await blogService.UpdateAsync(blog));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return Ok(await blogService.DeleteAsync(id));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await blogService.GetAllAsync());
        }
    }
}