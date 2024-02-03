using EFGetStarted.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFGetStarted.Data.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Blog> Blogs => Set<Blog>();
    
    public string DbPath { get; }

    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}