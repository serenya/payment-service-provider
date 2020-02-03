using FluentValidation;

namespace PaymentGateway.Application.Commands
{
    public class ProcessPaymentRequestCommandValidator : AbstractValidator<ProcessPaymentRequestCommand>
    {
        public ProcessPaymentRequestCommandValidator()
        {
            RuleFor(c => c.MerchantId).NotEmpty();
            RuleFor(c => c.CardNumber).NotEmpty();
            RuleFor(c => c.CardHolderName).NotEmpty();
            RuleFor(c => c.CurrencyCode).NotEmpty();
            RuleFor(c => c.Cvv).NotEmpty();
            RuleFor(c => c.ExpiryMonth).NotEmpty();
            RuleFor(c => c.ExpiryYear).NotEmpty();
        }
    }
}
