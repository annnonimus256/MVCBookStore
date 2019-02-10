using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BookModel
    {
        public int UserId { get; set; }

        public string Name { get; set; }
        public string UserName { get; set; }

        public string Price { get; set; }

        public string Autor { get; set; }
        public int Id { get; set; }

    }
}