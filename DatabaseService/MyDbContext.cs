using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

public class MyDbContext : DbContext
{
    [AllowNull]
    public DbSet<User> users { get; set; }
    [AllowNull]
    public DbSet<Post> posts {get;set;}

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
        modelBuilder.Entity<User>()
            .Property(u => u.Login)
            .IsRequired();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Login)
            .IsUnique();
        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .IsRequired();
    }
}