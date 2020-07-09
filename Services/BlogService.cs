using System.Collections.Generic;
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

        public async Task<Blog> CreateAsync(Blog blog)
        {
            return await blogRepository.InsertAsync(blog);
        }

        public async Task<Blog> GetAsync(string id)
        {
            return await blogRepository.FindOneByIdAsync(id);
        }

        public async Task<List<Blog>> GetByIdsAsync(params string[] ids)
        {
            return await blogRepository.FindByIdsAsync(ids);
        }

        public async Task<Blog> UpdateAsync(Blog blog)
        {
            return await blogRepository.UpdateAsync(blog);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await blogRepository.DeleteAsync(id);
        }

        public async Task<List<Blog>> GetAllAsync()
        {
            return await blogRepository.FindAllAsync();
        }
    }
}