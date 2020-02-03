using System;
using PaymentGateway.Domain.Enums;
using PaymentGateway.Domain.ValueObjects;

namespace PaymentGateway.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }

        public Guid PaymentCardId { get; private set; }

        public PaymentCard PaymentCard { get; private set; }

        public Guid MerchantId { get; private set; }

        public Price ChargeTotal { get; private set; }

        public string CurrencyCode { get; private set; }

        public PaymentStatus Status { get; private set; }

        protected Payment() { }

        public Payment(
            PaymentCard paymentCard,
            Guid merchantId,
            decimal chargeTotal,
            string currencyCode)
        {
            Id = Guid.NewGuid();
            PaymentCardId = paymentCard.Id;
            PaymentCard = paymentCard;
            MerchantId = merchantId;
            ChargeTotal = new Price(chargeTotal);
            CurrencyCode = currencyCode;
            Status = PaymentStatus.Initiated;
        }

        public void Succeed()
        {
            Status = PaymentStatus.Successful;
        }

        public void Fail()
        {
            Status = PaymentStatus.Failed;
        }
    }
}
