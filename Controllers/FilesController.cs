using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniversityDemo.Controllers.BaseControllers;
using UniversityDemo.Models;
using UniversityDemo.Services;

namespace UniversityDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : BaseApiController
    {
        public FilesController(FileService fileService)
        {
            FileService = fileService;
        }

        private FileService FileService { get; }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] FileForm fileForm)
        {
            return Ok(await FileService.UploadAsync(fileForm));
        }

        [HttpPost("request")]
        public async Task<IActionResult> UploadByRequest()
        {
            return Ok(await FileService.UploadAsync(Request));
        }
    }
}