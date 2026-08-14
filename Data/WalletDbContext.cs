using Microsoft.EntityFrameworkCore;
using MobileWalletApi.Data;
using MobileWalletApi.Models;

namespace MobileWalletApi.Data;


public class WalletDbContext : DbContext
{
    public WalletDbContext(DbContextOptions<WalletDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Wallet> Wallets => Set<Wallet>();

    public DbSet<WalletTransaction> Transactions => Set<WalletTransaction>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.Wallet)
            .WithOne()
            .HasForeignKey<Wallet>(w => w.UserId);

        modelBuilder.Entity<Wallet>()
            .HasMany<WalletTransaction>()
            .WithOne(t => t.Wallet)
            .HasForeignKey(t => t.WalletId);

        modelBuilder.Entity<Wallet>()
            .Property(w => w.Balance)
            .HasPrecision(18, 2);

        modelBuilder.Entity<WalletTransaction>()
            .Property(t => t.Amount)
            .HasPrecision(18, 2);
    }
}