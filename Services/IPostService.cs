using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.Models.DTO;

namespace UniversityDemo.Services
{
    public interface IPostService
    {
        Task<List<PostIndexingModel>> GetIndexingAsync(string blogId = null);
    }
}
