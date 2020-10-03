using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UniversityDemo.Data.BaseEntities;
using UniversityDemo.Data.Enum;

namespace UniversityDemo.Data.Models
{
    public class Blog : BaseEntity
    {
        [JsonProperty(PropertyName = nameof(Name))]
        [MaxLength(Limitation.Name)]
        public string Name { get; set; }
    }
}