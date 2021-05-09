using Logic.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using TechTalk.SpecFlow;
using WebApi.Utils;

namespace WebApi.Specs.Steps
{
    [Binding]
    public sealed class CalculatingNetGrossVatAmountsSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public CalculatingNetGrossVatAmountsSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"a valid net amount")]
        public void GivenAValidNetAmount()
        {
            _scenarioContext.Add("netAmount", "2.0");
        }

        [Given(@"a valid VAT rate")]
        public void GivenAValidVATRate()
        {
            _scenarioContext.Add("vatRate", "0.1");
        }

        [When(@"the API is called")]
        public void WhenTheAPIIsCalled()
        {
            string queryParameters = GetQueryParametersFromScenarioContext();

            var _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            var _client = _server.CreateClient();
            var response = _client.GetAsync($"/api/vatcalculator?{queryParameters}").Result;
            _scenarioContext.Add("response", response);
        }

        [Then(@"the other two missing amounts are calculated and returned")]
        public void ThenTheOtherTwoMissingAmountsAreCalculatedAndReturned()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            Assert.IsTrue(response.IsSuccessStatusCode);
            var responseBody = response.Content.ReadAsStringAsync().Result;
            var responseEnvelope = JsonConvert.DeserializeObject<Envelope<PurchaseInfoDto>>(responseBody);
            Assert.IsNull(responseEnvelope.Errors);
            Assert.AreEqual(2.0m, responseEnvelope.Result.NetAmount);
            Assert.AreEqual(0.1m, responseEnvelope.Result.VatRate);
            Assert.AreEqual(2.2m, responseEnvelope.Result.GrossAmount);
            Assert.AreEqual(0.2m, responseEnvelope.Result.VatAmount);
        }

        [Then(@"a validation error response is returned")]
        public void ThenAValidationErrorResponseIsReturned()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [Then(@"the response contains error message: ""(.*)""")]
        public void ThenTheResponseContainsErrorMessage(string errorMessage)
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            var responseBody = response.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(responseBody.Contains(errorMessage));
        }

        [Given(@"a valid gross amount")]
        public void GivenAValidGrossAmount()
        {
            _scenarioContext.Add("grossAmount", "2.2");
        }

        [Given(@"an invalid net amount")]
        public void GivenAnInvalidNetAmount()
        {
            _scenarioContext.Add("netAmount", "-12.3");
        }

        [Given(@"an invalid VAT rate")]
        public void GivenAnInvalidVATRate()
        {
            _scenarioContext.Add("vatRate", "0");
        }

        [Then(@"an unsucessful response is returned")]
        public void ThenAnUnsucessfulResponseIsReturned()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            Assert.IsFalse(response.IsSuccessStatusCode);
            var responseBody = response.Content.ReadAsStringAsync().Result;
            var responseEnvelope = JsonConvert.DeserializeObject<Envelope<PurchaseInfoDto>>(responseBody);
            Assert.IsNotNull(responseEnvelope.Errors);
            Assert.IsNull(responseEnvelope.Result);
        }

        #region Helpers
        private string GetQueryParametersFromScenarioContext()
        {
            Dictionary<string, string> queryParametersDict = new Dictionary<string, string>();
            if (_scenarioContext.TryGetValue<string>("netAmount", out var netAmount))
                queryParametersDict.Add("netAmount", netAmount);
            if (_scenarioContext.TryGetValue<string>("grossAmount", out var grossAmount))
                queryParametersDict.Add("grossAmount", grossAmount);
            if (_scenarioContext.TryGetValue<string>("vatAmount", out var vatAmount))
                queryParametersDict.Add("vatAmount", vatAmount);
            if (_scenarioContext.TryGetValue<string>("vatRate", out var vatRate))
                queryParametersDict.Add("vatRate", vatRate);
            string queryParameters = "";
            foreach (var queryParameter in queryParametersDict)
            {
                queryParameters += $"{queryParameter.Key}={queryParameter.Value}&";
            }

            return queryParameters;
        }
        #endregion
    }
}
