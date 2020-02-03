namespace PaymentGateway.Domain.Enums
{
    public enum PaymentStatus
    {
        // Payment process started
        Initiated = 0,
        // Acquiring bank accepted transaction
        Successful = 1,
        // Validation failed or Acquiring bank declined transaction
        Failed = 2
    }
}
