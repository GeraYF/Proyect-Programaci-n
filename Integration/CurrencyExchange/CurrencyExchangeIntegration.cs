using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trabajo_Final.Integration.CurrencyExchange.dto;

namespace Trabajo_Final.Integration.CurrencyExchange
{
    public class CurrencyExchangeIntegration
    {
        private readonly ILogger<CurrencyExchangeIntegration> _logger;
        private const string API_CONVERT_URL = " https://currency-conversion-and-exchange-rates.p.rapidapi.com/convert";
        private const string API_SYMBOLS_URL = "https://currency-conversion-and-exchange-rates.p.rapidapi.com/symbols";
        private const string API_KEY = "4dc2b79d34msh82ca35a4f7793e3p1a6300jsn61100b1a5896";

        public CurrencyExchangeIntegration(ILogger<CurrencyExchangeIntegration> logger)
        {
            _logger = logger;
        }

        public async Task<CurrencyExchangeResponse> GetExchangeRate(string fromCurrency, string toCurrency, decimal amount)
        {
            string requestUrl = $"{API_CONVERT_URL}?from={fromCurrency}&to={toCurrency}&amount={amount}";
            var body = new CurrencyExchangeResponse();
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("x-rapidapi-key", API_KEY);
                    var response = await client.GetAsync(requestUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        body = await response.Content.ReadFromJsonAsync<CurrencyExchangeResponse>();
                        _logger.LogInformation($"Exchange rate from {fromCurrency} to {toCurrency} is {body}");
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error getting exchange rate");
                }
            }
            return body;
        }

        public async Task<CurrencySymbols> GetCurrencySymbols()
        {
            string requestUrl = $"{API_SYMBOLS_URL}";
            CurrencySymbols body = new CurrencySymbols();
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("x-rapidapi-key", API_KEY);
                    var response = await client.GetAsync(requestUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        body = await response.Content.ReadFromJsonAsync<CurrencySymbols>();
                        _logger.LogInformation($"symbols {body}");
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error getting exchange rate");
                }
            }
            return body;
        }

    }
}