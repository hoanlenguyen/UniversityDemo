using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using UniversityDemo.Controllers.BaseControllers;
using UniversityDemo.Models;
using UniversityDemo.Services;

namespace UniversityDemo.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController : BaseApiController
    {
        private readonly BlogService blogService;

        public BlogsController(BlogService blogService)
        {
            this.blogService = blogService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Blog blog)
        {
            return Ok(await blogService.CreateAsync(GetUserInfo(User), blog));
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
            return Ok(await blogService.UpdateAsync(GetUserInfo(User), blog));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return Ok(await blogService.DeleteAsync(GetUserInfo(User), id));
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await blogService.GetAllAsync());
        }

        [HttpGet("indexing")]
        public async Task<IActionResult> GetIndexingAsync()
        {
            return Ok(await blogService.GetIndexingAsync());
        }

        [HttpGet("{blogId}/posts/indexing")]
        public async Task<IActionResult> GetPostIndexingAsync(string blogId)
        {
            return Ok(await HttpContext.RequestServices.GetRequiredService<PostService>().GetIndexingAsync(blogId));
        }
    }
}