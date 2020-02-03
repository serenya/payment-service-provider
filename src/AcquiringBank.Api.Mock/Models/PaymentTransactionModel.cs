namespace AcquiringBank.Api.Mock.Models
{
    public class PaymentTransactionModel
    {
        public CardInfo PaymentCard { get; set; }

        public string RecipientIban { get; set; }

        public decimal ChargeTotal { get; set; }

        public string CurrencyCode { get; set; }
    }
}
