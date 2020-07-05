using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UniversityDemo.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseID { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}