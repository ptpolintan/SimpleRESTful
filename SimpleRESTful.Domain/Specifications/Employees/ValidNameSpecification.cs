using System.Text.RegularExpressions;
using SimpleRESTful.Domain.Employees.Entities;

namespace SimpleRESTful.Domain.Specifications.Employees
{
    public class ValidNameSpecification : Specification<Employee>
    {
        private static readonly Regex _regex = new(@"^[a-zA-Z.\- ]+$");

        public override bool IsSatisfiedBy(Employee employee)
        {
            return IsValid(employee.FirstName) && IsValid(employee.LastName) && (string.IsNullOrWhiteSpace(employee.MiddleName) || IsValid(employee.MiddleName));
        }

        private static bool IsValid(string? name) => !string.IsNullOrWhiteSpace(name) && _regex.IsMatch(name);
    }
}