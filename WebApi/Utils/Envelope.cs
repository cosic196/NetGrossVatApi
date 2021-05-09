using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WebApi.Utils
{
    public class Envelope<T>
    {
        public T Result { get; }
        public IEnumerable<string> Errors { get; }
        public DateTime TimeGenerated { get; }

        protected internal Envelope(T result, IEnumerable<string> errors)
        {
            Result = result;
            Errors = errors;
            TimeGenerated = DateTime.UtcNow;
        }

        [JsonConstructor]
        public Envelope(T result, IEnumerable<string> errors, DateTime timeGenerated) : this(result, errors)
        {
            TimeGenerated = timeGenerated;
        }
    }

    public sealed class Envelope : Envelope<string>
    {
        private Envelope(IEnumerable<string> errors)
            : base(null, errors)
        {
        }

        public static Envelope<T> Ok<T>(T result)
        {
            return new Envelope<T>(result, null);
        }

        public static Envelope Ok()
        {
            return new Envelope(null);
        }

        public static Envelope Error(string errorMessage)
        {
            return new Envelope(new[] { errorMessage });
        }

        public static Envelope Error(IEnumerable<string> errors)
        {
            return new Envelope(errors);
        }
    }
}
