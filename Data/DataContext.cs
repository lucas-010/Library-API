using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Author> Authors { get; set; } = null!;
}