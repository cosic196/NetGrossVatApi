using WebApi.Utils;

namespace WebApi.Models
{
    [OnlyOneOfRequired(nameof(GrossAmount), nameof(NetAmount), nameof(VatAmount))]
    public class Amount
    {
        /// <summary>
        /// Required if NetAmount and VatAmount are not set
        /// </summary>
        public decimal? GrossAmount { get; set; }
        /// <summary>
        /// Required if GrossAmount and VatAmount are not set
        /// </summary>
        public decimal? NetAmount { get; set; }
        /// <summary>
        /// Required if NetAmount and GrossAmount are not set
        /// </summary>
        public decimal? VatAmount { get; set; }
    }
}
