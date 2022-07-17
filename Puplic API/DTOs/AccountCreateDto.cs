namespace Puplic_API.DTOs
{
    public record AccountCreateDto
    {
        public int CustomerId { get; set; }
        public string? Desciption { get; set; }
        public decimal InitialCredit { get; set; }

    }
}
