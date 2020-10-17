using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetGrossVatApi.DataModels;
using NetGrossVatApi.Services;
using Newtonsoft.Json;

namespace NetGrossVatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatCalculatorController : ControllerBase
    {
        [HttpGet]
        public VatCalculatorResult GetCalculationResult(string vatRate, string amountValue, string amountType)
        {
            if(!Enum.TryParse<AmountType>(amountType, out var parsedAmountType))
            {
                return new VatCalculatorResult(new ArgumentException($"Invalid amount type: {amountType}"));
            }
            if(string.IsNullOrEmpty(amountValue))
            {
                return new VatCalculatorResult(new ArgumentException($"Missing amount value."));
            }
            if(!double.TryParse(amountValue, out var parsedAmountValue))
            {
                return new VatCalculatorResult(new ArgumentException($"Invalid amount value: {amountValue}. Must be a number higher than 0."));
            }
            if (string.IsNullOrEmpty(vatRate))
            {
                return new VatCalculatorResult(new ArgumentException($"Missing vat rate."));
            }
            if (!double.TryParse(vatRate, out var parsedVatRate))
            {
                return new VatCalculatorResult(new ArgumentException($"Invalid vat rate: {amountValue}. Must be a number higher than 0."));
            }

            var vatCalculator = new VatCalculator();

            return vatCalculator.Calculate(parsedAmountType, parsedAmountValue, parsedVatRate);
        }
    }
}