using System;
using AcquiringBank.Api.Mock.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AcquiringBank.Api.Mock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ILogger<TransactionsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public TransactionResultModel Post([FromBody] PaymentTransactionModel model)
        {
            _logger.LogDebug("Process payment transaction {model}", model);
            var status = model.ChargeTotal % 2 == 0 ? TransactionStatus.Accepted : TransactionStatus.Declined;
            return new TransactionResultModel { TransactionId = Guid.NewGuid(), Status = status };
        }
    }
}
