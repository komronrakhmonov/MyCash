using AutoMapper;
using MyCash.Data.IRepositories;
using MyCash.Data.Repositories;
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;
using MyCash.Servise.Interfaces;
using Newtonsoft.Json;

namespace MyCash.Servise.Services;

public class ExchangeRateService : IExchangeRateService
{
    private readonly IExchangeRateRepository rateRepository = new ExchangeRateRepository();
    private readonly IMapper mapper;
    public ExchangeRateService()
    {

    }
    public ExchangeRateService(IMapper mapper)
    {
        this.mapper = mapper;
    }
    public async ValueTask<Response<ExchangeRateDto>> CreateAsync(ExchangeRateDto exchangeRateDto)
    {
        var newRate = mapper.Map<ExchangeRateForUSD>(exchangeRateDto);
        var rate = await rateRepository.InsertAsync(newRate);
        var mappedRate = mapper.Map<ExchangeRateDto>(rate);

        return new Response<ExchangeRateDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedRate
        };
    }

    public async ValueTask<Response<bool>> DeleteAsync(long id)
    {
        var rate = await rateRepository.SelectAsync(id);
        if (rate is null)
        {
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = false
            };
        }
        await rateRepository.DeleteAsync(id);
        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = true
        };
    }

    public async ValueTask<Response<List<ExchangeRateDto>>> GetAllAsync()
    {
        var rates = rateRepository.SelectAllAsync();
        var mappedRates = mapper.Map<List<ExchangeRateDto>>(rates);
        return new Response<List<ExchangeRateDto>>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedRates
        };
    }

    public async ValueTask<Response<ExchangeRateDto>> GetAsync(long id)
    {
        var rate = await rateRepository.SelectAsync(id);
        if (rate is null)
        {
            return new Response<ExchangeRateDto>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }
        var mappedRate = mapper.Map<ExchangeRateDto>(rate);
        return new Response<ExchangeRateDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedRate
        };
    }

    public async ValueTask<Response<RateDto>> GetRateFromAPI()
    {
        using(var client = new HttpClient())
        {
            var url = "https://openexchangerates.org/api/latest.json?app_id=36cd0e390dda467e9dedfb9043ee51c2";

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var rate = JsonConvert.DeserializeObject<RateDto>(json);

                return new Response<RateDto>()
                {
                    StatusCode = 200,
                    Message = "SUCCESS",
                    Result = rate
                };
            }
            return new Response<RateDto>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }

        
    }

    public async ValueTask<Response<ExchangeRateDto>> UpdateAsync(long id, ExchangeRateDto exchangeRateDto)
    {
        var rate = await rateRepository.SelectAsync(id);
        if (rate is null)
        {
            return new Response<ExchangeRateDto>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }
        rate.UpdatedAt = DateTime.Now;
        rate.UZS = exchangeRateDto.UZS;
        rate.RUB = exchangeRateDto.RUb;
        rate.EUR = exchangeRateDto.EUR;
        

        var result = await rateRepository.UpdateAsync(rate);
        var mappedRate = mapper.Map<ExchangeRateDto>(result);

        return new Response<ExchangeRateDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedRate
        };
    }
}
