using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniversityDemo.Models;
using UniversityDemo.Repositories.BaseRepositories;

namespace UniversityDemo.Repositories
{
    public interface IPostRepository : IManageRepository<Post>
    {
        Task<List<Post>> FindIndexingAsync(string blogId = null, CancellationToken token = default);
    }
}