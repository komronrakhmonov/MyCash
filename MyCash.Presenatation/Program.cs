
using AutoMapper;
using MyCash.Domain.Entities;
using MyCash.Domain.Enums;
using MyCash.Servise.DTOs;
using MyCash.Servise.Mappers;
using MyCash.Servise.Services;

var mapper = new Mapper(new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MapperProfile>();

}));

#region Expose
//var expose = new ExposeDto()
//{
//    WalletId = 3,
//    Amount = 25000,
//    Description = "I got my salary for march",
//    CategoryId = 2

//};

//var walletService = new WalletService(mapper);
//var expo = new ExposeService(mapper, walletService);
//await expo.CreateAsync(expose);
#endregion

#region Income
//var income = new IncomeDto()
//{
//    WalletId = 3,
//    Amount = 4444000,
//    Description = "I got my salary for march",
//    CategoryId = 2

//};
//var walletService = new WalletService(mapper);
//var inco = new IncomeService(mapper, walletService);
//await inco.CreateAsync(income);
#endregion

#region API
//var ser = new ExchangeRateService(mapper);
//var response = await ser.GetRateFromAPI();
//Console.WriteLine($"1 USD = {response.Result.ExchangeDto.UZS} UZS");
//Console.WriteLine($"1 USD = {response.Result.ExchangeDto.RUb} RUB");
//Console.WriteLine($"1 USD = {response.Result.ExchangeDto.EUR} EUR");

////await ser.CreateAsync(response.Result.ExchangeDto);

#endregion

#region User
//var user = new UserDtoForCreation()
//{
//    FirstName = "asad",
//    LastName = "asd",
//    Email = "kkk",
//    Password = "password",
//};

//var ser = new UserService(mapper);
//await ser.CreateAsync(user);

#endregion

#region wallet
//var wallet = new WalletDto()
//{
//    UserId = 3,
//    Currency = CurrencyType.USD,
//    Name = "Katmon"
//};
//var ser = new WalletService(mapper);
//await ser.CreateAsync(3, wallet);
#endregion

#region category
//var cate = new CategoryDto()
//{
//    Name = "Oylik",
//    Description = "Xotin qani",
//    Type = CategoryType.Income
//};

//var cat = new CategoryService(mapper);
//await cat.CreateAsync(cate);
#endregion



