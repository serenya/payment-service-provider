using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Api.Models;
using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Models;
using PaymentGateway.Application.Queries;

namespace PaymentGateway.Api.Controllers
{
    /// <summary>
    /// Provides endpoints for processing payment requests and get payment details
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IMediator mediator, ILogger<PaymentsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Return payment details
        /// </summary>
        /// <param name="id">Payment unique identifier</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaymentDetailsModel), (int)HttpStatusCode.OK)]
        public async Task<PaymentDetailsModel> Get(Guid id)
        {
            _logger.LogDebug("Fetch payment details for {id}", id);
            var paymentDetails = await _mediator.Send(new GetPaymentDetailsQuery(id));
            return paymentDetails;
        }

        /// <summary>
        /// Process payment request
        /// </summary>
        /// <param name="model">Payment request</param>
        /// <returns>Payment result</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PaymentResultModel), (int)HttpStatusCode.OK)]
        public async Task<PaymentResultModel> Post([FromBody] PaymentRequestModel model)
        {
            _logger.LogDebug("Process payment request {model}", model);
            var merchantId = Guid.Parse("53b82bee-6c21-473c-a33f-39a811168c65"); // TODO: receive merchantId from claims
            var paymentResult = await _mediator.Send(new ProcessPaymentRequestCommand(
                merchantId,
                model.CardNumber,
                model.CardHolderName,
                model.ExpiryMonth,
                model.ExpiryYear,
                model.ChargeTotal,
                model.CurrencyCode,
                model.Cvv));
            return paymentResult;
        }
    }
}
