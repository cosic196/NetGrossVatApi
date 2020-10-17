using NetGrossVatApi.Core;
using NetGrossVatApi.Core.DataModels;

namespace NetGrossVatApi.Services
{
    public class VatCalculator : IVatCalculator
    {
        public VatCalculatorResult Calculate(VatCalculatorInput input)
        {
            switch(input.AmountType)
            {
                case AmountType.Vat:
                    return CalculateWithVat(input.AmountValue, input.VatRate);
                case AmountType.Gross:
                    return CalculateWithGross(input.AmountValue, input.VatRate);
                case AmountType.Net:
                    return CalculateWithNet(input.AmountValue, input.VatRate);
                default:
                    return null;
            }
        }

        private VatCalculatorResult CalculateWithNet(decimal netValue, decimal vatRate)
        {
            decimal vatValue = netValue * vatRate;
            decimal grossValue = netValue + vatValue;
            return new VatCalculatorResult(netValue, grossValue, vatValue);
        }

        private VatCalculatorResult CalculateWithGross(decimal grossValue, decimal vatRate)
        {
            decimal netValue = grossValue / (1 + vatRate);
            decimal vatValue = netValue * vatRate;
            return new VatCalculatorResult(netValue, grossValue, vatValue);
        }

        private VatCalculatorResult CalculateWithVat(decimal vatValue, decimal vatRate)
        {
            decimal netValue = vatValue / vatRate;
            decimal grossValue = netValue + vatValue;
            return new VatCalculatorResult(netValue, grossValue, vatValue);
        }
    }
}
