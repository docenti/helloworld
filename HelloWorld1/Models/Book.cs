namespace HelloWorld1.Models;

public class Book
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public Author AuthorRef { get; set; } = null!;
    public DateTime PublishedOn { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string Description { get; set; } = null!;
}
