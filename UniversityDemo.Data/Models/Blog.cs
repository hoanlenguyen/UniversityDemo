using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UniversityDemo.Data.BaseEntities;
using UniversityDemo.Data.Enum;

namespace UniversityDemo.Data.Models
{
    public class Blog : BaseEntity
    {
        [JsonProperty(PropertyName = "url")]
        [MaxLength((int)Length.Url)]
        public string Url { get; set; }
    }
}