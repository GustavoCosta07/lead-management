using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Events;
using System.Linq;

namespace MyApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Event> Events { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<BaseDomainEvent>();

            modelBuilder.Entity<Lead>()
                .Ignore(l => l.DomainEvents);

            modelBuilder.Entity<Lead>()
                .Property(l => l.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Lead>()
                .Property(l => l.ContactFirstName)
                .HasMaxLength(100);

            modelBuilder.Entity<Lead>()
                .Property(l => l.ContactEmail)
                .HasMaxLength(150);

            modelBuilder.Entity<Event>().ToTable("Events");
        }

        public void Initialize()
        {
            if (!Leads.Any())
            {
                var lead1 = new Lead("John", "John Doe", "New York", "Painting", "Paint the house", 600, "vendas@test.com", "123-456-7890");
                var lead2 = new Lead("Jane", "Jane Smith", "Los Angeles", "Cleaning", "Clean the office", 200, "vendas@test.com", "987-654-3210");
                var lead3 = new Lead("Michael", "Michael Brown", "Chicago", "Plumbing", "Fix the sink", 450, "vendas@test.com", "555-123-4567");
                var lead4 = new Lead("Emma", "Emma Johnson", "Miami", "Landscaping", "Garden maintenance", 700, "vendas@test.com", "111-222-3333");
                var lead5 = new Lead("Liam", "Liam Wilson", "San Francisco", "Electrical", "Install new wiring", 350, "vendas@test.com", "444-555-6666");
                var lead6 = new Lead("Sophia", "Sophia Martinez", "Las Vegas", "Carpentry", "Build a bookshelf", 500, "vendas@test.com", "777-888-9999");

                Leads.AddRange(lead1, lead2, lead3, lead4, lead5, lead6);
                SaveChanges();
            }
        }
    }
}
