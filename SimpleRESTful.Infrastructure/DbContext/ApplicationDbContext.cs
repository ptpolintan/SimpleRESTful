using Microsoft.EntityFrameworkCore;
using SimpleRESTful.Domain.Entities;

namespace SimpleRESTful.Infrastructure.DbContext
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }
    }
}
