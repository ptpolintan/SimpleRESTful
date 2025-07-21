using SimpleRESTful.Application.Employees.DTOs.Request;
using SimpleRESTful.Application.Employees.DTOs.Response;

namespace SimpleRESTful.Application.Employees.Interfaces
{
    public interface IEmployeeService
    {
        Task<GetEmployeesResponse> GetEmployeesAsync();
        Task<GetEmployeeByIdResponse> GetEmployeeByIdAsync(int id);
        Task<CreateEmployeeResponse> CreateEmployeeAsync(CreateEmployeeRequest request);
        Task<UpdateEmployeeResponse> UpdateEmployeeAsync(UpdateEmployeeRequest request);
        Task<Response> DeleteEmployeeAsync(int id);
    }
}
