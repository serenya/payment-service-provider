using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Application.Models
{
    public class PaymentDetailsModel
    {
        public string CardNumber { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public decimal ChargeTotal { get; set; }

        public string CurrencyCode { get; set; }

        public PaymentStatus Status { get; set; }
    }
}
