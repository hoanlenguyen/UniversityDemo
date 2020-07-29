using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityDemo.Identity;

namespace UniversityDemo.Repositories.BaseRepositories
{
    public interface IBasePagingRepository
    {
        //Task<List<IndexingT>> FindIndexingAsync(FilterX filter);
        //Task<IEnumerable> FindIndexingAsync(UserInfo user, CancellationToken token = default);
        Task<IEnumerable> PageIndexingItemsAsync(int skipPages = 0, int take = 10);
    }
}
