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

        [When(@"the API is called")]
        public void WhenTheAPIIsCalled()
        {
            var vat = _scenarioContext.Get<string>("vat");
            var net = _scenarioContext.Get<string>("net");
            var gross = _scenarioContext.Get<string>("gross");
            var vatRate = _scenarioContext.Get<string>("vatRate");
            var vatCalculatorController = new VatCalculatorController();

            var vatResult = vatCalculatorController.GetCalculationResult(vatRate, vat, "Vat");
            var netResult = vatCalculatorController.GetCalculationResult(vatRate, net, "Net");
            var grossResult = vatCalculatorController.GetCalculationResult(vatRate, gross, "Gross");

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
