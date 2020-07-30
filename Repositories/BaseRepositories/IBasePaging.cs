using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityDemo.Identity;

namespace UniversityDemo.Repositories.BaseRepositories
{
    public interface IBasePaging<T,U> where T : class where U : class
    {
        //Task<List<IndexingT>> FindIndexingAsync(FilterX filter);
        //Task<IEnumerable> FindIndexingAsync(UserInfo user, CancellationToken token = default);
        Task<U> PageIndexingItemsAsync(T request);
    }
}
