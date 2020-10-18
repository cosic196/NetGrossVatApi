using NetGrossVatApi.Core.DataModels;
using System.Collections.Generic;

namespace NetGrossVatApi.Core
{
    public interface IVatCalculatorInputParser
    {
        VatCalculatorInput Parse(IDictionary<string, string> input);
    }
}
