using Moq;
using SimpleRESTful.Application.Employees.DTOs.Request;
using SimpleRESTful.Application.Employees.Errors;
using SimpleRESTful.Application.Employees.Services;
using SimpleRESTful.Domain.Employees.Entities;
using SimpleRESTful.Domain.Employees.Repository;

namespace SimpleRESTful.Application.Test.Employees.Services;

[TestFixture]
public class EmployeeServiceTests
{
    private Mock<IEmployeeRepository> _repositoryMock;
    private EmployeeService _employeeService;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IEmployeeRepository>();
        _employeeService = new EmployeeService(_repositoryMock.Object);
    }

    [Test]
    public async Task CreateEmployeeAsync_ShouldReturnSuccess_WhenInsertSucceeds()
    {
        var request = new CreateEmployeeRequest { FirstName = "John", MiddleName = "Mid", LastName = "Doe" };
        var employee = new Employee { FirstName = "John", LastName = "Doe" };

        _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<Employee>())).ReturnsAsync(employee);

        var result = await _employeeService.CreateEmployeeAsync(request);

        Assert.That(result.Error, Is.Null);
        Assert.That(result.Data, Is.Not.Null);
    }

    [Test]
    public async Task CreateEmployeeAsync_ShouldFail_WhenNameIsInvalid()
    {
        var request = new CreateEmployeeRequest { FirstName = "John!", MiddleName = "M!d", LastName = "Doe" };

        var result = await _employeeService.CreateEmployeeAsync(request);

        Assert.That(result.Data, Is.Null);
        Assert.That(result.Error, Is.EqualTo(EmployeeErrors.CreateError));
    }

    [Test]
    public async Task CreateEmployeeAsync_ShouldFail_WhenInsertReturnsNull()
    {
        var request = new CreateEmployeeRequest { FirstName = "John", LastName = "Doe" };

        _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<Employee>())).ReturnsAsync((Employee)null);

        var result = await _employeeService.CreateEmployeeAsync(request);

        Assert.That(result.Error, Is.EqualTo(EmployeeErrors.CreateError));
    }

    [Test]
    public async Task CreateEmployeeAsync_ShouldFail_WhenExceptionThrown()
    {
        var request = new CreateEmployeeRequest { FirstName = "John", LastName = "Doe" };

        _repositoryMock.Setup(r => r.InsertAsync(It.IsAny<Employee>())).ThrowsAsync(new Exception());

        var result = await _employeeService.CreateEmployeeAsync(request);

        Assert.That(result.Error, Is.EqualTo(EmployeeErrors.InternalError));
    }

    [Test]
    public async Task DeleteEmployeeAsync_ShouldReturnSuccess_WhenDeleted()
    {
        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

        var result = await _employeeService.DeleteEmployeeAsync(1);

        Assert.That(result.Error, Is.Null); // Fixed this one
    }

    [Test]
    public async Task DeleteEmployeeAsync_ShouldFail_WhenDeleteReturnsFalse()
    {
        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);

        var result = await _employeeService.DeleteEmployeeAsync(1);

        Assert.That(result.Error, Is.EqualTo(EmployeeErrors.NotFound));
    }

    [Test]
    public async Task DeleteEmployeeAsync_ShouldFail_WhenExceptionThrown()
    {
        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

        var result = await _employeeService.DeleteEmployeeAsync(1);

        Assert.That(result.Error, Is.EqualTo(EmployeeErrors.InternalError));
    }

    [Test]
    public async Task GetEmployeeByIdAsync_ShouldReturnData_WhenFound()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Employee { FirstName = "John", LastName = "Doe" });

        var result = await _employeeService.GetEmployeeByIdAsync(1);

        Assert.That(result.Error, Is.Null);
        Assert.That(result.Data, Is.Not.Null);
    }

    [Test]
    public async Task GetEmployeeByIdAsync_ShouldFail_WhenNotFound()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Employee)null);

        var result = await _employeeService.GetEmployeeByIdAsync(1);

        Assert.That(result.Error, Is.EqualTo(EmployeeErrors.NotFound));
    }

    [Test]
    public async Task GetEmployeeByIdAsync_ShouldFail_WhenExceptionThrown()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

        var result = await _employeeService.GetEmployeeByIdAsync(1);

        Assert.That(result.Error, Is.EqualTo(EmployeeErrors.InternalError));
    }

    [Test]
    public async Task GetEmployeesAsync_ShouldReturnList_WhenSuccess()
    {
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Employee> { new Employee { FirstName = "John", LastName = "Doe" } });

        var result = await _employeeService.GetEmployeesAsync();

        Assert.That(result.Error, Is.Null);
        Assert.That(result.Data, Is.Not.Empty);
    }

    [Test]
    public async Task GetEmployeesAsync_ShouldFail_WhenExceptionThrown()
    {
        _repositoryMock.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception());

        var result = await _employeeService.GetEmployeesAsync();

        Assert.That(result.Error, Is.EqualTo(EmployeeErrors.InternalError));
    }

    [Test]
    public async Task UpdateEmployeeAsync_ShouldReturnSuccess_WhenUpdateSucceeds()
    {
        var request = new UpdateEmployeeRequest { Id = 1, FirstName = "John", LastName = "Doe" };

        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Employee>())).ReturnsAsync(new Employee { Id = 1, FirstName = "John", LastName = "Doe" });

        var result = await _employeeService.UpdateEmployeeAsync(request);

        Assert.That(result.Error, Is.Null);
        Assert.That(result.Data, Is.Not.Null);
    }

    [Test]
    public async Task UpdateEmployeeAsync_ShouldFail_WhenNameIsInvalid()
    {
        var request = new UpdateEmployeeRequest { Id = 1, FirstName = "J0hn!", LastName = "Doe$" };

        var result = await _employeeService.UpdateEmployeeAsync(request);

        Assert.That(result.Data, Is.Null);
        Assert.That(result.Error, Is.EqualTo(EmployeeErrors.UpdateError));
    }

    [Test]
    public async Task UpdateEmployeeAsync_ShouldFail_WhenUpdateReturnsNull()
    {
        var request = new UpdateEmployeeRequest { Id = 1, FirstName = "John", LastName = "Doe" };

        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Employee>())).ReturnsAsync((Employee)null);

        var result = await _employeeService.UpdateEmployeeAsync(request);

        Assert.That(result.Error, Is.EqualTo(EmployeeErrors.NotFound));
    }

    [Test]
    public async Task UpdateEmployeeAsync_ShouldFail_WhenExceptionThrown()
    {
        var request = new UpdateEmployeeRequest { Id = 1, FirstName = "John", LastName = "Doe" };

        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Employee>())).ThrowsAsync(new Exception());

        var result = await _employeeService.UpdateEmployeeAsync(request);

        Assert.That(result.Error, Is.EqualTo(EmployeeErrors.InternalError));
    }
}
