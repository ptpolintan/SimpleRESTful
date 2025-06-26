using SimpleRESTful.Domain.Entities;

namespace SimpleRESTful.Infrastructure.Repositories.Interfaces
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
