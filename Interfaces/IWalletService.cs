using MobileWalletApi.DTOs;
namespace MobileWalletApi.Interfaces;

public interface IWalletService
{
    Task<TransactionResponse> DepositAsync(Guid userId, DepositRequest request);

    Task<WalletResponse> CreateWalletAsync(
        CreateUserRequest request);
}