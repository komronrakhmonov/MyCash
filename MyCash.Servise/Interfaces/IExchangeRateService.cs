using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;

namespace MyCash.Servise.Interfaces;

public interface IExchangeRateService
{
    ValueTask<Response<ExchangeRateDto>> CreateAsync(ExchangeRateDto exchangeRateDto);
    ValueTask<Response<bool>> DeleteAsync(long id);
    ValueTask<Response<ExchangeRateDto>> GetAsync(long id);
    ValueTask<Response<List<ExchangeRateDto>>> GetAllAsync();
    ValueTask<Response<ExchangeRateDto>> UpdateAsync(long id, ExchangeRateDto exchangeRateDto);
    ValueTask<Response<RateDto>> GetRateFromAPI();
}
