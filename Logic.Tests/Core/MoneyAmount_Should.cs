using Logic.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logic.Tests.Core
{
    [TestClass]
    public class MoneyAmount_Should
    {
        [DataTestMethod]
        [DataRow("0.1")]
        [DataRow("100.15")]
        [DataRow("123.62")]
        [DataRow("45.13")]
        [DataRow("1999.99")]
        public void Return_a_succesful_result_when_using_a_value_higher_than_0(string stringValue)
        {
            decimal value = decimal.Parse(stringValue);

            var result = MoneyAmount.Create(value);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(value, result.Value.Value);
        }

        [DataTestMethod]
        [DataRow("-20.1")]
        [DataRow("-0.15")]
        [DataRow("0.0")]
        [DataRow("-1251.77")]
        public void Return_a_failed_result_when_using_a_value_less_than_or_equal_to_0(string stringValue)
        {
            decimal value = decimal.Parse(stringValue);

            var result = MoneyAmount.Create(value);

            Assert.IsTrue(result.IsFailure);
            Assert.IsNull(result.Value);
        }
    }
}
