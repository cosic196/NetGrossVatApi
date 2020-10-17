namespace NetGrossVatApi.Core.DataModels
{
    public class VatCalculatorInput
    {
        public VatCalculatorInput(AmountType amountType, decimal amountValue, decimal vatRate)
        {
            AmountType = amountType;
            AmountValue = amountValue;
            VatRate = vatRate;
        }
        
        public AmountType AmountType { get; set; }
        public decimal AmountValue { get; set; }
        public decimal VatRate { get; set; }
    }

    public enum AmountType
    {
        Vat,
        Net,
        Gross
    }
}
