using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityDemo.Identity;
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

        public async Task<Blog> CreateAsync(UserInfo user, Blog item)
        {
            return await blogRepository.InsertAsync(user, item);
        }

        public async Task<Blog> GetAsync(string id)
        {
            return await blogRepository.FindOneByIdAsync(id);
        }

        public async Task<List<Blog>> GetByIdsAsync(params string[] ids)
        {
            return await blogRepository.FindByIdsAsync(ids);
        }

        public async Task<Blog> UpdateAsync(UserInfo user, Blog item)
        {
            return await blogRepository.UpdateAsync(user, item);
        }

        public async Task<bool> DeleteAsync(UserInfo user, string id)
        {
            return await blogRepository.DeleteAsync(user, id);
        }

        public async Task<List<Blog>> GetAllAsync()
        {
            return await blogRepository.FindAllAsync();
        }
    }
}