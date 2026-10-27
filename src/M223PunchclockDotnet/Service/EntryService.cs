using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace M223PunchclockDotnet.Service
{
    public class EntryService
    {
        private DatabaseContext _databaseContext; 

        public EntryService(DatabaseContext databaseContext){
            _databaseContext = databaseContext;
        }
        public Task<List<Entry>> FindAll()
        {
            var entries = _databaseContext.Entries.Include(entry => entry.Category).Include(entry => entry.Tags).ToListAsync();

            return entries;
        }

        public async Task<Entry> AddEntry(Entry entry)
        {
            _databaseContext.Entries.Add(entry);
            await _databaseContext.SaveChangesAsync();

            return entry;
        }

        public async Task<Entry?> FindById(int id)
        {
            return await _databaseContext.Entries.FindAsync(id);
        }

        public async Task DeleteEntry(Entry entry)
        {
            _databaseContext.Entries.Remove(entry);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<Entry> UpdateEntry(Entry entry)
        {
            _databaseContext.Entries.Update(entry);
            await _databaseContext.SaveChangesAsync();

            return entry;
        }
    }
}
