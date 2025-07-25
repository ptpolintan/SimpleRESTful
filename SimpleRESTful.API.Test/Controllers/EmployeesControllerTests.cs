using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleRESTful.API.Controllers;
using SimpleRESTful.API.DTOs.WebRequest;
using SimpleRESTful.API.Test.Utilities.Assertions;
using SimpleRESTful.Application.Employees.DTOs;
using SimpleRESTful.Application.Employees.DTOs.Request;
using SimpleRESTful.Application.Employees.DTOs.Response;
using SimpleRESTful.Application.Employees.Interfaces;

namespace SimpleRESTful.API.Test.Controllers
{
    [TestFixture]
    public class Tests
    {
        private EmployeesController _controller;
        private Mock<IEmployeeService> _serviceMock;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IEmployeeService>();
            _controller = new EmployeesController(_serviceMock.Object);
        }

        [Test]
        public async Task GetEmployeeById_ShouldReturn200_WhenEmployeeExists()
        {
            // Arrange
            var employeeDto = new EmployeeModel { Id = 1, FirstName = "John", LastName = "Doe" };
            _serviceMock.Setup(s => s.GetEmployeeByIdAsync(1))
                .ReturnsAsync(new GetEmployeeByIdResponse { Data = employeeDto });

            // Act
            var result = await _controller.GetEmployeeById(1);

            // Assert
            var response = ObjectResultAssertions.AssertObjectResult<GetEmployeeByIdResponse>(result, 200);
            Assert.That(response.Data!.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task GetEmployeeById_ShouldReturn404_WhenNotFound()
        {
            _serviceMock.Setup(s => s.GetEmployeeByIdAsync(99))
                .ReturnsAsync(new GetEmployeeByIdResponse { Data = null, Error = "NotFound" });

            var result = await _controller.GetEmployeeById(99);

            var response = ObjectResultAssertions.AssertObjectResultWithError<GetEmployeeByIdResponse>(result, 404);
            Assert.That(response.Error, Is.EqualTo("NotFound"));
        }

        [Test]
        public async Task GetEmployeeById_ShouldReturn500_WhenExceptionThrown()
        {
            _serviceMock.Setup(s => s.GetEmployeeByIdAsync(99))
                .ReturnsAsync(new GetEmployeeByIdResponse { Data = null, Error = "InternalError" });

            var result = await _controller.GetEmployeeById(99);

            var response = ObjectResultAssertions.AssertObjectResultWithError<GetEmployeeByIdResponse>(result, 500);
            Assert.That(response.Error, Is.EqualTo("InternalError"));
        }

        [Test]
        public async Task GetEmployees_ShouldReturn200_WithEmployeeCollection()
        {
            // Arrange
            var employees = new List<EmployeeModel>    {
                new EmployeeModel { Id = 1, FirstName = "John", LastName = "Doe" },
                new EmployeeModel { Id = 2, FirstName = "Jane", LastName = "Doe" }
            };

            _serviceMock.Setup(s => s.GetEmployeesAsync())
                .ReturnsAsync(new GetEmployeesResponse { Data = employees });

            // Act
            var result = await _controller.GetEmployees();

            // Assert
            var response = ObjectResultAssertions.AssertObjectResult<GetEmployeesResponse>(result, 200);
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Data!.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetEmployees_ShouldReturn200_WithEmptyCollection()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetEmployeesAsync())
                .ReturnsAsync(new GetEmployeesResponse { Data = Enumerable.Empty<EmployeeModel>() });

            // Act
            var result = await _controller.GetEmployees();

            // Assert
            var response = ObjectResultAssertions.AssertObjectResult<GetEmployeesResponse>(result, 200);
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(!response.Data!.Any());
        }

        [Test]
        public async Task GetEmployees_ShouldReturn500_WhenServiceFails()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetEmployeesAsync())
                .ReturnsAsync(new GetEmployeesResponse { Data = null, Error = "Database failure" });

            // Act
            var result = await _controller.GetEmployees();

