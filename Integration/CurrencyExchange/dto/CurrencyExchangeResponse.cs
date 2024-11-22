using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Trabajo_Final.Integration.CurrencyExchange.dto
{
    public class CurrencyExchangeResponse
    {
        public bool success { get; set; }

        [JsonPropertyName("date")]
        public string? dateExchange { get; set; }
        public decimal result { get; set; }
        public Info? info { get; set; }
    }

    public class Info
    {
        [JsonPropertyName("timestamp")]
        public decimal timestampRate { get; set; }
        public decimal rate { get; set; }

    }
}