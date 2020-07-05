using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.Data;
using UniversityDemo.Models;

namespace UniversityDemo.Services
{
    public class StudentService
    {
        private readonly DemoDbContext context;

        public StudentService(DemoDbContext context)
        {
            this.context = context;
        }

        public async Task<Student> Get(int id)
        {
            return context.Students
                            .Include(p=>p.Enrollments)
                            .SingleOrDefault(p => p.ID == id);
        }
    }
}