            // Assert
            var response = ObjectResultAssertions.AssertObjectResultWithError<GetEmployeesResponse>(result, 500);
            Assert.That(response.Error, Is.EqualTo("Database failure"));
        }

        [Test]
        public async Task CreateEmployee_ShouldReturn201_WhenSuccessful()
        {
            // Arrange
            var webRequest = new CreateEmployeeWebRequest { FirstName = "John", MiddleName = "Mid", LastName = "Doe" };
            _serviceMock.Setup(s => s.CreateEmployeeAsync(It.IsAny<CreateEmployeeRequest>()))
                .ReturnsAsync(new CreateEmployeeResponse
                {
                    Data = new EmployeeModel { Id = 1, FirstName = "John", LastName = "Doe" }
                });

            // Act
            var result = await _controller.CreateEmployee(webRequest);

            // Assert
            var response = ObjectResultAssertions.AssertObjectResult<CreateEmployeeResponse>(result, 201);
            Assert.That(response.Data!.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateEmployee_ShouldReturn422_WhenDbInsertionFails()
        {
            // Arrange
            var webRequest = new CreateEmployeeWebRequest { FirstName = "", LastName = "" };
            _serviceMock.Setup(s => s.CreateEmployeeAsync(It.IsAny<CreateEmployeeRequest>()))
                .ReturnsAsync(new CreateEmployeeResponse
                {
                    Error = "CreateError"
                });

            // Act
            var result = await _controller.CreateEmployee(webRequest);

            // Assert
            var response = ObjectResultAssertions.AssertObjectResultWithError<CreateEmployeeResponse>(result, 422);
            Assert.That(response.Error, Is.EqualTo("CreateError"));
        }

        [Test]
        public async Task CreateEmployee_ShouldReturn500_WhenUnexpectedErrorOccurs()
        {
            // Arrange
            var webRequest = new CreateEmployeeWebRequest { FirstName = "Jane", LastName = "Doe" };
            _serviceMock.Setup(s => s.CreateEmployeeAsync(It.IsAny<CreateEmployeeRequest>()))
                .ReturnsAsync(new CreateEmployeeResponse
                {
                    Error = "Unexpected database error."
                });

            // Act
            var result = await _controller.CreateEmployee(webRequest);

            // Assert
            var response = ObjectResultAssertions.AssertObjectResultWithError<CreateEmployeeResponse>(result, 500);
            Assert.That(response.Error, Is.EqualTo("Unexpected database error."));
        }

        [Test]
        public async Task UpdateEmployees_ShouldReturn200_WhenSuccessful()
        {
            // Arrange
            var webRequest = new UpdateEmployeeWebRequest
            {
                FirstName = "John",
                LastName = "Doe",
                MiddleName = "Middle"
            };

            _serviceMock.Setup(s => s.UpdateEmployeeAsync(It.IsAny<UpdateEmployeeRequest>()))
                .ReturnsAsync(new UpdateEmployeeResponse
                {
                    Data = new EmployeeModel { Id = 1, FirstName = "John", LastName = "Doe" }
                });

            // Act
            var result = await _controller.UpdateEmployees(1, webRequest);

            // Assert
            var response = ObjectResultAssertions.AssertObjectResult<UpdateEmployeeResponse>(result, 200);
            Assert.That(response.Data!.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateEmployees_ShouldReturn422_WhenDbUpdateFails()
        {
            // Arrange
            var webRequest = new UpdateEmployeeWebRequest
            {
                FirstName = "John",
                LastName = "Doe",
                MiddleName = "Middle"
            };

            _serviceMock.Setup(s => s.UpdateEmployeeAsync(It.IsAny<UpdateEmployeeRequest>()))
                .ReturnsAsync(new UpdateEmployeeResponse
                {
                    Error = "UpdateError"
                });

            // Act
            var result = await _controller.UpdateEmployees(1, webRequest);

            // Assert
            var response = ObjectResultAssertions.AssertObjectResultWithError<UpdateEmployeeResponse>(result, 422);
            Assert.That(response.Error, Is.EqualTo("UpdateError"));
        }

        [Test]
        public async Task UpdateEmployees_ShouldReturn500_WhenUnexpectedErrorOccurs()
        {
            // Arrange
            var webRequest = new UpdateEmployeeWebRequest
            {
                FirstName = "John",
                LastName = "Doe",
                MiddleName = "Middle"
            };

            _serviceMock.Setup(s => s.UpdateEmployeeAsync(It.IsAny<UpdateEmployeeRequest>()))
                .ReturnsAsync(new UpdateEmployeeResponse
                {
                    Error = "Unexpected error during update."
                });

            // Act
            var result = await _controller.UpdateEmployees(1, webRequest);

            // Assert
            var response = ObjectResultAssertions.AssertObjectResultWithError<UpdateEmployeeResponse>(result, 500);
            Assert.That(response.Error, Is.EqualTo("Unexpected error during update."));
        }

        [Test]
        public async Task DeleteEmployees_ShouldReturn200_WhenSuccessful()
        {
            _serviceMock.Setup(s => s.DeleteEmployeeAsync(1))
                .ReturnsAsync(new Response());

            var result = await _controller.DeleteEmployees(1);

            var response = ObjectResultAssertions.AssertObjectResult<Response>(result, 200);
            Assert.That(response.Error, Is.Null);
        }

        [Test]
        public async Task DeleteEmployees_ShouldReturn404_WhenEmployeeNotFound()
        {
            _serviceMock.Setup(s => s.DeleteEmployeeAsync(99))
                .ReturnsAsync(new Response
                {
                    Error = "NotFound"
                });

            var result = await _controller.DeleteEmployees(99);

            var response = ObjectResultAssertions.AssertObjectResultWithError<Response>(result, 404);
            Assert.That(response.Error, Is.EqualTo("NotFound"));
        }

        [Test]
        public async Task DeleteEmployees_ShouldReturn500_WhenUnexpectedErrorOccurs()
        {
            _serviceMock.Setup(s => s.DeleteEmployeeAsync(1))
                .ReturnsAsync(new Response
                {
                    Error = "Unexpected DB failure."
                });

            var result = await _controller.DeleteEmployees(1);

            var response = ObjectResultAssertions.AssertObjectResultWithError<Response>(result, 500);
            Assert.That(response.Error, Is.EqualTo("Unexpected DB failure."));
        }
    }
}
