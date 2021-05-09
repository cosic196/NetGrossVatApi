namespace Logic.Core
{
    public class MoneyAmount
    {
        public decimal Value { get; }
        
        private MoneyAmount(decimal value)
        {
            Value = value;
        }

        public static Result<MoneyAmount> Create(decimal value)
        {
            if(value <= 0)
            {
                return Result<MoneyAmount>.ResultFail("Can't use a negative or zero amount for money within purchases.");
            }
            return Result<MoneyAmount>.Ok(new MoneyAmount(value));
        }
    }
}
