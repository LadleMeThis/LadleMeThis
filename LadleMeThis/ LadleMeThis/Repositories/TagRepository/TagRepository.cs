using LadleMeThis.Context;
using LadleMeThis.Models.TagModels;
using Microsoft.EntityFrameworkCore;

namespace LadleMeThis.Repositories.TagRepository
{
    public class TagRepository : ITagRepository
    {

        private LadleMeThisContext _dbContext;

        public TagRepository(LadleMeThisContext dbContext)
        {
            _dbContext = dbContext;
        }


        //optional
        public async Task AddAsync(Tag tag)
        {
            await _dbContext.Tags.AddAsync(tag);
            await _dbContext.SaveChangesAsync();
        }

        //optional
        public async Task DeleteAsync(int id)
        {
            var tag = await GetByIdAsync(id);
            if (tag != null)
            {
                _dbContext.Tags.Remove(tag);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _dbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _dbContext.Tags.FindAsync(id);
        }
    }
}
