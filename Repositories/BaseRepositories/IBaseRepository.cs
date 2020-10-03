using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityDemo.Data.BaseEntities;
using UniversityDemo.Identity;

namespace UniversityDemo.Repositories.BaseRepositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync(int? maxResultCount = null);

        Task<T> FindOneByIdAsync(string id);

        Task<List<T>> FindByIdsAsync(params string[] ids);

        Task<bool> DeleteAsync(IUserInfo user, params string[] ids);

        Task<T> InsertAsync(IUserInfo user, T item);

        Task<T> UpdateAsync(IUserInfo user, T item);
    }
}