using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityDemo.Models;

namespace UniversityDemo.Service
{
    public interface IStudentsService
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<IEnumerable<Enrollment>> GetAllEnrollments();
    }
}