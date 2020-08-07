using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniversityDemo.Data.Models;
using UniversityDemo.Models.Paging;
using UniversityDemo.Repositories.BaseRepositories;

namespace UniversityDemo.Repositories
{
    public interface IBlogRepository : IManageRepository<Blog>, IBasePaging<PagingRequest, PagingResult>
    {
        //Task<IEnumerable> FindIndexingAsync(CancellationToken token = default);
        Task<int> GetItemCount();
    }
}