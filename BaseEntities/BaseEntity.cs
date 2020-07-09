using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDemo.BaseEntities
{
    public abstract class BaseEntity : IEntity<string>
    {
        protected BaseEntity()
        {
        }

        [JsonProperty(PropertyName = "id")]
        [Key]
        public string Id { get; set; }

        public Meta Meta { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        
    }
}