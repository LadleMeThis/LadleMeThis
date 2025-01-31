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


        public async Task AddAsync(Tag tag)
        {
            await _dbContext.Tags.AddAsync(tag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int tagId)
        {
            Tag tag = new() { TagId = tagId };

            _dbContext.Entry(tag).State = EntityState.Deleted;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _dbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(int tagId)
        {
            return await _dbContext.Tags.FindAsync(tagId);
        }

        public async Task<IEnumerable<Tag>> GetManyByIdAsync(int[] tagIds)
        {
            return await _dbContext.Tags.Where(tag => tagIds.Contains(tag.TagId)).ToListAsync();
        }
    }
}
