using NetGrossVatApi.Controllers;
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
            _scenarioContext.Add("vat", "10.3");
            _scenarioContext.Add("net", "11.3");
            _scenarioContext.Add("gross", "13.3");
        }

        [Given(@"a valid Austrian VAT rate")]
        public void GivenAValidAustrianVATRate()
        {
            _scenarioContext.Add("vatRate", "13");
        }

        [Given(@"a valid amount type")]
        public void GivenAValidAmountType()
        {
            _scenarioContext.Add("vatType", "Vat");
            _scenarioContext.Add("netType", "Net");
            _scenarioContext.Add("grossType", "Gross");
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
            var vatCalculatorController = new VatCalculatorController();

            var vatResult = vatCalculatorController.GetCalculationResult(vatRate, vat, vatType);
            var netResult = vatCalculatorController.GetCalculationResult(vatRate, net, netType);
            var grossResult = vatCalculatorController.GetCalculationResult(vatRate, gross, grossType);

            _scenarioContext.Add("vatResult", vatResult);
            _scenarioContext.Add("netResult", netResult);
            _scenarioContext.Add("grossResult", grossResult);
        }

        [Then(@"the other two missing amounts are calculated and returned")]
        public void ThenTheOtherTwoMissingAmountsAreCalculatedAndReturned()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
