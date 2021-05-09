using Logic.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logic.Tests.Core
{
    [TestClass]
    public class VatRate_Should
    {
        [DataTestMethod]
        [DataRow("0.1")]
        [DataRow("0.15")]
        [DataRow("0.62")]
        [DataRow("0.13")]
        [DataRow("0.99")]
        public void Return_a_succesful_result_when_using_a_value_between_0_and_1(string stringValue)
        {
            decimal value = decimal.Parse(stringValue);

            var result = VatRate.Create(value);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(value, result.Value.Value);
        }

        [DataTestMethod]
        [DataRow("-20.1")]
        [DataRow("-0.15")]
        [DataRow("1.62")]
        [DataRow("101.13")]
        [DataRow("5.99")]
        [DataRow("0.0")]
        [DataRow("1.0")]
        public void Return_a_failed_result_when_using_a_value_less_than_or_equal_to_0_or_higher_than_or_equal_to_1(string stringValue)
        {
            decimal value = decimal.Parse(stringValue);

            var result = VatRate.Create(value);

            Assert.IsTrue(result.IsFailure);
            Assert.IsNull(result.Value);
        }
    }
}
