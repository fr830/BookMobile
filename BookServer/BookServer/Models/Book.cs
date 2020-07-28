using System;
using System.Collections.Generic;

namespace BookServer.Models
{
    public partial class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime? PublishDate { get; set; }
        //public List<string> Authors { get; set; }
        public string Href => $"api/books/{ISBN}";
    }
}
