using AutoMapper;
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;

namespace MyCash.Servise.Mappers;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<UserDtoForCreation, User>().ReverseMap();
		CreateMap<UserDto, User>().ReverseMap();
		CreateMap<WalletDto, Wallet>().ReverseMap();
		CreateMap<IncomeDto, Income>().ReverseMap();
		CreateMap<ExposeDto, Expose>().ReverseMap();
		CreateMap<CategoryDto, Category>().ReverseMap();
		CreateMap<ExchangeRateDto, ExchangeRateForUSD>().ReverseMap();
	}
}
