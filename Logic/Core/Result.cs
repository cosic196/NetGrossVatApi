using System.Collections.Generic;

namespace Logic.Core
{
    public class Result<T>
    {
        public T Value { get; set; }
        public bool Success { get; private set; }
        public bool IsFailure => !Success;
        public IEnumerable<string> Errors { get; private set; }

        protected internal Result(T value, bool success, IEnumerable<string> errors)
        {
            Value = value;
            Success = success;
            Errors = errors;
        }

        public static Result<T> ResultFail(string errorMessage)
        {
            return new Result<T>(default, false, new[] { errorMessage });
        }

        public static Result<T> ResultFail(IEnumerable<string> errorMessages)
        {
            return new Result<T>(default, false, errorMessages);
        }

        public static Result<T> Ok(T value)
        {
            return new Result<T>(value, true, null);
        }
    }
}
