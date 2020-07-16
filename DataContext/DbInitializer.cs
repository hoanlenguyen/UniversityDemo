using System.Linq;
using UniversityDemo.Models;

namespace UniversityDemo.Data
{
    public class DbInitializer
    {
        public static void Initialize(DemoDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Enrollments.Any())
            {
                return;   // DB has been seeded
            }

            //var students = new Student[]
            //{
            //new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            //new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            //new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            //new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            //new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            //new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            //new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            //new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            //};
            //foreach (Student s in students)
            //{
            //    context.Students.Add(s);
            //}
            //context.SaveChanges();

            //var courses = new Course[]
            //{
            //new Course{Title="Chemistry",Credits=3},
            //new Course{Title="Microeconomics",Credits=3},
            //new Course{Title="Macroeconomics",Credits=3},
            //new Course{Title="Calculus",Credits=4},
            //new Course{Title="Trigonometry",Credits=4},
            //new Course{Title="Composition",Credits=3},
            //new Course{Title="Literature",Credits=4}
            //};
            //foreach (Course c in courses)
            //{
            //    context.Courses.Add(c);
            //}
            //context.AddRange(courses);
            //context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentId=1,CourseId=1,Grade=Grade.A},
            new Enrollment{StudentId=1,CourseId=3,Grade=Grade.C},
            new Enrollment{StudentId=1,CourseId=4,Grade=Grade.B},
            new Enrollment{StudentId=2,CourseId=4,Grade=Grade.B},
            new Enrollment{StudentId=2,CourseId=5,Grade=Grade.F},
            new Enrollment{StudentId=2,CourseId=3,Grade=Grade.F},
            new Enrollment{StudentId=3,CourseId=1},
            new Enrollment{StudentId=4,CourseId=3},
            new Enrollment{StudentId=4,CourseId=6,Grade=Grade.F},
            new Enrollment{StudentId=5,CourseId=7,Grade=Grade.C},
            new Enrollment{StudentId=6,CourseId=7},
            new Enrollment{StudentId=7,CourseId=2,Grade=Grade.A},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }
    }
}