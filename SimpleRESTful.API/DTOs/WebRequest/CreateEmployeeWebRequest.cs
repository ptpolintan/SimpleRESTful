namespace SimpleRESTful.API.DTOs.WebRequest
{
    public class CreateEmployeeWebRequest
    {
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
    }
}
