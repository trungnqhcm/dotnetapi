using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookToReadAPI.Models
{
    public class BookToReadAPIContext : DbContext
    {
        public BookToReadAPIContext (DbContextOptions<BookToReadAPIContext> options)
            : base(options)
        {
        }

        public DbSet<BookToReadAPI.Models.book> book { get; set; }
    }
}
