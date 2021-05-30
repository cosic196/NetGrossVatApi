using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Models
{
    [OnlyOneOfRequired(nameof(GrossAmount), nameof(NetAmount), nameof(VatAmount))]
    public class Amount
    {
        /// <summary>
        /// Required if **netAmount** and **vatAmount** are not set
        /// </summary>
        [FromQuery(Name = "grossAmount")]
        public decimal? GrossAmount { get; set; }
        /// <summary>
        /// Required if **grossAmount** and **vatAmount** are not set
        /// </summary>
        [FromQuery(Name = "netAmount")]
        public decimal? NetAmount { get; set; }
        /// <summary>
        /// Required if **netAmount** and **grossAmount** are not set
        /// </summary>
        [FromQuery(Name = "vatAmount")]
        public decimal? VatAmount { get; set; }
    }
}
