using M223PunchclockDotnet.Dto;
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
        public async Task<List<EntryDto>> FindAll()
        {
            var entries = await _databaseContext.Entries
                .Include(entry => entry.Category)
                .Include(entry => entry.Tags)
                .ToListAsync();

            var entryDtos = entries.Select(entry => new EntryDto
            {
                Id = entry.Id,
                CheckIn = entry.CheckIn,
                CheckOut = entry.CheckOut,
                CategoryId = entry.CategoryId,
                TagIds = entry.Tags?.Select(tag => tag.Id).ToList() ?? new()
            }).ToList();

            return entryDtos;
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
