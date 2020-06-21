using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityDemo.Data;
using UniversityDemo.Models;

namespace UniversityDemo.Service
{
    public class StudentsService : IStudentsService
    {
        private readonly SchoolContext _context;

        public StudentsService(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetAllEnrollments()
        {
            return await _context.Enrollments.ToListAsync();
        }
    }
}