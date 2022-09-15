using HelloWorld1.Infrastructure;
using HelloWorld1.Models;
using HelloWorld1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloWorld1.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly IBookService _bookService;

        public BookController(DataContext dbContext, IBookService bookService)
        {
            _dbContext = dbContext;
            _bookService = bookService;
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
            await _bookService.Update(id, request, cancel);

            return NoContent();
        }

        // POST: api/Book
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book, CancellationToken cancel)
        {
            if (book.Id != default)
                return BadRequest();

            return await _bookService.Add(book, cancel);
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
        
        // GET: api/Book
        [HttpGet("newest/{amount}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetNewest(int amount, CancellationToken cancel)
        {
            return Ok(await _bookService.GetNewest(amount, cancel));
        }

    }
}
