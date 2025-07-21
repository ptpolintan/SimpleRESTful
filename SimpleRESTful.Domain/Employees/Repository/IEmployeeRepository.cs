using SimpleRESTful.Domain.Employees.Entities;

namespace SimpleRESTful.Domain.Employees.Repository
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetAllAsync();
        public Task<Employee?> GetByIdAsync(int id);
        public Task<Employee?> InsertAsync(Employee employee);
        public Task<Employee?> UpdateAsync(Employee employee);
        public Task<bool> DeleteAsync(int id);
    }
}
