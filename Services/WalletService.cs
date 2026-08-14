using MobileWalletApi.Data;
using MobileWalletApi.DTOs;
using MobileWalletApi.Interfaces;
using MobileWalletApi.Models;

namespace MobileWalletApi.Services;

public class WalletService : IWalletService
{


    private readonly WalletDbContext _db;

    public WalletService(WalletDbContext db)
    {
        _db = db;
    }
    public async Task<TransactionResponse> DepositAsync(Guid walletId, DepositRequest request)
    {
        if (request.Amount <= 0)
        {
            throw new ArgumentException(
                "Deposit amount must be greater than zero.");
        }

        var wallet = await _db.Wallets.FindAsync(walletId) ?? throw new KeyNotFoundException(
                "Wallet was not found.");

        wallet.Balance += request.Amount;

        var transaction = new WalletTransaction
        {
            Id = Guid.NewGuid(),
            WalletId = wallet.Id,
            Amount = request.Amount,
            Type = TransactionType.Deposit,
            Status = TransactionStatus.Completed,
            Reference = request.Reference
        };

        _db.Transactions.Add(transaction);
        await _db.SaveChangesAsync();

        return new TransactionResponse
        {
            Id = transaction.Id,
            WalletId = transaction.WalletId,
            Amount = transaction.Amount,
            Type = transaction.Type,
            Status = transaction.Status,
            Reference = transaction.Reference,
            CreatedAt = transaction.CreatedAt
        };
    }

    public async Task<TransactionResponse> WithdrawalAsync(Guid walletId, WithdrawalRequest request)
    {
        if (request.Amount <= 0)
        {
            throw new ArgumentException(
                "Withdrawal amount must be greater than zero.");
        }

        var wallet = await _db.Wallets.FindAsync(walletId) ?? throw new KeyNotFoundException(
                "Wallet was not found.");
        
        if (wallet.Balance < request.Amount)
        {
            throw new InvalidOperationException(
                "Insufficient funds for withdrawal.");
        }

        wallet.Balance -= request.Amount;


        var transaction = new WalletTransaction
        {
            Id = Guid.NewGuid(),
            WalletId = wallet.Id,
            Amount = request.Amount,
            Type = TransactionType.Withdrawal,
            Status = TransactionStatus.Completed,
            Reference = request.Reference
        };

        _db.Transactions.Add(transaction);
        await _db.SaveChangesAsync();

        return new TransactionResponse
        {
            Id = transaction.Id,
            WalletId = transaction.WalletId,
            Amount = transaction.Amount,
            Type = transaction.Type,
            Status = transaction.Status,
            Reference = transaction.Reference,
            CreatedAt = transaction.CreatedAt
        };
    }


    public async Task<WalletResponse> CreateWalletAsync(
    CreateUserRequest request)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber
        };

        var wallet = new Wallet
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Balance = 0,
            Currency = "USD",
            IsActive = true
        };

        _db.Users.Add(user);
        _db.Wallets.Add(wallet);

        await _db.SaveChangesAsync();

        return new WalletResponse
        {
            WalletId = wallet.Id,
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Balance = wallet.Balance,
            Currency = wallet.Currency,
            IsActive = wallet.IsActive
        };
    }

}