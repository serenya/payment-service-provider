using System;
using MediatR;
using PaymentGateway.Application.Models;

namespace PaymentGateway.Application.Commands
{
    public class ProcessPaymentRequestCommand : IRequest<PaymentResultModel>
    {
        public Guid MerchantId { get; private set; }

        public string CardNumber { get; private set; }

        public string CardHolderName { get; private set; }

        public int ExpiryMonth { get; private set; }

        public int ExpiryYear { get; private set; }

        public decimal ChargeTotal { get; private set; }

        public string CurrencyCode { get; private set; }

        public int Cvv { get; private set; }

        public ProcessPaymentRequestCommand(
            Guid merchantId,
            string cardNumber,
            string cardHolderName,
            int expiryMonth,
            int expiryYear,
            decimal chargeTotal,
            string currencyCode,
            int cvv)
        {
            MerchantId = merchantId;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            ChargeTotal = chargeTotal;
            CurrencyCode = currencyCode;
            Cvv = cvv;
        }
    }
}
