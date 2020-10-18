namespace NetGrossVatApi.Core.DataModels
{
    public class VatCalculatorResult
    {
        public bool Succeeded { get; set; }
        public Result Result { get; set; }
        public string Error { get; set; }

        public VatCalculatorResult(decimal netValue, decimal grossValue, decimal vatValue)
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
        public Result(decimal netValue, decimal grossValue, decimal vatValue)
        {
            NetValue = netValue;
            GrossValue = grossValue;
            VatValue = vatValue;
        }
        public decimal NetValue { get; set; }
        public decimal GrossValue { get; set; }
        public decimal VatValue { get; set; }
    }
}
