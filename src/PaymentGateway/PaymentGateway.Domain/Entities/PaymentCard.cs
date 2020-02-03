using System;
using PaymentGateway.Domain.ValueObjects;

namespace PaymentGateway.Domain.Entities
{
    public class PaymentCard
    {
        public Guid Id { get; private set; }

        public CardNumber Number { get; private set; }

        public string HolderName { get; private set; }

        public int ExpiryMonth { get; private set; }

        public int ExpiryYear { get; private set; }

        protected PaymentCard() { }

        public PaymentCard(
            string number,
            string holderName,
            int expiryMonth,
            int expiryYear)
        {
            Id = Guid.NewGuid();
            Number = new CardNumber(number);
            HolderName = holderName;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
        }
    }
}
