using System;

namespace NetGrossVatApi.DataModels
{
    public class VatCalculatorResult
    {
        public bool Succeeded { get; set; }
        public double NetValue { get; set; }
        public double GrossValue { get; set; }
        public double VatValue { get; set; }
        public Exception Error { get; set; }

        public VatCalculatorResult(double netValue, double grossValue, double vatValue)
        {
            Succeeded = true;
            NetValue = netValue;
            GrossValue = grossValue;
            VatValue = vatValue;
        }

        public VatCalculatorResult(Exception error)
        {
            Succeeded = false;
            Error = error;
        }
    }
}
