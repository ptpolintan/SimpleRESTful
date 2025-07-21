using SimpleRESTful.Application.Employees.DTOs;

namespace SimpleRESTful.Application.Employees.DTOs.Response
{
    public class UpdateEmployeeResponseDTO : Response
    {
        public EmployeeModel? Data { get; set; }
    }
}
