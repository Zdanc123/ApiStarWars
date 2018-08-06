using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWarsApiV4.DTO;
using StarWarsApiV4.Models;

namespace StarWarsApiV4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private readonly SWContext _context;
        private readonly IMapper _mapper;

        public EpisodesController(SWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Episodes
        [HttpGet]
        public IEnumerable<EpisodeDTO> GetEpisodes()
        {
            var context = _context.Episodes.Include(c=>c.CharacterEpisodes).ThenInclude(ch=>ch.Character).ToList();
            var dst = context.Select(s => _mapper.Map<EpisodeDTO>(s));
            return dst;

        }

        // GET: api/Episodes/5
        [HttpGet("{id}")]
        public IActionResult GetEpisode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var context = _context.Episodes.Include(che => che.CharacterEpisodes).ThenInclude(ch => ch.Character);
            var episode = context.SingleOrDefault(x => x.EpisodeID == id);
            var dst = _mapper.Map<EpisodeDTO>(episode);

            if (episode == null)
            {
                return NotFound();
            }

            return Ok(dst);
        }

        // PUT: api/Episodes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEpisode([FromRoute] int id, [FromBody] Episode episode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != episode.EpisodeID)
            {
                return BadRequest();
            }

            _context.Entry(episode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpisodeExists(id))
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

        // POST: api/Episodes
        [HttpPost]
        public async Task<IActionResult> PostEpisode([FromBody] Episode episode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Episodes.Add(episode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEpisode", new { id = episode.EpisodeID }, episode);
        }

        // DELETE: api/Episodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpisode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var episode = await _context.Episodes.FindAsync(id);
            if (episode == null)
            {
                return NotFound();
            }

            _context.Episodes.Remove(episode);
            await _context.SaveChangesAsync();

            return Ok(episode);
        }

        private bool EpisodeExists(int id)
        {
            return _context.Episodes.Any(e => e.EpisodeID == id);
        }
    }
}