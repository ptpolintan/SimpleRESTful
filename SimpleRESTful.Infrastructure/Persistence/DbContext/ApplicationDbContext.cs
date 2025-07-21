using Microsoft.EntityFrameworkCore;
using SimpleRESTful.Domain.Employees.Entities;

namespace SimpleRESTful.Infrastructure.Persistence.DbContext
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }
    }
}
