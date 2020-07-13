using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.Identity;
using UniversityDemo.Models;
using UniversityDemo.Repositories;

namespace UniversityDemo.Services
{
    public class PostService
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

        public async Task<List<Post>> GetAllAsync()
        {
            return await postRepository.FindAllAsync();
        }
    }
}
