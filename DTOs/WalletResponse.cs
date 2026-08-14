namespace MobileWalletApi.DTOs;

public class WalletResponse
{
    public Guid WalletId { get; set; }

    public Guid UserId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public string Currency { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}