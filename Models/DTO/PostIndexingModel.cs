using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityDemo.Models.DTO
{
    public class PostIndexingModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int Views { get; set; }

        public string CoverImagePath { get; set; }

        public string BlogId { get; set; }
    }
}
