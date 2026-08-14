namespace MobileWalletApi.DTOs;

public class DepositRequest
{
    public decimal Amount { get; set; }

    public string? Reference { get; set; }
}