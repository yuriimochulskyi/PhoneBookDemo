using PhoneBookDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookDemo.DAL
{
    public class UnitOfWork
    {
        private PhoneBookLiteDBContext context;
        private PersonRepository personRepository;
        private InformationRepository informationRepository;

        public UnitOfWork()
        {
        }

        public UnitOfWork(PhoneBookLiteDBContext context)
        {
            this.context = context;
        }

        public PersonRepository PersonRepository
        {
            get
            {
                if (this.personRepository == null)
                {
                    this.personRepository = new PersonRepository(context);
                }
                return personRepository;
            }
        }

        public InformationRepository InformationRepository
        {
            get
            {
                if (this.informationRepository == null)
                {
                    this.informationRepository = new InformationRepository(context);
                }
                return informationRepository;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }
    }
}
