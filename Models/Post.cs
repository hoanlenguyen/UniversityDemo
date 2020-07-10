using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.BaseEntities;

namespace UniversityDemo.Models
{
    public class Post : BaseEntity
    {
        [JsonProperty(PropertyName = "url")]
        public string Title { get; set; }
        public int Views { get; set; } = 0;
        public string Content { get; set; }
        public string Excerpt { get; set; }
        public string CoverImagePath { get; set; }
        public bool Public { get; set; }
        public int BlogId { get; set; }

    }
}
