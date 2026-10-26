using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace M223PunchclockDotnet.Service
{
    public class EntryService
    {
        private DatabaseContext _databaseContext;

        public EntryService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public Task<List<Entry>> FindAll()
        {
            return _databaseContext.Entries.ToListAsync();
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

        public async Task<bool> UpdateEntry(int id, Entry entry)
        {
            var existingEntry = await _databaseContext.Entries.FindAsync(id);

            if (existingEntry == null)
            {
                return false;
            }

            existingEntry.CheckIn = entry.CheckIn;
            existingEntry.CheckOut = entry.CheckOut;

            await _databaseContext.SaveChangesAsync();

            return true;
        }
    }
}
