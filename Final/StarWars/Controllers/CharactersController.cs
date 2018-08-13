using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StarWars.Models;
using StarWars.ModelsDto;
using StarWars.QueryParametrs;
using StarWars.Repository;

namespace StarWars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
      
        private readonly IMapper _mapper;
        private IRepository<Character> _charactersRepository;
        public CharactersController( IMapper mapper, IRepository<Character> characterRepository)
        {
           
            _mapper = mapper;
            _charactersRepository = characterRepository;
           

        }

        // GET: api/Characters
        [HttpGet]
        public IActionResult GetCharacters(int page=1, int pagecount=100)
        {

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(new { totalCount = _charactersRepository.Count() }));
            var count = _charactersRepository.Count();
            var pages =(int) (count / pagecount);

            var characters = _charactersRepository.getAll(page,pagecount).
                Include(che => che.CharacterEpisodes).
                 ThenInclude(e => e.Episode).
                 Include(f => f.MainCharacterFriends).
                 ThenInclude(ff => ff.FriendCharacter).ToList() ;
            var result = characters.Select(s => _mapper.Map<CharacterDto>(s));

           
            return Ok(result);


        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public IActionResult GetCharacter([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            
            var character = _charactersRepository.get(id, "CharacterEpisodes.Episode,MainCharacterFriends.FriendCharacter");
            if (character == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<Character, CharacterDto>(character);

            return Ok(result);
        }
        //POST: api/Characters
        [HttpPost]
        public IActionResult PostCharacter([FromBody] Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var toAdd = _charactersRepository.insert(character);
            bool result = _charactersRepository.save();
            if (!result)
            {
                return new StatusCodeResult(500);
            }


            return CreatedAtAction("GetCharacter", new { id = character.Id }, _mapper.Map<CharacterDto>(toAdd));
        }

        // PUT: api/Characters/5
        [HttpPut("{id}"), ]
        public IActionResult PutCharacter([FromRoute] int id, [FromBody] CharacterUpdateDto characterUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCharacter = _charactersRepository.get(id,null);

            if(characterUpdate == null)
            {
                return BadRequest();
            }
            if (existingCharacter == null)
            {
                return NotFound();
            }
            

            if (characterUpdate.Name != null)
            {
                existingCharacter.Name = characterUpdate.Name;
            }
           

            _charactersRepository.put(existingCharacter);

            var result = _charactersRepository.save();
            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return CreatedAtAction("GetCharacter", new { id = existingCharacter.Id }, existingCharacter);
        }

       

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCharacter([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var character = _charactersRepository.get(id, null);
            if (character == null)
            {
                return NotFound();
            }

            _charactersRepository.delete(id);
            

            return Ok(character);
        }

        
    }
}