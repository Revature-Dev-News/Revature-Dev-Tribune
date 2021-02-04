using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StoreApi.Domain.Models;

namespace StoreApi.Service
{
  public class RDTContext : DbContext
  {
    public DbSet<Article> Articles { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Reader> Readers { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<ReaderTopic> ReaderTopics { get; set; }
    public RDTContext(DbContextOptions<RDTContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
      builder.UseSqlServer("Server=x54server.database.windows.net; Initial Catalog=RDTdb;User Id=sqladmin;Password=123QWEasd");
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Article>().HasKey(c => c.EntityId);
      builder.Entity<Author>().HasKey(s => s.EntityId);
      builder.Entity<Reader>().HasKey(s => s.EntityId);
      builder.Entity<Topic>().HasKey(s => s.EntityId);
      builder.Entity<ReaderTopic>().HasKey(s => s.EntityId);
      
      builder.Entity<Topic>().Property(t => t.EntityId).ValueGeneratedNever();
      
      SeedData(builder);

    }
    private void SeedData(ModelBuilder builder) // Startups, Hacker News, Frameworks, Languages, DevOps, Testing, Databases, Machine Learning, FAANG
    {
      builder.Entity<Author>().HasData(
        new Author() { EntityId = 1, Email = "cc@aol.com", Password = "12345", Name = "Chedro Cardenas" },
        new Author() { EntityId = 2, Email = "el@aol.com", Password = "12345", Name = "Elliott Lockwood" },
        new Author() { EntityId = 3, Email = "jg@aol.com", Password = "12345", Name = "Joshwin Greene" }
      );
      builder.Entity<Topic>().HasData(new List<Topic>
      {
        new Topic() { EntityId = 1, Name = "Startups" },
        new Topic() { EntityId = 2, Name = "DevOps" },
        new Topic() { EntityId = 3, Name = "Testing" },
        new Topic() { EntityId = 4, Name = "Big Data" },
        new Topic() { EntityId = 5, Name = "Machine Learning" },
        new Topic() { EntityId = 6, Name = "FAANG" },
        new Topic() { EntityId = 7, Name = "Languages" },
        new Topic() { EntityId = 8, Name = "Hacker News" },
        new Topic() { EntityId = 9, Name = "Databases" }
      });
    }
  }
};