using HelloWorld1.Infrastructure;
using HelloWorld1.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloWorld1.Services;

public interface IBookService
{
    Task<Book> Add(Book book, CancellationToken cancel);
    Task<int> Update(long id, Book book, CancellationToken cancel);
    Task<IEnumerable<Book>> GetNewest(int amount, CancellationToken cancel);
}

public class BookService : IBookService
{
    private readonly DataContext _dbContext;

    public BookService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Book> Add(Book book, CancellationToken cancel)
    {
        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync(cancel);

        return book;
    }

    public async Task<int> Update(long id, Book request, CancellationToken cancel)
    {
        var book = await _dbContext.Books.SingleOrDefaultAsync(x=> x.Id == request.Id, cancel);

        if (book == null)
            throw new Exception("Book not found: ");

        book.Title = request.Title;
        book.PublishedOn = request.PublishedOn;
        book.ImageUrl = request.ImageUrl;
        book.Description = request.Description;

        return await _dbContext.SaveChangesAsync(cancel);
    }

    public async Task<IEnumerable<Book>> GetNewest(int amount, CancellationToken cancel)
    {
        return await _dbContext.Books.
            OrderByDescending(b => b.PublishedOn).
            Take(amount).
            ToListAsync(cancel);
    }
}
