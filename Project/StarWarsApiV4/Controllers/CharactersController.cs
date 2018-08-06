using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StarWarsApiV4.Models;
using StarWarsApiV4.Pagination;

namespace StarWarsApiV4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly SWContext _context;
        private readonly IMapper _mapper;

        public CharactersController(SWContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

           
        }

        // GET: api/Characters
        [HttpGet]
        public IEnumerable<ApiModelCharacter> GetCharacters(PagingParametrs customQueryParameters)
        {
            var characters = _context.Characters.Skip(customQueryParameters.PageCount * (customQueryParameters.Page - 1))
                .Take(customQueryParameters.PageCount).Include(che => che.CharacterEpisodes).
                 ThenInclude(e => e.Episode).
                 Include(f => f.MainCharacterFriends).
                 ThenInclude(ff => ff.FriendCharacter).ToList();


            var dest = characters.Select(s => _mapper.Map<ApiModelCharacter>(s));

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(new { totalCount = _context.Characters.Count() }));
            return dest;
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public IActionResult GetCharacter([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var context = _context.Characters.Include(che => che.CharacterEpisodes).ThenInclude(e => e.Episode).Include(f=>f.MainCharacterFriends).ThenInclude(ff=>ff.FriendCharacter);
            var character = context.Where(ch => ch.CharacterID == id).First();

            if (character == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<Character, ApiModelCharacter>(character);

            return Ok(result);
        }

        // PUT: api/Characters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter([FromRoute] int id, [FromBody] Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != character.CharacterID)
            {
                return BadRequest();
            }

            _context.Entry(character).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
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

        // POST: api/Characters
        [HttpPost]
        public async Task<IActionResult> PostCharacter([FromBody] Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacter", new { id = character.CharacterID }, character);
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            return Ok(character);
        }

        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.CharacterID == id);
        }
    }
}