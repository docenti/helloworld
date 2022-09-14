namespace HelloWorld1.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Author AuthorRef { get; set; }
    public DateTime PublishedOn { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
}
