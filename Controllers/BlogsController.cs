using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Blog blog)
        {
            await blogService.CreateAsync(blog);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>Get(string id)
        {
            return Ok(await blogService.GetAsync(id));
        }
    }
}