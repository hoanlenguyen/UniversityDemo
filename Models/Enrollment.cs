using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDemo.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        #region reference

        [ForeignKey(nameof(CourseID))]
        public virtual Course Course { get; set; }

        [ForeignKey(nameof(StudentID))]
        public virtual Student Student { get; set; }

        #endregion

    }
}