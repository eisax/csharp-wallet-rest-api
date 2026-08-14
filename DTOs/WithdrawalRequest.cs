namespace MobileWalletApi.DTOs;

public class WithdrawalRequest
{
    public decimal Amount { get; set; }

    public string? Reference { get; set; }
}