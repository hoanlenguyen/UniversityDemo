using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.Data.Models;
using UniversityDemo.Identity;
using UniversityDemo.Models;
using UniversityDemo.Models.DTO;
using UniversityDemo.Models.Paging;
using UniversityDemo.Repositories;

namespace UniversityDemo.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;

        public PostService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public async Task<Post> CreateAsync(UserInfo user, Post post)
        {
            return await postRepository.InsertAsync(user, post);
        }

        public async Task<Post> GetAsync(string id)
        {
            return await postRepository.FindOneByIdAsync(id);
        }

        public async Task<List<Post>> GetByIdsAsync(params string[] ids)
        {
            return await postRepository.FindByIdsAsync(ids);
        }

        public async Task<Post> UpdateAsync(UserInfo user, Post post)
        {
            return await postRepository.UpdateAsync(user, post);
        }

        public async Task<bool> DeleteAsync(UserInfo user, string id)
        {
            return await postRepository.DeleteAsync(user, id);
        }

        public async Task<List<Post>> GetAllAsync(int? maxResultCount = null, string orderBy = null, bool isOrderAsc = true)
        {
            return await postRepository.GetAllAsync(maxResultCount, orderBy, isOrderAsc);
        }

        public async Task<List<PostIndexingModel>> GetIndexingAsync(string blogId = null)
        {
            return (await postRepository.FindIndexingAsync(blogId))
                                        .Select(x => x.ToIndexingModel()).ToList();
        }

        public async Task<PagingResult> PageIndexingItemsAsync(PagingRequest request)
        {
            return await postRepository.PageIndexingItemsAsync(request);
        }
    }
}