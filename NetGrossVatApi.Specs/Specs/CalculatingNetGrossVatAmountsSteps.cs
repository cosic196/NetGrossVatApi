using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetGrossVatApi.Controllers;
using NetGrossVatApi.Core.DataModels;
using NetGrossVatApi.Services;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace NetGrossVatApi.Specs.Specs
{
    [Binding]
    public sealed class CalculatingNetGrossVatAmountsSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public CalculatingNetGrossVatAmountsSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"one of the net, gross or VAT amounts")]
        public void GivenOneOfTheNetGrossOrVATAmounts()
        {
            _scenarioContext.Add("vat", "0.65");
            _scenarioContext.Add("net", "5");
            _scenarioContext.Add("gross", "5.65");
        }

        [Given(@"a valid Austrian VAT rate")]
        public void GivenAValidAustrianVATRate()
        {
            _scenarioContext.Add("vatRate", "0.13");
        }

        [When(@"the API is called")]
        public void WhenTheAPIIsCalled()
        {
            _scenarioContext.TryGetValue<string>("vat", out var vat);
            _scenarioContext.TryGetValue<string>("net", out var net);
            _scenarioContext.TryGetValue<string>("gross", out var gross);
            _scenarioContext.TryGetValue<string>("vatType", out var vatType);
            _scenarioContext.TryGetValue<string>("netType", out var netType);
            _scenarioContext.TryGetValue<string>("grossType", out var grossType);
            _scenarioContext.TryGetValue<string>("vatRate", out var vatRate);
            var vatCalculatorController = new VatCalculatorController(new VatCalculator(), new VatCalculatorInputParser());
            var vatInput = new Dictionary<string, string>();
            var netInput = new Dictionary<string, string>();
            var grossInput = new Dictionary<string, string>();
            if(!string.IsNullOrEmpty(vat))
            {
                vatInput.Add("vatAmount", vat);
            }
            if (!string.IsNullOrEmpty(net))
            {
                netInput.Add("netAmount", net);
            }
            if (!string.IsNullOrEmpty(gross))
            {
                grossInput.Add("grossAmount", gross);
            }
            if(!string.IsNullOrEmpty(vatRate))
            {
                vatInput.Add("vatRate", vatRate);
                netInput.Add("vatRate", vatRate);
                grossInput.Add("vatRate", vatRate);
            }

            var vatResult = vatCalculatorController.GetCalculationResult(vatInput);
            var netResult = vatCalculatorController.GetCalculationResult(netInput);
            var grossResult = vatCalculatorController.GetCalculationResult(grossInput);

            _scenarioContext.Add("vatResult", vatResult);
            _scenarioContext.Add("netResult", netResult);
            _scenarioContext.Add("grossResult", grossResult);
        }

        [Then(@"the other two missing amounts are calculated and returned")]
        public void ThenTheOtherTwoMissingAmountsAreCalculatedAndReturned()
        {
            var vatResult = _scenarioContext.Get<VatCalculatorResult>("vatResult");
            var netResult = _scenarioContext.Get<VatCalculatorResult>("netResult");
            var grossResult = _scenarioContext.Get<VatCalculatorResult>("grossResult");

            Assert.IsTrue(vatResult.Succeeded);
            Assert.AreEqual((decimal)0.65, vatResult.Result.VatValue);
            Assert.AreEqual((decimal)5.0, vatResult.Result.NetValue);
            Assert.AreEqual((decimal)5.65, vatResult.Result.GrossValue);

            Assert.IsTrue(netResult.Succeeded);
            Assert.AreEqual((decimal)0.65, netResult.Result.VatValue);
            Assert.AreEqual((decimal)5.0, netResult.Result.NetValue);
            Assert.AreEqual((decimal)5.65, netResult.Result.GrossValue);

            Assert.IsTrue(grossResult.Succeeded);
            Assert.AreEqual((decimal)0.65, grossResult.Result.VatValue);
            Assert.AreEqual((decimal)5.0, grossResult.Result.NetValue);
            Assert.AreEqual((decimal)5.65, grossResult.Result.GrossValue);
        }
    }
}
