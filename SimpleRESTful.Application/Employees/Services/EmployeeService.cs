using SimpleRESTful.Application.Employees.DTOs.Request;
using SimpleRESTful.Application.Employees.DTOs.Response;
using SimpleRESTful.Application.Employees.Errors;
using SimpleRESTful.Application.Employees.Extensions;
using SimpleRESTful.Application.Employees.Interfaces;
using SimpleRESTful.Domain.Employees.Repository;
using SimpleRESTful.Domain.Specifications.Employees;

namespace SimpleRESTful.Application.Employees.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository service;

        public EmployeeService(IEmployeeRepository service)
        {
            this.service = service;
        }

        public async Task<CreateEmployeeResponse> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            var response = new CreateEmployeeResponse();

            var employee = request.AsEntity();

            var nameSpec = new ValidNameSpecification();
            if (!nameSpec.IsSatisfiedBy(employee))
            {
                response.Fail(EmployeeErrors.CreateError);
                return response;
            }

            try
            {
                var result = await service.InsertAsync(request.AsEntity());

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
                var result = await service.DeleteAsync(id);

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

        public async Task<GetEmployeeByIdResponse> GetEmployeeByIdAsync(int id)
        {
            var response = new GetEmployeeByIdResponse();

            try
            {
                var dto = (await service.GetByIdAsync(id))?.AsDTO();

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

        public async Task<GetEmployeesResponse> GetEmployeesAsync()
        {
            var response = new GetEmployeesResponse();

            try
            {
                response.Data = (await service.GetAllAsync())?.AsDTOs();

            }
            catch (Exception)
            {
                response.Fail(EmployeeErrors.InternalError);
            }
            return response;
        }

        public async Task<UpdateEmployeeResponse> UpdateEmployeeAsync(UpdateEmployeeRequest request)
        {
            var response = new UpdateEmployeeResponse();

            var employee = request.AsEntity();

            var nameSpec = new ValidNameSpecification();
            if (!nameSpec.IsSatisfiedBy(employee))
            {
                response.Fail(EmployeeErrors.UpdateError);
                return response;
            }

            try
            {
                var result = await service.UpdateAsync(request.AsEntity());

                if (result is null)
                {
                    response.Fail(EmployeeErrors.NotFound);
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
