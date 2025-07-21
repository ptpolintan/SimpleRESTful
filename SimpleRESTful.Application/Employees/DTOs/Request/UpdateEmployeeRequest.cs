namespace SimpleRESTful.Application.Employees.DTOs.Request
{
    public class UpdateEmployeeRequest
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
    }
}
