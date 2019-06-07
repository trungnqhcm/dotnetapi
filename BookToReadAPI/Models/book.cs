using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookToReadAPI.Models
{
    public class book
    {
        public long id { get; set; }
        public string name { get; set; }
        public bool done { get; set; }
    }
}
