using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookServer.Models;

namespace BookServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookRepository _bookRepo { get; set; }

        public BooksController(IBookRepository repo)
        {
            _bookRepo = repo;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var bookList = await _bookRepo.GetAll();
            return Ok(bookList);
        }

        // GET: api/Books/5
        [HttpGet("{isbn}")]
        public async Task<IActionResult> GetBook([FromRoute] string isbn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            isbn = isbn.ToUpperInvariant();
            var book = await _bookRepo.Find(isbn);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        [HttpPut("{isbn}")]
        public async Task<IActionResult> PutBook([FromRoute] string isbn, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(book.ISBN))
            {
                return BadRequest();
            }

            //_context.Entry(book).State = EntityState.Modified;

            var updateBook = await _bookRepo.Find(isbn);
            if (updateBook == null)
            {
                return NotFound();
            }

            try
            {
                await _bookRepo.Update(book);
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(isbn))
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

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> PostBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrWhiteSpace(book.ISBN))
            {
                return BadRequest();
            }

            try
            {
                await _bookRepo.Add(book);
                //_context.Books.Add(book);
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookExists(book.ISBN))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBook", new { isbn = book.ISBN }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{isbn}")]
        public async Task<IActionResult> DeleteBook([FromRoute] string isbn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var book = await _context.Books.FindAsync(id);
            var book = await _bookRepo.Find(isbn);
            if (book == null)
            {
                return NotFound();
            }

            //_context.Books.Remove(book);
            //await _context.SaveChangesAsync();
            await _bookRepo.Remove(isbn);

            return Ok(book);
        }

        private bool BookExists(string isbn)
        {
            var book = _bookRepo.Find(isbn);
            //return _context.Books.Any(e => e.Isbn == id);
            return book != null;
        }
    }
}
