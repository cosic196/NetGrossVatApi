namespace NetGrossVatApi.DataModels
{
    public class VatCalculatorResult
    {
        public bool Succeeded { get; set; }
        public Result Result { get; set; }
        public string Error { get; set; }

        public VatCalculatorResult(double netValue, double grossValue, double vatValue)
        {
            Succeeded = true;
            Result = new Result(netValue, grossValue, vatValue);
            
        }

        public VatCalculatorResult(string error)
        {
            Succeeded = false;
            Error = error;
        }
    }

    public class Result
    {
        public Result(double netValue, double grossValue, double vatValue)
        {
            NetValue = netValue;
            GrossValue = grossValue;
            VatValue = vatValue;
        }
        public double NetValue { get; set; }
        public double GrossValue { get; set; }
        public double VatValue { get; set; }
    }
}
