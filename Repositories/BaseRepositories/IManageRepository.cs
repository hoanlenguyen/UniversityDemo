using UniversityDemo.BaseEntities;

namespace UniversityDemo.Repositories.BaseRepositories
{
    public interface IManageRepository<T> : IRepository<T> where T : BaseEntity
    {
    }
}