using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using UniversityDemo.BaseEntities;

namespace UniversityDemo.Models
{
    public class Post : BaseEntity
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "views")]
        public int Views { get; set; } = 0;

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        //public string Excerpt { get; set; }
        [JsonProperty(PropertyName = "coverImagePath")]
        public string CoverImagePath { get; set; }

        [JsonProperty(PropertyName = "public")]
        [DefaultValue(true)]
        public bool Public { get; set; } = true;

        [JsonProperty(PropertyName = "tags")]
        public List<string> Tags { get; set; } = new List<string>();

        [JsonProperty(PropertyName = "blogId")]
        public string BlogId { get; set; }
    }
}