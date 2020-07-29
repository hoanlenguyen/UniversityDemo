using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniversityDemo.Models;
using UniversityDemo.Repositories.BaseRepositories;

namespace UniversityDemo.Repositories
{
    public interface IBlogRepository : IManageRepository<Blog>, IBasePagingRepository
    {
        //Task<IEnumerable> FindIndexingAsync(CancellationToken token = default);
    }
}