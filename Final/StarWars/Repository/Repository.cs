using Microsoft.EntityFrameworkCore;
using StarWars.Models;
using StarWars.QueryParametrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StarWars.Repository
{
    public class Repository<T>: IRepository <T> where T :class, IBaseModel
    {
        protected StarWarsContext context;
        protected DbSet<T> entities;
        public Repository(StarWarsContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }


        //public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        //{
        //    IQueryable<T> query = null;
        //    foreach (var include in includes)
        //    {
        //        query = entities.Include(include);
        //    }

        //    return query ?? entities;
        //}

    


        public IQueryable<T> getAll(int page, int pageCount)
        {
            return entities.Skip(pageCount * (page - 1))
                .Take(pageCount);
        }

        public T get(int id, string includeProperties)
        {
            IQueryable<T> query = entities;

            includeProperties = includeProperties ?? string.Empty;

            foreach (var includeProperty in includeProperties.Split
                          (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.SingleOrDefault(x=>x.Id==id);
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
            return obj;
        }

        public void delete(int id)
        {
            entities.Remove(entities.Find(id));
          
        }
        public int Count()
        {
           return entities.Count();
        }

        public bool save()
        {
            return context.SaveChanges() >= 0;
        }
    }
}
