
using MyCash.Servise.DTOs;
using MyCash.Servise.Services;

var income = new ExposeCreationDto()
{
    CategoryId = 1,
    Description = "Oylik",
    Amount = 15,
    WalletId = 2,
};

var service = new ExposeService();
await service.CreateAsync(income);


