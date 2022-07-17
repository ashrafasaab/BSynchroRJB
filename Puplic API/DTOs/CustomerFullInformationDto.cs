namespace Puplic_API.DTOs
{
    public class CustomerFullInformationDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
    }
}
