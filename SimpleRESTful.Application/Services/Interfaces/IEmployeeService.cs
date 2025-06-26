using SimpleRESTful.Application.DTOs.Request;
using SimpleRESTful.Application.DTOs.Response;

namespace SimpleRESTful.Application.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<GetEmployeesResponseDTO> GetEmployeesAsync();
        public Task<GetEmployeeByIdResponseDTO> GetEmployeeByIdAsync(int id);
        public Task<CreateEmployeeResponseDTO> CreateEmployeeAsync(CreateEmployeeRequestDTO request);
        public Task<UpdateEmployeeResponseDTO> UpdateEmployeeAsync(UpdateEmployeeRequestDTO request);
        public Task<Response> DeleteEmployeeAsync(int id);
    }
}
