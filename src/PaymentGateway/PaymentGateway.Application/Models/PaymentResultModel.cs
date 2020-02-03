using System;
using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Application.Models
{
    public class PaymentResultModel
    {
        public Guid PaymentId { get; set; }

        public PaymentStatus Status { get; set; }
    }
}
