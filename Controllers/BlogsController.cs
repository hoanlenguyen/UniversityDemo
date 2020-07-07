using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

        //[HttpPost("Create")]
        //public async Task<IActionResult> Create(Blog blog)
        //{
        //    await cosmosDbService.AddItemAsync(blog);
        //    return Ok();
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(string id)
        //{
        //    string query = $"SELECT * FROM c WHERE c.id='{id}' AND c.type='{nameof(Blog)}'";
        //    return Ok(await cosmosDbService.GetItemsAsync(query));
        //}

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