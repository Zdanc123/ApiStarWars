using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWars.Models;

namespace StarWars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterEpisodesController : ControllerBase
    {
        private readonly StarWarsContext _context;

        public CharacterEpisodesController(StarWarsContext context)
        {
            _context = context;
        }

        // GET: api/CharacterEpisodes
        [HttpGet]
        public IEnumerable<CharacterEpisode> GetCharacterEpisodes()
        {
            return _context.CharacterEpisodes;
        }

        // GET: api/CharacterEpisodes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacterEpisode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var characterEpisode = await _context.CharacterEpisodes.FindAsync(id);

            if (characterEpisode == null)
            {
                return NotFound();
            }

            return Ok(characterEpisode);
        }

        // PUT: api/CharacterEpisodes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacterEpisode([FromRoute] int id, [FromBody] CharacterEpisode characterEpisode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != characterEpisode.Id)
            {
                return BadRequest();
            }

            _context.Entry(characterEpisode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterEpisodeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CharacterEpisodes
        [HttpPost]
        public async Task<IActionResult> PostCharacterEpisode([FromBody] CharacterEpisode characterEpisode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CharacterEpisodes.Add(characterEpisode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacterEpisode", new { id = characterEpisode.Id }, characterEpisode);
        }

        // DELETE: api/CharacterEpisodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacterEpisode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var characterEpisode = await _context.CharacterEpisodes.FindAsync(id);
            if (characterEpisode == null)
            {
                return NotFound();
            }

            _context.CharacterEpisodes.Remove(characterEpisode);
            await _context.SaveChangesAsync();

            return Ok(characterEpisode);
        }

        private bool CharacterEpisodeExists(int id)
        {
            return _context.CharacterEpisodes.Any(e => e.Id == id);
        }
    }
}