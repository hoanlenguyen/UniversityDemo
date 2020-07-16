using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UniversityDemo.BaseEntities;
using UniversityDemo.Enum;

namespace UniversityDemo.Models
{
    public class Blog : BaseEntity
    {
        [JsonProperty(PropertyName = "url")]
        [MaxLength((int)Length.Url)]
        public string Url { get; set; }

        //[JsonProperty(PropertyName = "postIds")]
        //[DefaultValue(null)]
        //public List<string> PostIds { get; set; } = new List<string>();

        #region reference

        [JsonIgnore]
        [DefaultValue(null)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual IEnumerable<Post> Posts { get; set; } = new List<Post>();

        #endregion reference
    }
}