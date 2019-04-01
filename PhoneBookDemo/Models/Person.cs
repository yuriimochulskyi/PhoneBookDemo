using System;
using System.Collections.Generic;

namespace PhoneBookDemo.Models
{
    public partial class Person
    {
        public Person()
        {
            Information = new HashSet<Information>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Information> Information { get; set; }
    }
}
