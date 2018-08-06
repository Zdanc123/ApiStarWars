using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StarWarsApiV4.Respository
{
    interface IRepository < T > where T : class
    {
        IEnumerable<T> getAll();
        T get(int id);
        T insert(T obj);
        T put(T obj);
        void delete(int id);

    }
}
