using Logic.AppServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Logic.Tests.AppServices
{
    [TestClass]
    public class GetPurchaseInfoQueryHandler_Should
    {
        [TestMethod]
        public void Return_a_succesful_result_with_valid_dto_given_a_query_with_valid_values()
        {
            var query = new GetPurchaseInfoQuery(1.0m, AmountType.Net, 0.1m);
            var queryHandler = new GetPurchaseInfoQueryHandler();

            var result = queryHandler.Handle(query);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(1.0m, result.Value.NetAmount);
            Assert.AreEqual(1.1m, result.Value.GrossAmount);
            Assert.AreEqual(0.1m, result.Value.VatAmount);
            Assert.AreEqual(0.1m, result.Value.VatRate);
        }

        [TestMethod]
        public void Return_a_failed_result_with_1_descriptive_error_message_when_given_query_with_invalid_amount()
        {
            var query = new GetPurchaseInfoQuery(-1.0m, AmountType.Net, 0.1m);
            var queryHandler = new GetPurchaseInfoQueryHandler();

            var result = queryHandler.Handle(query);

            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(1, result.Errors.Count());
            Assert.AreEqual("Can't use a negative or zero amount for money within purchases.", result.Errors.First());
        }

        [TestMethod]
        public void Throw_argument_exception_when_given_query_with_invalid_amount_type()
        {
            var query = new GetPurchaseInfoQuery(1.0m, (AmountType)15, 0.1m);
            var queryHandler = new GetPurchaseInfoQueryHandler();

            Assert.ThrowsException<ArgumentException>(() => queryHandler.Handle(query));
        }

        [TestMethod]
        public void Return_a_failed_result_with_descriptive_error_message_when_given_query_with_invalid_vat_rate()
        {
            var query = new GetPurchaseInfoQuery(1.0m, AmountType.Net, 12.1m);
            var queryHandler = new GetPurchaseInfoQueryHandler();

            var result = queryHandler.Handle(query);

            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(1, result.Errors.Count());
            Assert.AreEqual("Invalid VAT rate. VAT rate must be higher than 0 and lower than 1.", result.Errors.First());
        }
    }
}
