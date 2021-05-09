namespace Logic.Core
{
    public class VatRate
    {
        public decimal Value { get; }

        private VatRate(decimal value)
        {
            Value = value;
        }

        public static Result<VatRate> Create(decimal value)
        {
            if (value <= 0 || value >= 1)
            {
                return Result<VatRate>.ResultFail("Invalid VAT rate. VAT rate must be higher than 0 and lower than 1.");
            }
            return Result<VatRate>.Ok(new VatRate(value));
        }
    }
}
