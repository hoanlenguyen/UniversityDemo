using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.Data;
using UniversityDemo.Models;

namespace UniversityDemo.Repositories.Internal
{
    internal class BlogRepository : IBlogRepository
    {
        private readonly DemoDbContext context;
        public BlogRepository(DemoDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeleteAsync(params string[] ids)
        {
            if (ids.Length == 0)
                return false;
            foreach (var id in ids)
            {
                var entity = context.Blogs.FirstOrDefault(x => x.Id.Equals(id));
                context.Blogs.Remove(entity);
            }
            await context.SaveChangesAsync();
            return true;
        }

        public Task<List<Blog>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Blog> FindOneByIdAsync(string id)
        {
            return context.Blogs.SingleOrDefault(x => x.Id.ToString().Equals(id));
        }

        public async Task<Blog> InsertAsync(Blog item)
        {
            item.Id = new Guid();
            var x=  await context.Blogs.AddAsync(item);
            await context.SaveChangesAsync();
            return x.Entity;
        }

        public Task<Blog> UpdateAsync(Blog item)
        {
            throw new NotImplementedException();
        }
    }
}
