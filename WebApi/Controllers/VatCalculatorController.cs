using Logic.AppServices;
using Logic.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class VatCalculatorController : ControllerBase
    {
        private readonly IQueryHandler<GetPurchaseInfoQuery, PurchaseInfoDto> _getPurchaseInfoQueryHandler;

        public VatCalculatorController(IQueryHandler<GetPurchaseInfoQuery, PurchaseInfoDto> getPurchaseInfoQueryHandler)
        {
            _getPurchaseInfoQueryHandler = getPurchaseInfoQueryHandler;
        }

        /// <summary>
        /// Calculate gross, net and vat amounts of a purchase based on one of the amounts and a vat rate.
        /// </summary>
        [ProducesResponseType(typeof(Envelope<PurchaseInfoDto>), 200)]
        [ProducesResponseType(typeof(Envelope<object>), 422)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [HttpGet]
        public IActionResult GetPurchaseDetails([FromQuery] Amount amount, [FromQuery, Required] decimal? vatRate)
        {
            var query = CreateFromInput(amount, vatRate.Value);
            var result = _getPurchaseInfoQueryHandler.Handle(query);
            
            if(result.IsFailure)
                return UnprocessableEntity(Envelope.Error(result.Errors));

            return Ok(Envelope.Ok(result.Value));
        }

        private GetPurchaseInfoQuery CreateFromInput(Amount amount, decimal vatRate)
        {
            if(amount.GrossAmount.HasValue)
            {
                return new GetPurchaseInfoQuery(amount.GrossAmount.Value, AmountType.Gross, vatRate);
            }
            else if(amount.NetAmount.HasValue)
            {
                return new GetPurchaseInfoQuery(amount.NetAmount.Value, AmountType.Net, vatRate);
            }
            else if(amount.VatAmount.HasValue)
            {
                return new GetPurchaseInfoQuery(amount.VatAmount.Value, AmountType.Vat, vatRate);
            }
            else
            {
                throw new ArgumentException("Model validation failed. No amount specified in the request model.");
            }
        }
    }
}