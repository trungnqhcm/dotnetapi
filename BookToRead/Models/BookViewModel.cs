using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookToRead.Models
{
    public class BookViewModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public bool done { get; set; }
    }
}
