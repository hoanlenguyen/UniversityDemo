using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityDemo.Models;

namespace UniversityDemo.Services
{
    public interface ICourseService
    {
        Task Create(List<Course> courses);
    }
}