using System;
using System.Linq;

namespace PaymentGateway.Domain.ValueObjects
{
    public struct CardNumber
    {
        public readonly string Value;

        public CardNumber(string value)
        {
            if (!IsValid(value)) throw new ArgumentOutOfRangeException(nameof(CardNumber));

            Value = value;
        }

        public string GetMaskedValue()
            => new string('*', Value.SkipLast(4).Count()) + string.Join("", Value.TakeLast(4));

        public override string ToString()
            => Value;

        public static bool IsValid(string value)
            => value.Length >= 12 && value.Length <= 24;
    }
}
