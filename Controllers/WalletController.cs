using Microsoft.AspNetCore.Mvc;
using MobileWalletApi.DTOs;
using MobileWalletApi.Interfaces;

namespace MobileWalletApi.Controllers;

[ApiController]
[Route("api/wallets")]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;

    public WalletController(IWalletService walletService)
    {
        _walletService = walletService;
    }

    [HttpPost("{walletId}/deposit")]
    public async Task<IActionResult> Deposit(Guid walletId,
        DepositRequest request)
    {
        var transaction =
            await _walletService.DepositAsync(
                walletId,
                request);

        return Ok(transaction);
    }

    [HttpPost("{walletId}/withdrawal")]
    public async Task<IActionResult> Withdrawal(Guid walletId,
        WithdrawalRequest request)
    {
        var transaction =
            await _walletService.WithdrawalAsync(
                walletId,
                request);

        return Ok(transaction);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWallet(
    CreateUserRequest request)
    {
        var wallet =
            await _walletService.CreateWalletAsync(request);

        return Created(
            $"/api/wallets/{wallet.WalletId}",
            wallet);
    }
}