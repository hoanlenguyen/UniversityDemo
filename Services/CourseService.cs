using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityDemo.Data;
using UniversityDemo.Models;

namespace UniversityDemo.Services
{
    public class CourseService : ICourseService
    {
        private readonly DemoDbContext context;

        public CourseService(DemoDbContext context)
        {
            this.context = context;
        }

        public async Task Create(List<Course> courses)
        {
            await context.AddRangeAsync(courses);
            await context.SaveChangesAsync();
        }
    }
}