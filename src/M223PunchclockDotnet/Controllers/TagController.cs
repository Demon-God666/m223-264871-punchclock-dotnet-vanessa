using System.Net.Mime;
using M223PunchclockDotnet.Model;
using M223PunchclockDotnet.Service;
using Microsoft.AspNetCore.Mvc;

namespace M223PunchclockDotnet.Controllers
{
    [ApiController]
    [Route("tags")]
    public class TagController : ControllerBase
    {
        private readonly TagService _tagService;

        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        [ProducesResponseType<Tag>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var allEntries = await _tagService.FindAll();
            return Ok(allEntries);
        }

        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<Tag>(StatusCodes.Status201Created)]
        public async Task<ActionResult<Tag>> AddTag(Tag tag){
            var newElement = await _tagService.AddTag(tag);

            return CreatedAtAction(nameof(Get), new{id = tag.Id}, tag);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTag(int id)
        {
            var Tag = await _tagService.FindById(id);
            if (Tag == null)
            {
                return NotFound();
            }

            await _tagService.DeleteTag(Tag);
            return Ok();
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Tag>> UpdateTag(int id, Tag updatedTag)  

        {
                var Tag = await _tagService.FindById(id);
                if (Tag == null)
                {
                    return NotFound();
                }
    
                Tag.Title = updatedTag.Title;
                Tag.Entries = updatedTag.Entries;
    
                await _tagService.UpdateTag(Tag);
    
                return Ok(Tag);
            }


    }
}
