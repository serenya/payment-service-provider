using System;
using System.Threading.Tasks;
using PaymentGateway.Application.Services.AcquiringBank.Models;
using Refit;

namespace PaymentGateway.Application.Services.AcquiringBank
{
    /// <summary>
    /// Assume that PaymentGateway has a trusted connection to AcquiringBank
    /// </summary>
    public interface IAcquiringBankClient
    {
        [Post("/api/transactions")]
        Task<TransactionResultModel> ProcessTransactionAsync(PaymentTransactionModel model);
    }

    namespace Models
    {

        public class PaymentTransactionModel
        {
            public CardInfo PaymentCard { get; set; }

            public string RecipientIban { get; set; }

            public decimal ChargeTotal { get; set; }

            public string CurrencyCode { get; set; }
        }

        public class CardInfo
        {
            public string Number { get; set; }

            public string HolderName { get; set; }

            public int ExpiryMonth { get; set; }

            public int ExpiryYear { get; set; }

            public int Cvv { get; set; }
        }

        public class TransactionResultModel
        {
            public Guid TransactionId { get; set; }

            public TransactionStatus Status { get; set; }
        }

        public enum TransactionStatus
        {
            Declined = 0,
            Accepted = 1
        }
    }
}
