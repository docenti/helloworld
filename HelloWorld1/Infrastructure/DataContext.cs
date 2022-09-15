using HelloWorld1.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloWorld1.Infrastructure;

public class DataContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(_configuration.GetConnectionString("HelloWorldDatabase"));
    }

    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
}
