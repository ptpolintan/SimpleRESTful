using SimpleRESTful.Application.DTOs;
using SimpleRESTful.Application.DTOs.Request;
using SimpleRESTful.Domain.Entities;

namespace SimpleRESTful.Application.Extensions
{
    public static class EmployeeExtensions
    {
        public static EmployeeDTO AsDTO(this Employee entity)
        {
            return new EmployeeDTO
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MiddleName = entity.MiddleName
            };
        }

        public static IEnumerable<EmployeeDTO>? AsDTOs(this IEnumerable<Employee> entities)
        {
            return entities.Select(x => x.AsDTO());
        }

        public static Employee AsEntity(this UpdateEmployeeRequestDTO request)
        {
            return new Employee
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName
            };
        }
        public static Employee AsEntity(this CreateEmployeeRequestDTO request)
        {
            return new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName
            };
        }
    }
}
