using System;
using System.Collections.Generic;

namespace BookServer.Models
{
    public partial class Author
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
    }
}
