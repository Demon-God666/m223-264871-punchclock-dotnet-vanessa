using System.Net.Mime;
using System.Security.Claims;
using M223PunchclockDotnet.Dto;
using M223PunchclockDotnet.Model;
using M223PunchclockDotnet.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace M223PunchclockDotnet.Controllers
{
    [ApiController]
    [Route("entries")]
    public class EntryController : ControllerBase
    {
        private readonly EntryService _entryService;

        public EntryController(EntryService entryService)
        {
            _entryService = entryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<EntryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var allEntries = await _entryService.FindAll();

            var result = allEntries.Select(entry => new EntryDto
            {
                Id = entry.Id,
                CheckIn = entry.CheckIn,
                CheckOut = entry.CheckOut,
                CategoryId = entry.CategoryId,
                TagIds = entry.TagIds,
                UserId = entry.UserId,
                Description = entry.Description
            });

            return Ok(result);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(EntryDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<EntryDto>> AddEntry(EntryDto dto)
        {
            var entry = new Entry
            {
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
                CategoryId = dto.CategoryId,
                Description = dto.Description
            };

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null &&
                int.TryParse(userIdClaim.Value, out int userId))
            {
                entry.UserId = userId;
            }

            var newElement = await _entryService.AddEntry(entry);

            var result = new EntryDto
            {
                Id = newElement.Id,
                CheckIn = newElement.CheckIn,
                CheckOut = newElement.CheckOut,
                CategoryId = newElement.CategoryId,
                UserId = newElement.UserId,
                Description = newElement.Description
            };

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteEntry(int id)
        {
            var entry = await _entryService.FindById(id);

            if (entry == null)
            {
                return NotFound();
            }

            await _entryService.DeleteEntry(entry);

            return Ok();
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(EntryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EntryDto>> UpdateEntry(
            int id, EntryDto dto)
        {
            var entry = await _entryService.FindById(id);

            if (entry == null)
            {
                return NotFound();
            }

            entry.CheckIn = dto.CheckIn;
            entry.CheckOut = dto.CheckOut;
            entry.CategoryId = dto.CategoryId;
            entry.Description = dto.Description;

            await _entryService.UpdateEntry(entry);

            var result = new EntryDto
            {
                Id = entry.Id,
                CheckIn = entry.CheckIn,
                CheckOut = entry.CheckOut,
                CategoryId = entry.CategoryId,
                UserId = entry.UserId,
                Description = entry.Description
            };

            return Ok(result);
        }
    }
}