using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWars.Models;
using StarWars.ModelsDto;
using StarWars.QueryParametrs;
using StarWars.Repository;

namespace StarWars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IRepository<Episode> _episodesRepository;

        public EpisodesController(IMapper mapper, IRepository<Episode> episodesRepository)
        {
            _mapper = mapper;
            _episodesRepository = episodesRepository;
        }

        // GET: api/Episodes
        [HttpGet]
        public IActionResult GetEpisodes(int page = 1, int pagecount = 100)
        {
            var episodes = _episodesRepository.getAll(page,pagecount).Include(che => che.CharacterEpisodes).ThenInclude(ch => ch.Character);
            var result = episodes.Select(s=> _mapper.Map<EpisodeDto>(s));
            return  Ok(result);
        }

        // GET: api/Episodes/5
        [HttpGet("{id}")]
        public IActionResult GetEpisode([FromRoute] int id)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var episode = _episodesRepository.get(id, "CharacterEpisodes.Character");
            if (episode == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<EpisodeDto>(episode);
            return Ok(result);
        }
        // POST: api/Episodes
        [HttpPost]
        public IActionResult PostEpisode([FromBody] Episode episode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _episodesRepository.insert(episode);
            var result = _episodesRepository.save();
            if (!result)
            {
                return new StatusCodeResult(500);
            }

            
            
            return CreatedAtAction("GetEpisode", new { id = episode.Id }, episode);
        }

        // PUT: api/Episodes/5
        [HttpPut("{id}")]
        public IActionResult PutEpisode([FromRoute] int id, [FromBody] EpisodeUpdateDto episodeUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingEpisode = _episodesRepository.get(id, null);
            if (existingEpisode == null)
            {
                return NotFound();
            }

            if (episodeUpdate==null)
            {
                return BadRequest();
            }
            if (episodeUpdate.Name != existingEpisode.Name)
            {
                existingEpisode.Name = episodeUpdate.Name;
            }
            var result = _episodesRepository.save();
            if (!result)
            {
                return new StatusCodeResult(500);
            }

           
            return CreatedAtAction("GetEpisode", new { id = existingEpisode.Id }, existingEpisode);
            
        }

        // DELETE: api/Episodes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEpisode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var episodeToDelete = _episodesRepository.get(id,null);
            if(episodeToDelete==null)
            {
                return NotFound();
            }

            _episodesRepository.delete(id);
            var result = _episodesRepository.save();
            if (!result)
            {
                return new StatusCodeResult(500);
            }
            return Ok(episodeToDelete);
        }





    }
}