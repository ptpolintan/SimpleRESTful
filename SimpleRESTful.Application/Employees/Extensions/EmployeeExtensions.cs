using SimpleRESTful.Application.Employees.DTOs;
using SimpleRESTful.Application.Employees.DTOs.Request;
using SimpleRESTful.Domain.Employees.Entities;

namespace SimpleRESTful.Application.Employees.Extensions
{
    public static class EmployeeExtensions
    {
        public static EmployeeModel AsDTO(this Employee entity)
        {
            return new EmployeeModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MiddleName = entity.MiddleName
            };
        }

        public static IEnumerable<EmployeeModel>? AsDTOs(this IEnumerable<Employee> entities)
        {
            return entities.Select(x => x.AsDTO());
        }

        public static Employee AsEntity(this UpdateEmployeeRequest request)
        {
            return new Employee
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName
            };
        }
        public static Employee AsEntity(this CreateEmployeeRequest request)
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
