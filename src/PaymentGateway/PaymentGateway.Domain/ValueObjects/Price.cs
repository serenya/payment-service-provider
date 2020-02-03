using System;

namespace PaymentGateway.Domain.ValueObjects
{
    public struct Price
    {
        public readonly decimal Value;

        public Price(decimal value)
        {
            if (!IsValid(value)) throw new ArgumentOutOfRangeException(nameof(Price));

            Value = decimal.Round(value, decimals: 2);
        }

        public override int GetHashCode()
            => Value.GetHashCode();

        public override string ToString()
        {
            return Value.ToString("N2");
        }

        public static bool IsValid(decimal value)
            => value >= 0;

        public override bool Equals(object obj)
            => (obj is Price other && Value == other.Value)
            || (obj is decimal decValue && Value == decValue)
            || (obj is int intValue && Value == intValue);

        public static implicit operator Price(decimal value)
            => new Price(value);

        public static bool operator ==(Price price1, Price price2)
            => price1.Value == price2.Value;

        public static bool operator !=(Price price1, Price price2)
            => price1.Value != price2.Value;
    }
}
