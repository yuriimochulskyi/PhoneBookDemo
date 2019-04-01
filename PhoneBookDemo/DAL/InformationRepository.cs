using Microsoft.EntityFrameworkCore;
using PhoneBookDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookDemo.DAL
{
    public class InformationRepository : IRepository<Information>
    {
        private PhoneBookLiteDBContext context;
        public InformationRepository(PhoneBookLiteDBContext context)
        {
            this.context = context;
        }

        public void Create(Information obj)
        {
            context.Information.Add(obj);
        }

        public void Delete(object id)
        {
            Information information = context.Information.Find(id);
            context.Information.Remove(information);
        }

        public IEnumerable<Information> SelectAll()
        {
            return context.Information.Include(i => i.Person).ToList();
        }

        public async Task<Information> SelectByID(object id)
        {
            return await context.Information.FindAsync(id);
        }

        public void Update(Information obj)
        {
            context.Entry(obj).State = EntityState.Modified;
        }
    }
}
