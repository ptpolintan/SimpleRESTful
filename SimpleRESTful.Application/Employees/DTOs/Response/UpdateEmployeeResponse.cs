using SimpleRESTful.Application.Employees.DTOs;

namespace SimpleRESTful.Application.Employees.DTOs.Response
{
    public class UpdateEmployeeResponse : Response
    {
        public EmployeeModel? Data { get; set; }
    }
}
