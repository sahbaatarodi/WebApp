using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.Extensions.Configuration;
using WebApp.Models;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> UsersTable { get; set; }
    public DbSet<Book> BooksTable { get; set; }

    private readonly IConfiguration _configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Read the connection string from appsettings.json
        string connectionString = _configuration.GetConnectionString("DefaultConnection");

        // Use the MySQL provider
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}
