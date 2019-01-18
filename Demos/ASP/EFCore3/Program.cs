using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;

// Model class
public class Friend
{
    [Key]
    public string Name { get; set; }

    [Required]
    public string Location { get; set; }
}

public class MyDbContext : DbContext
{
    public DbSet<Friend> Friends { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseCosmos(
      "https://localhost:8081",
      "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
      "MyDocuments");
    }
}

class Program
{
    private static void Main(string[] args)
    {
        // Setup data in datbase
        using (var context = new MyDbContext())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Add(
                new Friend
                {
                    Name = "Bill",
                    Location = "Here",
                });
            context.Add(
                new Friend
                {
                    Name = "Paul",
                    Location = "There",
                });
            context.SaveChanges();
        }

        // find nearest friends
        using (var context = new MyDbContext())
        {
            var firends = context.Friends.ToList();
            foreach (var item in firends)
            {
                Console.WriteLine($"{item.Name} {item.Location}");
            }
        }
    }
}