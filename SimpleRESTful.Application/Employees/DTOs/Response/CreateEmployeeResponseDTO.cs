using SimpleRESTful.Application.Employees.DTOs;

namespace SimpleRESTful.Application.Employees.DTOs.Response
{
    public class CreateEmployeeResponseDTO : Response
    {
        public EmployeeModel? Data { get; set; }
    }
}
