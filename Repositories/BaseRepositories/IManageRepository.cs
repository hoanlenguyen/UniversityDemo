
using UniversityDemo.Data.BaseEntities;

namespace UniversityDemo.Repositories.BaseRepositories
{
    public interface IManageRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
    }
}