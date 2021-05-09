using Logic.Core;
using Logic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.AppServices
{
    public class GetPurchaseInfoQuery : IQuery<PurchaseInfoDto>
    {
        public decimal Amount { get; }
        public AmountType AmountType { get; }
        public decimal VatRate { get; }

        public GetPurchaseInfoQuery(decimal amount, AmountType amountType, decimal vatRate)
        {
            Amount = amount;
            AmountType = amountType;
            VatRate = vatRate;
        }
    }

    public enum AmountType
    {
        Gross,
        Net,
        Vat
    }

    public class GetPurchaseInfoQueryHandler : IQueryHandler<GetPurchaseInfoQuery, PurchaseInfoDto>
    {
        public Result<PurchaseInfoDto> Handle(GetPurchaseInfoQuery query)
        {
            List<string> errors = new List<string>();
            
            var vatRateResult = VatRate.Create(query.VatRate);
            if(vatRateResult.IsFailure)
            {
                errors.AddRange(vatRateResult.Errors);
            }

            var amountResult = MoneyAmount.Create(query.Amount);
            if (amountResult.IsFailure)
            {
                errors.AddRange(amountResult.Errors);
                return Result<PurchaseInfoDto>.ResultFail(errors);
            }

            if (errors.Any())
            {
                return Result<PurchaseInfoDto>.ResultFail(errors);
            }

            if (query.AmountType == AmountType.Gross)
            {
                return MapPurchaseInfoToDto(PurchaseInfo.CreateWithGrossAmount(vatRateResult.Value, amountResult.Value));
            }
            else if(query.AmountType == AmountType.Net)
            {
                return MapPurchaseInfoToDto(PurchaseInfo.CreateWithNetAmount(vatRateResult.Value, amountResult.Value));
            }
            else if(query.AmountType == AmountType.Vat)
            {
                return MapPurchaseInfoToDto(PurchaseInfo.CreateWithVatAmount(vatRateResult.Value, amountResult.Value));
            }
            else
            {
                throw new ArgumentException(nameof(AmountType));
            }
        }

        private Result<PurchaseInfoDto> MapPurchaseInfoToDto(PurchaseInfo purchaseInfo)
        {
            var dto = new PurchaseInfoDto()
            {
                GrossAmount = purchaseInfo.GrossAmount.Value,
                NetAmount = purchaseInfo.NetAmount.Value,
                VatAmount = purchaseInfo.VatAmount.Value,
                VatRate = purchaseInfo.VatRate.Value
            };
            return Result<PurchaseInfoDto>.Ok(dto);
        }
    }
}