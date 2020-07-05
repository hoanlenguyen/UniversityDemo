using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityDemo.BaseEntities;

namespace UniversityDemo.Repositories.BaseRepositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> FindAllAsync();

        Task<T> FindOneByIdAsync(string id);

        Task<bool> DeleteAsync(params string[] ids);

        Task<T> InsertAsync(T item);

        Task<T> UpdateAsync(T item);
    }
}