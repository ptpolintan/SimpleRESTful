using Microsoft.EntityFrameworkCore;
using Moq;
using SimpleRESTful.Domain.Employees.Entities;
using SimpleRESTful.Infrastructure.Persistence.DbContext;
using SimpleRESTful.Infrastructure.Persistence.Repositories;
namespace SimpleRESTful.Infrastructure.Test.Persistence.Repositories
{
    [TestFixture]
    public class EmployeeRepositoryTests
    {
        private ApplicationDbContext _context;
        private EmployeeRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new EmployeeRepository(_context);
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task InsertAsync_ShouldAddEmployee()
        {
            var employee = new Employee { FirstName = "Jane", MiddleName = "Mid" , LastName = "Doe" };

            var result = await _repository.InsertAsync(employee);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.GreaterThan(0));

            var inDb = await _context.Employees.FindAsync(result.Id);
            Assert.That(inDb, Is.Not.Null);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnEmployee_WhenExists()
        {
            var employee = new Employee { FirstName = "John", LastName = "Smith" };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(employee.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.FirstName, Is.EqualTo("John"));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            var result = await _repository.GetByIdAsync(999);
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllEmployees()
        {
            _context.Employees.AddRange(
                new Employee { FirstName = "A", LastName = "AA" },
                new Employee { FirstName = "B", LastName = "BB" }
            );
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllAsync();
            Assert.That(result, Has.Exactly(2).Items);
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveEmployee_WhenExists()
        {
            var employee = new Employee { FirstName = "ToDelete", LastName = "Employee" };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var result = await _repository.DeleteAsync(employee.Id);

            Assert.That(result, Is.True);

            var deleted = await _context.Employees.FindAsync(employee.Id);
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_ShouldReturnFalse_WhenNotFound()
        {
            var result = await _repository.DeleteAsync(999);
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateEmployee_WhenExists()
        {
            var employee = new Employee { FirstName = "Old", MiddleName = "Mid", LastName = "Name" };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            employee.FirstName = "New";
            employee.LastName = "Last";

            var result = await _repository.UpdateAsync(employee);

            Assert.That(result, Is.Not.Null);

            var updated = await _context.Employees.FindAsync(employee.Id);
            Assert.That(updated!.FirstName, Is.EqualTo("New"));
        }

        [Test]
        public async Task UpdateAsync_ShouldReturnNull_WhenNotFound()
        {
            var fake = new Employee { Id = 999, FirstName = "Ghost", LastName = "Employee" };

            var result = await _repository.UpdateAsync(fake);

            Assert.That(result, Is.Null);
        }

        // Exception-handling tests

        [Test]
        public async Task InsertAsync_ShouldReturnNull_WhenExceptionThrown()
        {
            var contextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            contextMock.Setup(c => c.Employees.AddAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Simulated insert failure"));

            var repository = new EmployeeRepository(contextMock.Object);
            var result = await repository.InsertAsync(new Employee { Id = 1, FirstName = "John", LastName = "Doe" });

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnNull_WhenExceptionThrown()
        {
            var contextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            contextMock.Setup(c => c.Employees.FindAsync(It.IsAny<object[]>()))
                .ThrowsAsync(new Exception("Simulated read failure"));

            var repository = new EmployeeRepository(contextMock.Object);
            var result = await repository.GetByIdAsync(1);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenExceptionThrown()
        {
            var contextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            // Make the DbSet throw an exception when accessed
            var dbSetMock = new Mock<DbSet<Employee>>();
            dbSetMock.As<IQueryable<Employee>>().Setup(m => m.Provider).Throws(new Exception("Simulated LINQ failure"));

            contextMock.Setup(c => c.Employees).Returns(dbSetMock.Object);

            var repository = new EmployeeRepository(contextMock.Object);

            var result = await repository.GetAllAsync();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task DeleteAsync_ShouldReturnFalse_WhenExceptionThrown()
        {
            var contextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            contextMock.Setup(c => c.Employees.FindAsync(It.IsAny<object[]>()))
                .ThrowsAsync(new Exception("Simulated delete failure"));

            var repository = new EmployeeRepository(contextMock.Object);
            var result = await repository.DeleteAsync(1);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task UpdateAsync_ShouldReturnNull_WhenExceptionThrown()
        {
            var contextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            contextMock.Setup(c => c.Employees.FindAsync(It.IsAny<object[]>()))
                .ThrowsAsync(new Exception("Simulated update failure"));

            var repository = new EmployeeRepository(contextMock.Object);
            var result = await repository.UpdateAsync(new Employee { Id = 1, FirstName = "John", LastName = "Doe" });

            Assert.That(result, Is.Null);
        }
    }
}