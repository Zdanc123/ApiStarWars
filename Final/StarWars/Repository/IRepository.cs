
using StarWars.QueryParametrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StarWars.Repository
{
    public interface IRepository<T> where T : class, Models.IBaseModel
    {
     
        IQueryable<T> getAll(int page, int pagecount);
        T get(int id, string includeProperties);
        T insert(T obj);
        T put(T obj);
        void delete(int id);
        bool save();
        int Count();

    }
}
