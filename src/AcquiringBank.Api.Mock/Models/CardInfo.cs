namespace AcquiringBank.Api.Mock.Models
{
    public class CardInfo
    {
        public string Number { get; set; }

        public string HolderName { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public int Cvv { get; set; }
    }
}
