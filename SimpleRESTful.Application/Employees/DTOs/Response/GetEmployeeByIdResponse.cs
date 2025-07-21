using SimpleRESTful.Application.Employees.DTOs;

namespace SimpleRESTful.Application.Employees.DTOs.Response
{
    public class GetEmployeeByIdResponse : Response
    {
        public EmployeeModel? Data { get; set; }
    }
}
