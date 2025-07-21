using SimpleRESTful.Application.Employees.DTOs.Request;
using SimpleRESTful.Application.Employees.DTOs.Response;

namespace SimpleRESTful.Application.Employees.Interfaces
{
    public interface IEmployeeService
    {
        Task<GetEmployeesResponseDTO> GetEmployeesAsync();
        Task<GetEmployeeByIdResponseDTO> GetEmployeeByIdAsync(int id);
        Task<CreateEmployeeResponseDTO> CreateEmployeeAsync(CreateEmployeeRequest request);
        Task<UpdateEmployeeResponseDTO> UpdateEmployeeAsync(UpdateEmployeeRequest request);
        Task<Response> DeleteEmployeeAsync(int id);
    }
}
