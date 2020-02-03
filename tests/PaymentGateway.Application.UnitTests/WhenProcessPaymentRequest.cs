using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Services.AcquiringBank;
using PaymentGateway.Application.Services.AcquiringBank.Models;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Enums;
using PaymentGateway.Domain.Repositories;
using Xunit;

namespace PaymentGateway.Application.UnitTests
{
    public class WhenProcessPaymentRequest
    {
        [Fact]
        public async Task ItSucceedsPaymentIfAcquiringBankAcceptedTransaction()
        {
            // Arrange
            var acquiringBankClient = Substitute.For<IAcquiringBankClient>();
            acquiringBankClient
                .ProcessTransactionAsync(Arg.Any<PaymentTransactionModel>())
                .Returns(new TransactionResultModel
                {
                    TransactionId = Guid.NewGuid(),
                    Status = TransactionStatus.Accepted
                });
            var merchantRepository = Substitute.For<IMerchantRepository>();
            var merchantId = Guid.NewGuid();
            merchantRepository
                .GetAsync(Arg.Any<Guid>())
                .Returns(new Merchant { Id = merchantId, Iban = "DE91100000000123456789" });
            var paymentRepository = Substitute.For<IPaymentRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var logger = Substitute.For<ILogger<ProcessPaymentRequestCommandHandler>>();

            var command = new ProcessPaymentRequestCommand(
                    merchantId: merchantId,
                    cardNumber: "4463879623345671",
                    cardHolderName: "Johan Slivchenko",
                    expiryMonth: 10,
                    expiryYear: 2020,
                    chargeTotal: 135m,
                    currencyCode: "EUR",
                    cvv: 461);

            // Act
            var handler = new ProcessPaymentRequestCommandHandler(
                acquiringBankClient,
                merchantRepository,
                paymentRepository,
                unitOfWork,
                logger);
            var result = await handler.Handle(command);

            // Assert
            result.PaymentId.Should().NotBeEmpty();
            result.Status.Should().Be(PaymentStatus.Successful);
        }

        [Fact]
        public async Task ItFailsPaymentIfAcquiringBankDeclinedTransaction()
        {
            // Arrange
            var acquiringBankClient = Substitute.For<IAcquiringBankClient>();
            acquiringBankClient
                .ProcessTransactionAsync(Arg.Any<PaymentTransactionModel>())
                .Returns(new TransactionResultModel
                {
                    TransactionId = Guid.NewGuid(),
                    Status = TransactionStatus.Declined
                });
            var merchantRepository = Substitute.For<IMerchantRepository>();
            var merchantId = Guid.NewGuid();
            merchantRepository
                .GetAsync(Arg.Any<Guid>())
                .Returns(new Merchant { Id = merchantId, Iban = "DE91100000000123456789" });
            var paymentRepository = Substitute.For<IPaymentRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var logger = Substitute.For<ILogger<ProcessPaymentRequestCommandHandler>>();

            var command = new ProcessPaymentRequestCommand(
                    merchantId: merchantId,
                    cardNumber: "4463879623345671",
                    cardHolderName: "Johan Slivchenko",
                    expiryMonth: 10,
                    expiryYear: 2020,
                    chargeTotal: 135m,
                    currencyCode: "EUR",
                    cvv: 461);

            // Act
            var handler = new ProcessPaymentRequestCommandHandler(
                acquiringBankClient,
                merchantRepository,
                paymentRepository,
                unitOfWork,
                logger);
            var result = await handler.Handle(command);

            // Assert
            result.PaymentId.Should().NotBeEmpty();
            result.Status.Should().Be(PaymentStatus.Failed);
        }
    }
}
