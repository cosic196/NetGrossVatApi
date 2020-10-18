using NetGrossVatApi.Core.DataModels;

namespace NetGrossVatApi.Core
{
    public interface IVatCalculator
    {
        VatCalculatorResult Calculate(VatCalculatorInput input);
    }
}
