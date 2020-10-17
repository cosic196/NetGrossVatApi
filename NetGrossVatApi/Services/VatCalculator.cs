using NetGrossVatApi.DataModels;
using System;

namespace NetGrossVatApi.Services
{
    public class VatCalculator
    {
        public VatCalculatorResult Calculate(AmountType amountType, double amountValue, double vatRate)
        {
            switch(amountType)
            {
                case AmountType.Vat:
                    return CalculateWithVat(amountValue, vatRate);
                case AmountType.Gross:
                    return CalculateWithGross(amountValue, vatRate);
                case AmountType.Net:
                    return CalculateWithNet(amountValue, vatRate);
                default:
                    return null;
            }
        }

        private VatCalculatorResult CalculateWithNet(double amountValue, double vatRate)
        {
            throw new NotImplementedException();
        }

        private VatCalculatorResult CalculateWithGross(double amountValue, double vatRate)
        {
            throw new NotImplementedException();
        }

        private VatCalculatorResult CalculateWithVat(double amountValue, double vatRate)
        {
            throw new NotImplementedException();
        }
    }

    public enum AmountType
    {
        Vat,
        Net,
        Gross
    }
}
