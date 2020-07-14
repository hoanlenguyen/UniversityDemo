using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.Identity;
using UniversityDemo.Models;
using UniversityDemo.Repositories;

namespace UniversityDemo.Services
{
    public class BlogService
    {
        private readonly IBlogRepository blogRepository;
        private readonly IPostService postService;
        public BlogService(IBlogRepository blogRepository, IPostService postService)
        {
            this.blogRepository = blogRepository;
            this.postService = postService;
        }

        public async Task<Blog> CreateAsync(UserInfo user, Blog item)
        {
            return await blogRepository.InsertAsync(user, item);
        }

        public async Task<Blog> GetAsync(string id)
        {
            return await blogRepository.FindOneByIdAsync(id);
        }

        public async Task<IEnumerable> GetByIdsAsync(params string[] ids)
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

        public async Task<IEnumerable> GetAllAsync()
        {
            return await blogRepository.FindAllAsync();
        }

        //public async Task<List<BlogIndexingModel>> GetIndexingAsync()
        //{
        //    return (await blogRepository.FindIndexingAsync()).Select(x=>x.ToIndexingModel()).ToList();
        //}

        public async Task<IEnumerable> GetIndexingAsync()
        {
            var blogs= (await blogRepository.FindAllAsync())
                                            .Select(x => x.ToIndexingModel())
                                            .ToList();
            foreach (var blog in blogs)
            {
                blog.Posts = (await postService.GetIndexingAsync(blog.Id));
            }
            return blogs;
        }
    }
}