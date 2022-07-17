namespace Application.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public int AccountId { get; set; }
        public decimal Credit { get; set; }
        public DateTime CreationDate { get; set; }

        public Transaction(int accountId, decimal credit)
        {
            AccountId = accountId;
            Credit = credit;
        }

        public Transaction()
        {  }
    }
}
