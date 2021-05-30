using Logic.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logic.Tests.Core
{
    [TestClass]
    public class PurchaseInfo_Should
    {
        [TestMethod]
        public void Create_a_purchase_info_given_valid_VatRate_and_GrossAmount()
        {
            VatRate vatRate = VatRate.Create(0.10m).Value;
            MoneyAmount grossAmount = MoneyAmount.Create(1.10m).Value;

            var result = PurchaseInfo.CreateWithGrossAmount(vatRate, grossAmount);

            Assert.AreEqual(vatRate.Value, result.VatRate.Value);
            Assert.AreEqual(grossAmount.Value, result.GrossAmount.Value);
            Assert.AreEqual(1.0m, result.NetAmount.Value);
            Assert.AreEqual(0.10m, result.VatAmount.Value);
        }

        [TestMethod]
        public void Create_a_succesful_result_given_valid_VatRate_and_NetAmount()
        {
            VatRate vatRate = VatRate.Create(0.10m).Value;
            MoneyAmount netAmount = MoneyAmount.Create(1).Value;

            var result = PurchaseInfo.CreateWithNetAmount(vatRate, netAmount);

            Assert.AreEqual(vatRate.Value, result.VatRate.Value);
            Assert.AreEqual(netAmount.Value, result.NetAmount.Value);
            Assert.AreEqual(1.10m, result.GrossAmount.Value);
            Assert.AreEqual(0.10m, result.VatAmount.Value);
        }

        [TestMethod]
        public void Create_a_succesful_result_given_valid_VatRate_and_VatAmount()
        {
            VatRate vatRate = VatRate.Create(0.10m).Value;
            MoneyAmount vatAmount = MoneyAmount.Create(1).Value;

            var result = PurchaseInfo.CreateWithVatAmount(vatRate, vatAmount);

            Assert.AreEqual(vatRate.Value, result.VatRate.Value);
            Assert.AreEqual(vatAmount.Value, result.VatAmount.Value);
            Assert.AreEqual(11.0m, result.GrossAmount.Value);
            Assert.AreEqual(10.0m, result.NetAmount.Value);
        }
    }
}
