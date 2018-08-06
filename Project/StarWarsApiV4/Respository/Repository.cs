using Microsoft.EntityFrameworkCore;
using StarWarsApiV4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StarWarsApiV4.Respository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SWContext context;
        private DbSet<T> entities;


        public Repository(SWContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IEnumerable<T> getAll()
        {

            return entities;
        }

        public T get(int id)
        {
            return entities.Find(id);
        }


        public T insert(T obj)
        {
            entities.Add(obj);
            context.SaveChanges();
            return obj;

        }

        public T put(T obj)
        {
            entities.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
            return obj;
        }

      

        public void delete(int id)
        {
            entities.Remove(entities.Find(id));
            throw new NotImplementedException();
        }

       
    }
}
