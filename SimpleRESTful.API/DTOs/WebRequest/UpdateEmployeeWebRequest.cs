using Microsoft.AspNetCore.Mvc;

namespace SimpleRESTful.API.DTOs.WebRequest
{
    public class UpdateEmployeeWebRequest
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
    }
}
