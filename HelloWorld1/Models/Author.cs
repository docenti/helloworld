namespace HelloWorld1.Models;

public class Author
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Bio { get; set; } = null!;
    public string Image { get; set; } = null!;
}
