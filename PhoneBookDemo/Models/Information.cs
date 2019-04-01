using System;
using System.Collections.Generic;

namespace PhoneBookDemo.Models
{
    public partial class Information
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string PhoneNumber { get; set; }

        public Person Person { get; set; }
    }
}
