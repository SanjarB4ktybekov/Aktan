using System;
using Microsoft.EntityFrameworkCore;
using Aktan.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AktanDb;Trusted_Connection=True;");
    }

    public DbSet<Unit> Units { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Finances> Finances { get; set; }
}