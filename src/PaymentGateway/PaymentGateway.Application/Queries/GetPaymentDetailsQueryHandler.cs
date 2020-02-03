using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PaymentGateway.Application.Models;
using PaymentGateway.Domain.Repositories;

namespace PaymentGateway.Application.Queries
{
    public class GetPaymentDetailsQueryHandler : IRequestHandler<GetPaymentDetailsQuery, PaymentDetailsModel>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<GetPaymentDetailsQueryHandler> _logger;

        public GetPaymentDetailsQueryHandler(IPaymentRepository paymentRepository, ILogger<GetPaymentDetailsQueryHandler> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task<PaymentDetailsModel> Handle(GetPaymentDetailsQuery query, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetAsync(query.PaymentId);
            if (payment == null)
            {
                _logger.LogWarning("Payment with {id} was not found", query.PaymentId);
                throw new Exception("Payment was not found");
            }

            var model = new PaymentDetailsModel
            {
                CardNumber = payment.PaymentCard.Number.GetMaskedValue(),
                ExpiryMonth = payment.PaymentCard.ExpiryMonth,
                ExpiryYear = payment.PaymentCard.ExpiryYear,
                ChargeTotal = payment.ChargeTotal.Value,
                CurrencyCode = payment.CurrencyCode,
                Status = payment.Status
            };
            return model;
        }
    }
}
