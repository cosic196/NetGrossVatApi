using Logic.AppServices;
using Logic.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatCalculatorController : ControllerBase
    {
        private readonly IQueryHandler<GetPurchaseInfoQuery, PurchaseInfoDto> _getPurchaseInfoQueryHandler;

        public VatCalculatorController(IQueryHandler<GetPurchaseInfoQuery, PurchaseInfoDto> getPurchaseInfoQueryHandler)
        {
            _getPurchaseInfoQueryHandler = getPurchaseInfoQueryHandler;
        }

        [HttpGet]
        public IActionResult GetPurchaseDetails([FromQuery] AmountModel amount, [FromQuery, Required] decimal? vatRate)
        {
            var query = CreateFromInput(amount, vatRate.Value);
            var result = _getPurchaseInfoQueryHandler.Handle(query);
            
            if(result.IsFailure)
                return BadRequest(Envelope.Error(result.Errors));

            return Ok(Envelope.Ok(result.Value));
        }

        private GetPurchaseInfoQuery CreateFromInput(AmountModel amount, decimal vatRate)
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