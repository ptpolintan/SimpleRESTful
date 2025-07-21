using SimpleRESTful.Application.Employees.DTOs;

namespace SimpleRESTful.Application.Employees.DTOs.Response
{
    public class GetEmployeesResponse : Response
    {
        public IEnumerable<EmployeeModel>? Data { get; set; }
    }
}
