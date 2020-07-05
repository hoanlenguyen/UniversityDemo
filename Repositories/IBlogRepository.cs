using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.Models;
using UniversityDemo.Repositories.BaseRepositories;

namespace UniversityDemo.Repositories
{
    public interface IBlogRepository : IManageRepository<Blog>
    {
    }
}
