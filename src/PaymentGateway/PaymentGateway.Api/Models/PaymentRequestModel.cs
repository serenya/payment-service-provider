namespace PaymentGateway.Api.Models
{
    public class PaymentRequestModel
    {
        public string CardNumber { get; set; }

        public string CardHolderName { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public decimal ChargeTotal { get; set; }

        public string CurrencyCode { get; set; }

        public int Cvv { get; set; }
    }
}
