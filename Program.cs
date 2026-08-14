using Microsoft.EntityFrameworkCore;
using MobileWalletApi.Data;
using MobileWalletApi.Interfaces;
using MobileWalletApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<WalletDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString(
            "DefaultConnection")));

builder.Services.AddScoped<IWalletService, WalletService>();

var app = builder.Build();

app.MapControllers();

app.Run();