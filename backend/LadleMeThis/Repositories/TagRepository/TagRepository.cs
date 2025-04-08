using LadleMeThis.Context;
using LadleMeThis.Data.Entity;
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


        public async Task<Tag> AddAsync(Tag tag)
        {
            await _dbContext.Tags.AddAsync(tag);
            await _dbContext.SaveChangesAsync();
            return tag;
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

        public async Task<Tag> GetByIdAsync(int tagId)
        {
            var tag = await _dbContext.Tags.FindAsync(tagId);
            return tag ?? throw new KeyNotFoundException("No tag was found with given Id!");
        }

        public async Task<IEnumerable<Tag>> GetManyByIdAsync(int[] tagIds)
        {
            var tags = await _dbContext.Tags.Where(tag => tagIds.Contains(tag.TagId)).ToListAsync();
            if (tags.Count == 0)
            {
                throw new KeyNotFoundException("Not a single tag was found!");
            }
            return tags;
        }
    }
}
