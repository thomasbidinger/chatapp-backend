using Microsoft.EntityFrameworkCore;
using PaceBackend.Entities;

namespace PaceBackend.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Specify 'Sub' as unique
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Subject)
            .IsUnique();
        
        // Specify 'Email' as unique
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}