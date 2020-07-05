using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.Models;
using UniversityDemo.Repositories;

namespace UniversityDemo.Services
{
    public class BlogService
    {
        private readonly IBlogRepository blogRepository;
        public BlogService(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }

        public async Task CreateAsync(Blog blog)
        {
           await blogRepository.InsertAsync(blog);
        }

        public async Task<Blog> GetAsync(string id)
        {
           return await blogRepository.FindOneByIdAsync(id);
        }
    }
}
