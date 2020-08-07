using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniversityDemo.Data.Models;
using UniversityDemo.Models.Paging;
using UniversityDemo.Repositories.BaseRepositories;

namespace UniversityDemo.Repositories
{
    public interface IPostRepository : IManageRepository<Post>, IBasePaging<PagingRequest, PagingResult>
    {
        Task<List<Post>> FindIndexingAsync(string blogId = null, CancellationToken token = default);
    }
}