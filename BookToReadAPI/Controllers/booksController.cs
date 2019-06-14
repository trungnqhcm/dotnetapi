using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookToReadAPI.Models;

namespace BookToReadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class booksController : ControllerBase
    {
        private readonly BookToReadAPIContext _context;

        public booksController(BookToReadAPIContext context)
        {
            _context = context;

            if (_context.book.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.book.Add(new book { name = "harry potter 1" });
                _context.SaveChanges();
            }
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<book>>> Getbook()
        {
            return await _context.book.ToListAsync();
        }

        // GET: api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<book>> Getbook(long id)
        {
            var book = await _context.book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/books/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Putbook(long id, book book)
        {
            if (id != book.id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<book>> Postbook(book book)
        {
            _context.book.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getbook", new { id = book.id }, book);
        }

        // DELETE: api/books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<book>> Deletebook(long id)
        {
            var book = await _context.book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.book.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        private bool bookExists(long id)
        {
            return _context.book.Any(e => e.id == id);
        }
    }
}
