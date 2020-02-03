using System;

namespace PaymentGateway.Domain.Entities
{
    /// <summary>
    /// Person who sells products
    /// </summary>
    public class Merchant
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Bank account
        /// </summary>
        public string Iban { get; set; }
    }
}
