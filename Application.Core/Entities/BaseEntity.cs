using System.ComponentModel.DataAnnotations;

namespace Application.Core.Entities
{
    abstract public class BaseEntity
    {
        [Key]
        public int ID { get; set; }
    }
}
