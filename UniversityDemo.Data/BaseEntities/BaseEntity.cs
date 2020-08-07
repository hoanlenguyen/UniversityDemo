using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UniversityDemo.Data.BaseEntities
{
    public abstract class BaseEntity : IEntity<string>
    {
        protected BaseEntity()
        {
        }

        [JsonProperty(PropertyName = "id")]
        [Key]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "meta", NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(null)]
        public Meta Meta { get; set; } = new Meta();

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}