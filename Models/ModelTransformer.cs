using UniversityDemo.Data.Models;
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
                Url = entity.Name
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
                BlogId = entity.BlogId
            };
        }
    }
}