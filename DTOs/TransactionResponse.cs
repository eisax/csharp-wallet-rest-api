using MobileWalletApi.Models;
namespace MobileWalletApi.DTOs;

public class TransactionResponse
{
    public Guid Id { get; set; }
    public Guid WalletId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public TransactionStatus Status { get; set; }
    public string? Reference { get; set; }
    public DateTime CreatedAt { get; set; }
}