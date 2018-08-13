using Microsoft.EntityFrameworkCore;
using StarWars.Models;
using StarWars.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StarWars.Repository
{
    public class CharacterRepository : IRepository<Character>
    {
        protected StarWarsContext context;
        protected DbSet<Character> entities;
        public CharacterRepository(StarWarsContext context)
        {
            this.context = context;
            entities = context.Set<Character>();
        }

      

        public Character get(int id, string includeProperties)
        {
            var characters =entities.Include(che => che.CharacterEpisodes).ThenInclude(e => e.Episode).Include(f => f.MainCharacterFriends).ThenInclude(ff => ff.FriendCharacter);
            return characters.Where(x => x.Id == id).First();
        }

        public IQueryable<Character> getAll()
        {
            return entities ;
        }

        public Character insert(Character obj)
        {
            entities.Add(obj);
            context.SaveChanges();
            return obj;
        }

        public Character put(Character obj)
        {
            entities.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
            return obj;
        }

        public void delete(int id)
        {
            entities.Remove(entities.Find(id));
            context.SaveChanges();
        }
        public int Count()
        {
            return entities.Count();
        }

        
    }







}

