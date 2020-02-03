using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PaymentGateway.Application.Models;
using PaymentGateway.Application.Services.AcquiringBank;
using PaymentGateway.Application.Services.AcquiringBank.Models;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Repositories;

namespace PaymentGateway.Application.Commands
{
    public class ProcessPaymentRequestCommandHandler : IRequestHandler<ProcessPaymentRequestCommand, PaymentResultModel>
    {
        private readonly IAcquiringBankClient _acquiringBankClient;
        private readonly IMerchantRepository _merchantRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProcessPaymentRequestCommandHandler> _logger;

        public ProcessPaymentRequestCommandHandler(
            IAcquiringBankClient acquiringBankClient,
            IMerchantRepository merchantRepository,
            IPaymentRepository paymentRepository,
            IUnitOfWork unitOfWork,
            ILogger<ProcessPaymentRequestCommandHandler> logger)
        {
            _acquiringBankClient = acquiringBankClient;
            _merchantRepository = merchantRepository;
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Models.PaymentResultModel> Handle(
            ProcessPaymentRequestCommand command,
            CancellationToken cancellationToken = default)
        {
            var merchant = await _merchantRepository.GetAsync(command.MerchantId);
            if (merchant == null)
            {
                _logger.LogWarning("Merchant with {id} was not found", command.MerchantId);
                throw new Exception("Merchant was not found");
            }

            var shopperCard = new PaymentCard(
                command.CardNumber,
                command.CardHolderName,
                command.ExpiryMonth,
                command.ExpiryYear);

            var payment = new Payment(
                shopperCard,
                command.MerchantId,
                command.ChargeTotal,
                command.CurrencyCode);

            _paymentRepository.Add(payment);
            await _unitOfWork.CommitAsync();

            try
            {
                var transactionResult = await ProcessPaymentByAcquiringBank(command, merchant.Iban);
                if (transactionResult.Status == TransactionStatus.Accepted)
                    payment.Succeed();
                else
                    payment.Fail();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment transaction with {id} failed to processed by acquiring bank", payment.Id);
                payment.Fail();
            }

            await _unitOfWork.CommitAsync();

            return new PaymentResultModel
            {
                PaymentId = payment.Id,
                Status = payment.Status
            };
        }

        private async Task<TransactionResultModel> ProcessPaymentByAcquiringBank(ProcessPaymentRequestCommand command, string recipientIban)
        {
            var payerCard = new CardInfo
            {
                Number = command.CardNumber,
                HolderName = command.CardHolderName,
                ExpiryMonth = command.ExpiryMonth,
                ExpiryYear = command.ExpiryYear,
                Cvv = command.Cvv
            };

            var transaction = new PaymentTransactionModel
            {
                PaymentCard = payerCard,
                RecipientIban = recipientIban,
                ChargeTotal = command.ChargeTotal,
                CurrencyCode = command.CurrencyCode
            };
            _logger.LogDebug("Send payment {transaction} to acquiring bank", transaction);
            var transactionResult = await _acquiringBankClient.ProcessTransactionAsync(transaction);
            _logger.LogDebug("Received payment transaction {result} from acquiring bank", transactionResult);
            return transactionResult;
        }
    }
}
