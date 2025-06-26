using Microsoft.AspNetCore.Mvc;
using SimpleRESTful.API.DTOs.Request;
using SimpleRESTful.Application.DTOs.Response;

namespace SimpleRESTful.API.Extensions
{
    public static class EmployeeExtensions
    {
        //all the 404 are for the sake of errors for now. can design an even better design based on what error will be thrown beyond the controller layer
        public static ObjectResult AsHttpResponse(this GetEmployeesResponseDTO response)
        {
            response.TimeStamp = DateTime.UtcNow;

            return new ObjectResult(response)
            {
                StatusCode = ErrorStatusCode(response.Error)
            };
        }

        public static ObjectResult AsHttpResponse(this GetEmployeeByIdResponseDTO response)
        {
            response.TimeStamp = DateTime.UtcNow;

            return new ObjectResult(response)
            {
                StatusCode = ErrorStatusCode(response.Error)
            };
        }

        public static ObjectResult AsHttpResponse(this UpdateEmployeeResponseDTO response)
        {
            response.TimeStamp = DateTime.UtcNow;

            return new ObjectResult(response)
            {
                StatusCode = ErrorStatusCode(response.Error)
            };
        }

        public static ObjectResult AsHttpResponse(this CreateEmployeeResponseDTO response)
        {
            response.TimeStamp = DateTime.UtcNow;

            return new ObjectResult(response)
            {
                StatusCode = response.Error is null ? StatusCodes.Status201Created : ErrorStatusCode(response.Error)
            };
        }

        public static ObjectResult AsHttpResponse(this Response response)
        {
            response.TimeStamp = DateTime.UtcNow;

            return new ObjectResult(response)
            {
                StatusCode = ErrorStatusCode(response.Error)
            };
        }

        public static Application.DTOs.Request.UpdateEmployeeRequestDTO AsDTO(this UpdateEmployeeRequestDTO request)
        {
            return new Application.DTOs.Request.UpdateEmployeeRequestDTO
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName
            };
        }
        public static Application.DTOs.Request.CreateEmployeeRequestDTO AsDTO(this CreateEmployeeRequestDTO request)
        {
            return new Application.DTOs.Request.CreateEmployeeRequestDTO
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName
            };
        }

        private static int ErrorStatusCode(string? error)
        {
            return error switch
            {
                null => StatusCodes.Status200OK,
                "NotFound" => StatusCodes.Status404NotFound,
                "InternalError" => StatusCodes.Status500InternalServerError,
                "CreateError" => StatusCodes.Status422UnprocessableEntity,
                "UpdateError" => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
