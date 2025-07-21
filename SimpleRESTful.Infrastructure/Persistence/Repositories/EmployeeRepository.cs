using Microsoft.EntityFrameworkCore;
using SimpleRESTful.Domain.Employees.Entities;
using SimpleRESTful.Domain.Employees.Repository;
using SimpleRESTful.Infrastructure.Persistence.DbContext;

namespace SimpleRESTful.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await context.Employees.FindAsync(id);
                if (entity == null)
                {
                    return false;
                }

                context.Employees.Remove(entity); //Hard deletes are a no for me aside from special situations. For the sake of passing this hands on, I'll do it.

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                //add logging here
                return false;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try
            {
                return await context.Employees.ToListAsync();
            }
            catch (Exception)
            {
                //add logging here
                return [];
            }
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            try
            {
                return await context.Employees.FindAsync(id);
            }
            catch (Exception)
            {
                //add logging here
                return null;
            }
        }

        public async Task<Employee?> InsertAsync(Employee employee)
        {
            try
            {
                await context.Employees.AddAsync(employee);
                await context.SaveChangesAsync();
                return employee;
            }
            catch (Exception)
            {
                //add logging here
                return null;
            }
        }

        public async Task<Employee?> UpdateAsync(Employee employee)
        {
            try
            {
                var entity = await context.Employees.FindAsync(employee.Id);
                if (entity == null)
                {
                    return null;
                }

                entity.FirstName = employee.FirstName;
                entity.MiddleName = employee.MiddleName;
                entity.LastName = employee.LastName;

                await context.SaveChangesAsync();

                return employee;
            }
            catch (Exception)
            {
                //add logging here
                return null;
            }
        }
    }
}
