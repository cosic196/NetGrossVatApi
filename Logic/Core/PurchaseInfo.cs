using System;

namespace Logic.Core
{
    public class PurchaseInfo
    {
        public MoneyAmount GrossAmount { get; private set; }
        public MoneyAmount NetAmount { get; private set; }
        public MoneyAmount VatAmount { get; private set; }
        public VatRate VatRate { get; private set; }

        private PurchaseInfo(MoneyAmount grossAmount, MoneyAmount netAmount, MoneyAmount vatAmount, VatRate vatRate)
        {
            GrossAmount = grossAmount;
            NetAmount = netAmount;
            VatAmount = vatAmount;
            VatRate = vatRate;
        }

        public static PurchaseInfo CreateWithGrossAmount(VatRate vatRate, MoneyAmount grossAmount)
        {
            ThrowIfInputNull(vatRate, grossAmount);
            decimal netAmount = grossAmount.Value / (1 + vatRate.Value);
            decimal vatAmount = netAmount * vatRate.Value;

            var netAmountResult = MoneyAmount.Create(netAmount);
            var vatAmountResult = MoneyAmount.Create(vatAmount);
            if(netAmountResult.IsFailure || vatAmountResult.IsFailure)
            {
                throw new ArgumentException();
            }

            return new PurchaseInfo(grossAmount, netAmountResult.Value, vatAmountResult.Value, vatRate);
        }

        public static PurchaseInfo CreateWithNetAmount(VatRate vatRate, MoneyAmount netAmount)
        {
            ThrowIfInputNull(vatRate, netAmount);
            decimal vatAmount = netAmount.Value * vatRate.Value;
            decimal grossAmount = netAmount.Value + vatAmount;

            var vatAmountResult = MoneyAmount.Create(vatAmount);
            var grossAmountResult = MoneyAmount.Create(grossAmount);
            if (grossAmountResult.IsFailure || vatAmountResult.IsFailure)
            {
                throw new ArgumentException();
            }

            return new PurchaseInfo(grossAmountResult.Value, netAmount, vatAmountResult.Value, vatRate);
        }

        public static PurchaseInfo CreateWithVatAmount(VatRate vatRate, MoneyAmount vatAmount)
        {
            ThrowIfInputNull(vatRate, vatAmount);
            decimal netAmount = vatAmount.Value / vatRate.Value;
            decimal grossAmount = netAmount + vatAmount.Value;

            var netAmountResult = MoneyAmount.Create(netAmount);
            var grossAmountResult = MoneyAmount.Create(grossAmount);
            if (netAmountResult.IsFailure || grossAmountResult.IsFailure)
            {
                throw new ArgumentException();
            }

            return new PurchaseInfo(grossAmountResult.Value, netAmountResult.Value, vatAmount, vatRate);
        }

        private static void ThrowIfInputNull(VatRate vatRate, MoneyAmount amount)
        {
            if (vatRate == null)
            {
                throw new ArgumentException(nameof(vatRate));
            }
            if (amount == null)
            {
                throw new ArgumentException(nameof(amount));
            }
        }
    }
}