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
        public VatCalculatorResult GetCalculationResult([FromQuery]IDictionary<string, string> input)
        {
            double vatRate;
            AmountType amountType = default;
            double? amountValue = null;

            Dictionary<string, AmountType> validAmountFields = new Dictionary<string, AmountType>
            {
                {"vatAmount", AmountType.Vat },
                {"netAmount", AmountType.Net},
                {"grossAmount", AmountType.Gross }
            };

            VatCalculator vatCalculator = new VatCalculator();

            string listOfValidFields = "";
            foreach (var validAmountField in validAmountFields)
            {
                listOfValidFields += $"{validAmountField.Key} ";
            }
            if (!input.ContainsKey("vatRate"))
            {
                return new VatCalculatorResult("Please provide vatRate.");
            }
            if(input.Count != 2)
            {
                return new VatCalculatorResult($"Please provide only vatRate and one of the following: {listOfValidFields}");
            }
            if (!double.TryParse(input["vatRate"], out vatRate))
            {
                return new VatCalculatorResult("vatRate needs to be a numerical.");
            }
            if(vatRate <= 0)
            {
                return new VatCalculatorResult("vatRate needs to be higher than 0.");
            }
            foreach (var validField in validAmountFields)
            {
                if(input.ContainsKey(validField.Key))
                {
                    if(!double.TryParse(input[validField.Key], out double parsedAmountValue))
                    {
                        return new VatCalculatorResult("Amount needs to be a numerical.");
                    }
                    amountValue = parsedAmountValue;
                    if(amountValue <= 0)
                    {
                        return new VatCalculatorResult("Amount needs to be higher than 0.");
                    }
                    amountType = validField.Value;
                    break;
                }
            }
            if(!amountValue.HasValue)
            {
                return new VatCalculatorResult($"Please provide only vatRate and one of the following: {listOfValidFields}.");
            }

            return vatCalculator.Calculate(amountType, amountValue.Value, vatRate);
        }
    }
}