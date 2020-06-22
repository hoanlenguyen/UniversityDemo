using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            return await _context.Students
                .Include(p=>p.Enrollments)
                .ThenInclude(q => q.Course)
                .ToListAsync();
        }

        //public async Task<IEnumerable<Enrollment>> GetAllEnrollments()
        //{
        //    return await _context.Enrollments.ToListAsync();
        //}

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task UpdateStudentAsync([FromBody]Student student)
        {
            var entity = _context.Students
                .AsNoTracking()
                .FirstOrDefault(p => p.ID == student.ID);
            if (entity != null)
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }           
        }

        public async Task CreateStudentAsync([FromBody]Student student)
        {
            await _context.AddAsync(student);
            await _context.SaveChangesAsync();
        }

    }
}