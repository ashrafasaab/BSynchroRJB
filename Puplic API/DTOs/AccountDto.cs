namespace Puplic_API.DTOs
{
    public record AccountDto
    {
        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public int CustomerId { get; set; }
        public string Desciption { get; set; }

        public decimal CreditBalance { get; set; }
    }
}
