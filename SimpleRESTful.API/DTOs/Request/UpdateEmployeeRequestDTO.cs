using Microsoft.AspNetCore.Mvc;

namespace SimpleRESTful.API.DTOs.Request
{
    public class UpdateEmployeeRequestDTO
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
    }
}
