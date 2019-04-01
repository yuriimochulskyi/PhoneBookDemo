using Microsoft.EntityFrameworkCore;
using PhoneBookDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookDemo.DAL
{
    public class PersonRepository : IRepository<Person>
    {
        private PhoneBookLiteDBContext context;
        public PersonRepository(PhoneBookLiteDBContext context)
        {
            this.context = context;
        }

        public void Delete(object id)
        {
            Person person = context.Person.Find(id);
            context.Person.Remove(person);
        }

        public void Create(Person obj)
        {
            context.Person.Add(obj);
        }

        public IEnumerable<Person> SelectAll()
        {
            return context.Person.ToList();
        }

        public async Task<Person> SelectByID(object id)
        {
            return await context.Person.FindAsync(id);
        }

        public void Update(Person obj)
        {
            context.Entry(obj).State = EntityState.Modified;
        }
    }
}
