using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeServer
{
    class Currency
    {
        public string CountryCode { get; set; }
        public int ExchangeRate { get; set; }

        public Currency(string countryCode, int exchangeRate)
        {
            CountryCode = countryCode;
            ExchangeRate = exchangeRate;
        }
    }
}
