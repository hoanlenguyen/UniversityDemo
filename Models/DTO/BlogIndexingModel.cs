using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityDemo.Models.DTO
{
    public class BlogIndexingModel
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public int PostCount 
        { 
            get { return Posts.Count; } 
        }
        public int TotalViews 
        {
            get { return Posts.Sum(q => q.Views); }
        }
        public List<PostIndexingModel> Posts { get; set; } = new List<PostIndexingModel>();
    }
}
