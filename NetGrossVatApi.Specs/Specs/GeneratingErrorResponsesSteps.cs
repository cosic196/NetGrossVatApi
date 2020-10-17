using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetGrossVatApi.Controllers;
using NetGrossVatApi.Core.DataModels;
using NetGrossVatApi.Services;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace NetGrossVatApi.Specs.Specs
{
    [Binding]
    public sealed class GeneratingErrorResponsesSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public GeneratingErrorResponsesSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"an invalid amount value")]
        public void GivenAnInvalidAmountValue()
        {
            _scenarioContext.Add("vat", "-0.65");
            _scenarioContext.Add("net", "-1");
            _scenarioContext.Add("gross", "0");
        }

        [Given(@"an invalid Austrian VAT rate")]
        public void GivenAnInvalidAustrianVATRate()
        {
            _scenarioContext.Add("vatRate", "0");
        }

        [When(@"the API is called with more than one amount type")]
        public void WhenTheAPIIsCalledWithMoreThanOneAmountType()
        {
            _scenarioContext.TryGetValue<string>("vat", out var vat);
            _scenarioContext.TryGetValue<string>("net", out var net);
            _scenarioContext.TryGetValue<string>("gross", out var gross);
            _scenarioContext.TryGetValue<string>("vatType", out var vatType);
            _scenarioContext.TryGetValue<string>("netType", out var netType);
            _scenarioContext.TryGetValue<string>("grossType", out var grossType);
            _scenarioContext.TryGetValue<string>("vatRate", out var vatRate);
            var vatCalculatorController = new VatCalculatorController(new VatCalculator(), new VatCalculatorInputParser());
            var vatInput = new Dictionary<string, string>
            {
                {"vatAmount", vat },
                {"vatRate", vatRate },
                {"netAmount", net }
            };
            var netInput = new Dictionary<string, string>
            {
                {"netAmount", net },
                {"vatRate", vatRate },
                {"vatAmount", vat },
            };
            var grossInput = new Dictionary<string, string>
            {
                {"grossAmount", gross },
                {"vatRate", vatRate },
                {"vatAmount", vat },
                {"netAmount", net }
            };

            var vatResult = vatCalculatorController.GetCalculationResult(vatInput);
            var netResult = vatCalculatorController.GetCalculationResult(netInput);
            var grossResult = vatCalculatorController.GetCalculationResult(grossInput);

            _scenarioContext.Add("vatResult", vatResult);
            _scenarioContext.Add("netResult", netResult);
            _scenarioContext.Add("grossResult", grossResult);
        }

        [Then(@"the API returns a response with '(.*)' as the error message")]
        public void ThenTheAPIReturnsAResponseWithAsTheErrorMessage(string expectedErrorMessage)
        {
            var vatResult = _scenarioContext.Get<VatCalculatorResult>("vatResult");
            var netResult = _scenarioContext.Get<VatCalculatorResult>("netResult");
            var grossResult = _scenarioContext.Get<VatCalculatorResult>("grossResult");

            Assert.IsFalse(vatResult.Succeeded);
            Assert.AreEqual(expectedErrorMessage, vatResult.Error);
            Assert.IsFalse(netResult.Succeeded);
            Assert.AreEqual(expectedErrorMessage, netResult.Error);
            Assert.IsFalse(grossResult.Succeeded);
            Assert.AreEqual(expectedErrorMessage, grossResult.Error);
        }
    }
}
