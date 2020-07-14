using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.Models.DTO;

namespace UniversityDemo.Models
{
    public static class ModelTransformer
    {
        public static BlogIndexingModel ToIndexingModel(this Blog entity)
        {
            return new BlogIndexingModel
            {
                Id = entity.Id,
                Url = entity.Url
            };
        }

        public static PostIndexingModel ToIndexingModel(this Post entity)
        {
            return new PostIndexingModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Views = entity.Views,
                CoverImagePath = entity.CoverImagePath,
                BlogId=entity.BlogId
            };
        }
    }
}
