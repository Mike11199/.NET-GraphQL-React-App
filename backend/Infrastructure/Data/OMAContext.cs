using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Enums;


namespace Infrastructure.Data
{
    public class OMAContext : DbContext  // inherit from DbContext CLASS which is from from entity framework core
    {
        // This is the CONSTRUCTOR for our database context.
        // Database context is a class that provides a connection to a database and allows you to interact with the data in that database using C# code.
        // The database context is typically used in combination with an Object-Relational Mapping (ORM) framework such as Entity Framework or NHibernate.
        // An ORM provides a way to map database tables to C# classes and vice versa.
        public OMAContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer>Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }


        // this to populate with fake data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    FirstName = "James",
                    LastName = "Bond",
                    ContactNumber = "041235456",                    
                    IsDeleted = false,
                    Email = "jamesbond@test.com"
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Monty",
                    LastName = "Python",
                    ContactNumber = "04135156",                    
                    IsDeleted = false,
                    Email = "montypython@test.com"
                }
            );

            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    Id = 1,
                    CustomerID = 1,
                    AddressLine1 = "SomePlace",
                    AddressLine2 = "There",
                    City = "Melbourne",
                    State = "Victoria",
                    Country = "AU"
                },
                new Address
                {
                    Id = 2,
                    CustomerID = 2,
                    AddressLine1 = "SomePlace2",
                    AddressLine2 = "There2",
                    City = "Melbourne",
                    State = "Victoria",
                    Country = "AU"
                }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = new DateTime(2022, 11, 25),
                    Description = "new item",
                    TotalAmount = 500,
                    DepositAmount = 200,
                    IsDelivery = true,
                    Status = Status.Pending,
                    OtherNotes = "something new",
                    IsDeleted = false,

                },
                new Order
                {
                    Id = 2,
                    CustomerId = 2,
                    OrderDate = new DateTime(2022, 12, 25),
                    Description = "new item",
                    TotalAmount = 1500,
                    DepositAmount = 220,
                    IsDelivery = false,
                    Status = Status.Draft,
                    OtherNotes = "something new",
                    IsDeleted = false,

                }
            );

        }

    }

}

