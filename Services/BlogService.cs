using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.Data.Models;
using UniversityDemo.Identity;
using UniversityDemo.Models;
using UniversityDemo.Models.Paging;
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

        public async Task<Blog> CreateAsync(IUserInfo user, Blog item)
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

        public async Task<Blog> UpdateAsync(IUserInfo user, Blog item)
        {
            return await blogRepository.UpdateAsync(user, item);
        }

        public async Task<bool> DeleteAsync(IUserInfo user, string id)
        {
            return await blogRepository.DeleteAsync(user, id);
        }

        public async Task<IEnumerable> GetAllAsync(int? maxResultCount = null, string orderBy = null, bool isOrderAsc = true)
        {
            return await blogRepository.GetAllAsync(maxResultCount, orderBy, isOrderAsc);
        }

        //public async Task<List<BlogIndexingModel>> GetIndexingAsync()
        //{
        //    return (await blogRepository.FindIndexingAsync()).Select(x=>x.ToIndexingModel()).ToList();
        //}

        public async Task<IEnumerable> GetIndexingAsync(int? maxResultCount = null)
        {
            var blogs= (await blogRepository.GetAllAsync(maxResultCount))
                                            .Select(x => x.ToIndexingModel())
                                            .ToList();
            foreach (var blog in blogs)
            {
                blog.Posts = (await postService.GetIndexingAsync(blog.Id));
            }
            return blogs;
        }

        public async Task<PagingResult> PageIndexingItemsAsync(PagingRequest request)
        {
            return await blogRepository.PageIndexingItemsAsync(request);
        }

        public async Task<int> GetItemCount()
        {
            return await blogRepository.GetItemCount();
        }
    }
}