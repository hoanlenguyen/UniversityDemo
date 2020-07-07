using UniversityDemo.Models;
using UniversityDemo.Repositories.BaseRepositories;

namespace UniversityDemo.Repositories
{
    public interface IBlogRepository : IManageRepository<Blog>
    {
    }
}