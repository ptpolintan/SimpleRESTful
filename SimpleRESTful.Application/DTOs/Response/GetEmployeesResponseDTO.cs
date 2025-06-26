namespace SimpleRESTful.Application.DTOs.Response
{
    public class GetEmployeesResponseDTO : Response
    {
        public IEnumerable<EmployeeDTO>? Data { get; set; }
    }
}
