using HelloWorld1.Infrastructure;
using HelloWorld1.Models;

namespace HelloWorld1.Services;

public interface IBookService
{
    IEnumerable<Book> GetAll();
    Book GetById(int id);
    Book Add(Book book);
    void Update(Book book);
    void Delete(int id);
}

public class BookService : IBookService
{
    private readonly DataContext _dbContext;

    public BookService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Book> GetAll()
    {
        return _dbContext.Books;
    }

    public Book GetById(int id)
    {
        return GetBook(id);
    }

    public Book Add(Book book)
    {
        // TODO: proper validation of book
        if (_dbContext.Books.Any(x => x.Id == book.Id))
            throw new Exception("Book already exists");
        _dbContext.Books.Add(book);
        return book;
    }

    public void Update(Book book)
    {
        var b = GetBook(book.Id);

        // TODO: some validation

        // TODO: proper implementation of update
        b.Title = book.Title;
        //b.Author = book.Author;

        _dbContext.Books.Update(b);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var book = GetBook(id);

        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
    }

    private Book GetBook(long id)
    {
        var book = _dbContext.Books.Find(id);

        if (book == null)
            throw new InvalidOperationException("Book not found");

        return book;
    }
}
