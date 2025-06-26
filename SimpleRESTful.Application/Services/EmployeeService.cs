using SimpleRESTful.Application.DTOs.Request;
using SimpleRESTful.Application.DTOs.Response;
using SimpleRESTful.Application.Errors;
using SimpleRESTful.Application.Extensions;
using SimpleRESTful.Application.Services.Interfaces;
using SimpleRESTful.Infrastructure.Repositories.Interfaces;

namespace SimpleRESTful.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository service;

        public EmployeeService(IEmployeeRepository service)
        {
            this.service = service;
        }

        public async Task<CreateEmployeeResponseDTO> CreateEmployeeAsync(CreateEmployeeRequestDTO request)
        {
            var response = new CreateEmployeeResponseDTO();

            try
            {
                var result = await this.service.InsertAsync(request.AsEntity());

                if (result is null)
                {
                    response.Fail(EmployeeErrors.CreateError);
                }

                response.Data = result?.AsDTO();
            }
            catch (Exception)
            {
                response.Fail(EmployeeErrors.InternalError);
            }

            return response;
        }

        public async Task<Response> DeleteEmployeeAsync(int id)
        {
            var response = new Response();

            try
            {
                var result = await this.service.DeleteAsync(id);

                if (!result)
                {
                    response.Fail(EmployeeErrors.NotFound);
                }
            }
            catch (Exception)
            {
                response.Fail(EmployeeErrors.InternalError);
            }

            return response;
        }

        public async Task<GetEmployeeByIdResponseDTO> GetEmployeeByIdAsync(int id)
        {
            var response = new GetEmployeeByIdResponseDTO();

            try
            {
                var dto = (await this.service.GetByIdAsync(id))?.AsDTO();

                if (dto is not null)
                {
                    response.Data = dto;
                }
                else
                {
                    response.Fail(EmployeeErrors.NotFound);
                }
            }
            catch (Exception)
            {
                response.Fail(EmployeeErrors.InternalError);
            }

            return response;
        }

        public async Task<GetEmployeesResponseDTO> GetEmployeesAsync()
        {
            var response = new GetEmployeesResponseDTO();

            try
            {
                response.Data = (await this.service.GetAllAsync())?.AsDTOs();

            }
            catch (Exception)
            {
                response.Fail(EmployeeErrors.InternalError);
            }
            return response;
        }

        public async Task<UpdateEmployeeResponseDTO> UpdateEmployeeAsync(UpdateEmployeeRequestDTO request)
        {
            var response = new UpdateEmployeeResponseDTO();

            try
            {
                var result = await this.service.UpdateAsync(request.AsEntity());

                if (result is null)
                {
                    response.Fail(EmployeeErrors.UpdateError);
                }

                response.Data = result?.AsDTO();
            }
            catch (Exception)
            {
                response.Fail(EmployeeErrors.InternalError);
            }


            return response;
        }
    }
}
