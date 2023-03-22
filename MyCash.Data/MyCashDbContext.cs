using Microsoft.EntityFrameworkCore;
using MyCash.Domain.Entities;

namespace MyCash.Data;

public class MyCashDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server = (localdb)\\MSSQLLocalDB; database = MyMoney;" +
            "trusted_connection = true");
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Expose> Exposes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ExchangeRateForUSD> ExchangeRatesForUSD { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        #region Seed Data
        
        modelBuilder.Entity<User>()
            .HasData(new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Komron",
                    LastName = "Rakhmonov",
                    Email = "komron2052@gmail.com",
                    Password = "12345678",
                    CreatedAt = DateTime.Now                   
                },
                new User
                {
                    Id = 2,
                    FirstName = "Ahmad",
                    LastName = "jurayev",
                    Email = "ahmadboy@gmail.com",
                    Password = "87654321",
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    Id = 3,
                    FirstName = "Shaxmat",
                    LastName = "Shashka",
                    Email = "shaxmatboyboy@gmail.com",
                    Password = "54689135",
                    CreatedAt = DateTime.Now
                }
            });

        modelBuilder.Entity<Wallet>()
            .HasData(new List<Wallet>
            {
                new Wallet
                {
                    Id = 1,
                    UserId = 2,
                    Name = "MyWallet",
                    Amount = 10000,
                    Currency = Domain.Enums.CurrencyType.UZS,
                    CreatedAt = DateTime.Now
                },
                new Wallet
                {
                    Id = 2,
                    UserId = 2,
                    Name = "MyWalletNomber2",
                    Amount = 50000,
                    Currency = Domain.Enums.CurrencyType.RUB,
                    CreatedAt = DateTime.Now
                },
                new Wallet
                {
                    Id = 3,
                    UserId = 1,
                    Name = "MyWallet",
                    Amount = 75000,
                    Currency = Domain.Enums.CurrencyType.RUB,
                    CreatedAt = DateTime.Now
                }
            });

        modelBuilder.Entity<Income>()
            .HasData(new List<Income>
            {
                new Income
                {
                    Id = 1,
                    WalletId = 1,
                    Description = "Salary",
                    Amount = 1000,
                    CategoryId = 1,
                    CreatedAt = DateTime.Now
                },
                new Income
                {
                    Id = 2,
                    WalletId = 2,
                    Description = "Salary",
                    Amount = 2000,
                    CategoryId = 1,
                    CreatedAt = DateTime.Now
                },
                new Income
                {
                    Id = 3,
                    WalletId = 2,
                    Description = "Price",
                    Amount = 2000,
                    CategoryId = 1,
                    CreatedAt = DateTime.Now
                }
            });

        modelBuilder.Entity<Expose>()
            .HasData(new List<Expose>
            {
                new Expose
                {
                    Id = 1,
                    WalletId = 1,
                    Description = "Furniture",
                    Amount = 1000,
                    CategoryId = 2,
                    CreatedAt = DateTime.Now
                },
                new Expose
                {
                    Id = 2,
                    WalletId = 2,
                    Description = "Salary",
                    Amount = 2000,
                    CategoryId = 2,
                    CreatedAt = DateTime.Now
                },
                new Expose
                {
                    Id = 3,
                    WalletId = 2,
                    Description = "Price",
                    Amount = 2000,
                    CategoryId = 2,
                    CreatedAt = DateTime.Now
                }
            });

        modelBuilder.Entity<Category>()
            .HasData(new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Salary",
                    CreatedAt = DateTime.Now,
                    Type = Domain.Enums.CategoryType.Income,
                    Description = "I got my salary",
                },
                new Category
                {
                    Id = 2,
                    Name = "bills",
                    CreatedAt = DateTime.Now,
                    Type = Domain.Enums.CategoryType.Expose,
                    Description = "gas",
                },
            });

        #endregion
    }
}
