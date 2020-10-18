using NetGrossVatApi.Core;
using NetGrossVatApi.Core.DataModels;
using System;
using System.Collections.Generic;

namespace NetGrossVatApi.Services
{
    public class VatCalculatorInputParser : IVatCalculatorInputParser
    {
        private static Dictionary<string, AmountType> _validAmountFields = new Dictionary<string, AmountType>
        {
            {"vatAmount", AmountType.Vat },
            {"netAmount", AmountType.Net},
            {"grossAmount", AmountType.Gross }
        };

        public VatCalculatorInput Parse(IDictionary<string, string> input)
        {
            decimal vatRate;
            AmountType amountType = default;
            decimal? amountValue = null;

            string listOfValidFields = "";
            foreach (var validAmountField in _validAmountFields)
            {
                listOfValidFields += $"{validAmountField.Key} ";
            }
            if (!input.ContainsKey("vatRate"))
            {
                throw new ArgumentException("Please provide vatRate.");
            }
            if (input.Count != 2)
            {
                throw new ArgumentException($"Please provide only vatRate and one of the following: {listOfValidFields}");
            }
            if (!decimal.TryParse(input["vatRate"], out vatRate))
            {
                throw new ArgumentException("vatRate needs to be a numerical.");
            }
            if (vatRate <= 0)
            {
                throw new ArgumentException("vatRate needs to be higher than 0.");
            }
            foreach (var validField in _validAmountFields)
            {
                if (input.ContainsKey(validField.Key))
                {
                    if (!decimal.TryParse(input[validField.Key], out decimal parsedAmountValue))
                    {
                        throw new ArgumentException("Amount needs to be a numerical.");
                    }
                    amountValue = parsedAmountValue;
                    if (amountValue <= 0)
                    {
                        throw new ArgumentException("Amount needs to be higher than 0.");
                    }
                    amountType = validField.Value;
                    break;
                }
            }
            if (!amountValue.HasValue)
            {
                throw new ArgumentException($"Please provide only vatRate and one of the following: {listOfValidFields}.");
            }

            return new VatCalculatorInput(amountType, amountValue.Value, vatRate);
        }
    }
}
