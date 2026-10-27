using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace M223PunchclockDotnet.Service
{
    public class TagService
    {
        private DatabaseContext _databaseContext; 

        public TagService(DatabaseContext databaseContext){
            _databaseContext = databaseContext;
        }
        public Task<List<Tag>> FindAll()
        {
            return _databaseContext.Tags.ToListAsync();
        }

        public async Task<Tag> AddTag(Tag Tag)
        {
            _databaseContext.Tags.Add(Tag);
            await _databaseContext.SaveChangesAsync();

            return Tag;
        }

        public async Task<Tag?> FindById(int id)
        {
            return await _databaseContext.Tags.FindAsync(id);
        }

        public async Task DeleteTag(Tag Tag)
        {
            _databaseContext.Tags.Remove(Tag);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<Tag> UpdateTag(Tag Tag)
        {
            _databaseContext.Tags.Update(Tag);
            await _databaseContext.SaveChangesAsync();

            return Tag;
        }
    }
}
