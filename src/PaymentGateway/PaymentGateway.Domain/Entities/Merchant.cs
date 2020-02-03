using System;

namespace PaymentGateway.Domain.Entities
{
    public class Merchant
    {
        public Guid Id { get; set; }

        public string Iban { get; set; }
    }
}
