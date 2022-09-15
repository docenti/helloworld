using HelloWorld1.Infrastructure;
using HelloWorld1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloWorld1.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public BookController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(CancellationToken cancel)
        {
            return await _dbContext.Books.ToListAsync(cancel);
        }

        // GET: api/Book/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<Book>> GetBook(long id, CancellationToken cancel)
        {
            // var book = await _dbContext.Books.FindAsync(new object?[] { request.Id }, cancel); // I don't like this
            var book = await _dbContext.Books.SingleOrDefaultAsync(x=>x.Id == id, cancel);

            if (book == null)
                return NotFound();

            return book;
        }

        // PATCH: api/Book/5
        [HttpPatch("{id:long}")]
        public async Task<IActionResult> PutBook(long id, Book request, CancellationToken cancel)
        {
            var book = await _dbContext.Books.SingleOrDefaultAsync(x=> x.Id == request.Id, cancel);

            if (book == null)
                return NotFound();

            book.Title = request.Title;
            // book.AuthorRef = request.AuthorRef;
            book.PublishedOn = request.PublishedOn;
            book.ImageUrl = request.ImageUrl;
            book.Description = request.Description;

            await _dbContext.SaveChangesAsync(cancel);

            return NoContent();
        }

        // POST: api/Book
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book, CancellationToken cancel)
        {
            if (book.Id != default)
                return BadRequest();

            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync(cancel);

            return book;
        }

        // DELETE: api/Book/5
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteBook(long id, CancellationToken cancel)
        {
            var book = await _dbContext.Books.SingleOrDefaultAsync(x=> x.Id == id, cancel);

            if (book == null)
                return NotFound();

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync(cancel);

            return NoContent();
        }
    }
}
