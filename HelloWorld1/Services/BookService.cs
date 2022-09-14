using HelloWorld1.Helpers;
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
    private DataContext _context;

    public BookService(DataContext context)
    {
        this._context = context;
    }

    public IEnumerable<Book> GetAll()
    {
        return this._context.Books;
    }

    public Book GetById(int id)
    {
        return this.getBook(id);
    }

    public Book Add(Book book)
    {
        // TODO: proper validation of book
        if (_context.Books.Any(x => x.Id == book.Id))
            throw new Exception("Book already exists");
        _context.Books.Add(book);
        return book;
    }

    public void Update(Book book)
    {
        var b = getBook(book.Id);
        
        // TODO: some validation
        
        // TODO: proper implementation of update
        b.Title = book.Title;
        //b.Author = book.Author;
        
        _context.Books.Update(b);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var book = getBook(id);
        
        _context.Books.Remove(book);
        _context.SaveChanges();
    }
    
    private Book getBook(int id)
    {
        var book = this._context.Books.Find(id);
        if (book == null) throw new KeyNotFoundException("Book not found");
        return book;
    }

}

