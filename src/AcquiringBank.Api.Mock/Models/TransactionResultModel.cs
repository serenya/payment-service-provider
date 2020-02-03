using System;

namespace AcquiringBank.Api.Mock.Models
{
    public class TransactionResultModel
    {
        public Guid TransactionId { get; set; }

        public TransactionStatus Status { get; set; }
    }
}
