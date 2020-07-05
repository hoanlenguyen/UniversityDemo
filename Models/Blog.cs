using System.ComponentModel.DataAnnotations;
using UniversityDemo.BaseEntities;
using UniversityDemo.Enum;

namespace UniversityDemo.Models
{
    public class Blog : BaseEntity
    {
        [MaxLength((int)Length.Url)]
        public string Url { get; set; }
    }
}
