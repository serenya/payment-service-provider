using System;
using MediatR;
using PaymentGateway.Application.Models;

namespace PaymentGateway.Application.Queries
{
    public class GetPaymentDetailsQuery : IRequest<PaymentDetailsModel>
    {
        public Guid PaymentId { get; private set; }

        public GetPaymentDetailsQuery(Guid paymentId)
        {
            PaymentId = paymentId;
        }
    }
}
