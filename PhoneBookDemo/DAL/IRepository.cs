using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookDemo.DAL
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> SelectAll();

        Task<T> SelectByID(object id);

        void Create(T obj);

        void Update(T obj);

        void Delete(object id);
    }

}
