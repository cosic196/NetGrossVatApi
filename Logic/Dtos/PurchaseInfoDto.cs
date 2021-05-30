namespace Logic.Dtos
{
    public class PurchaseInfoDto
    {
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal VatRate { get; set; }
    }
}
