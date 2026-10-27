using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using M223PunchclockDotnet.Dto;
using M223PunchclockDotnet.Model;

namespace M223PunchclockDotnet.Controllers
{
    [ApiController]
    [Route("entry-details")]
    public class EntryDetailController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public EntryDetailController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<EntryDetailDto>>> GetAll()
        {
            var entries = await _context.Entries
                .Include(entry => entry.Category)
                .Include(entry => entry.Tags)
                .ToListAsync();

            var result = entries.Select(entry => new EntryDetailDto
            {
                Id = entry.Id,
                CheckIn = entry.CheckIn,
                CheckOut = entry.CheckOut,

                Category = entry.Category == null ? null : new CategoryDto
                {
                    Id = entry.Category.Id,
                    Title = entry.Category.Title
                },

                Tags = entry.Tags?.Select(tag => new TagDto
                {
                    Id = tag.Id,
                    Title = tag.Title
                }).ToList() ?? new()
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EntryDetailDto>> GetById(int id)
        {
            var entry = await _context.Entries
                .Include(entry => entry.Category)
                .Include(entry => entry.Tags)
                .FirstOrDefaultAsync(entry => entry.Id == id);

            if (entry == null)
            {
                return NotFound();
            }

            var result = new EntryDetailDto
            {
                Id = entry.Id,
                CheckIn = entry.CheckIn,
                CheckOut = entry.CheckOut,

                Category = entry.Category == null ? null : new CategoryDto
                {
                    Id = entry.Category.Id,
                    Title = entry.Category.Title
                },

                Tags = entry.Tags?.Select(tag => new TagDto
                {
                    Id = tag.Id,
                    Title = tag.Title
                }).ToList() ?? new()
            };

            return Ok(result);
        }
    }
}