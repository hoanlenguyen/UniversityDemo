using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniversityDemo.Controllers.BaseControllers;
using UniversityDemo.Models;
using UniversityDemo.Models.Paging;
using UniversityDemo.Services;

namespace UniversityDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : BaseApiController
    {
        private readonly PostService postService;

        public PostsController(PostService postService)
        {
            this.postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Post item)
        {
            return Ok(await postService.CreateAsync(GetUserInfo(User), item));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return Ok(await postService.GetAsync(id));
        }

        [HttpPost("all/ids")]
        public async Task<IActionResult> GetByIdsAsync(params string[] ids)
        {
            return Ok(await postService.GetByIdsAsync(ids));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Post item)
        {
            return Ok(await postService.UpdateAsync(GetUserInfo(User), item));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return Ok(await postService.DeleteAsync(GetUserInfo(User), id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int? maxResultCount = null)
        {
            return Ok(await postService.GetAllAsync(maxResultCount));
        }

        [HttpPost("paging")]
        public async Task<IActionResult> PageIndexingItemsAsync([FromForm]PagingRequest request)
        {
            return Ok(await postService.PageIndexingItemsAsync(request));
        }
    }
}