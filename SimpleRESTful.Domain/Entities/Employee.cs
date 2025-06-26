using System.ComponentModel.DataAnnotations;

namespace SimpleRESTful.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        [MaxLength(100)]
        public string? MiddleName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }
    }
}
