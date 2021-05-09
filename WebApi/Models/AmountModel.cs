using WebApi.Utils;

namespace WebApi.Models
{
    [OnlyOneOfRequired(nameof(GrossAmount), nameof(NetAmount), nameof(VatAmount))]
    public class AmountModel
    {
        public decimal? GrossAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? VatAmount { get; set; }
    }
}
