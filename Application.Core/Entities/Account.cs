namespace Application.Core.Entities
{
    public class Account : BaseEntity
    {
        public Customer? Customer { get; set; }
        public int CustomerId { get; set; }

        public string? Description { get; set; }
        public decimal CreditBalance { get; set; }

        public DateTime CreationDate { get; set; }

        public Account(int customerId)
        {
            CustomerId = customerId;
        }

        public Account()
        {  }
    }
}
