using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SimpleRESTful.Application.Employees.DTOs.Response;

namespace SimpleRESTful.API.Test.Utilities.Assertions
{
    public static class ObjectResultAssertions
    {
        public static TResponse AssertObjectResult<TResponse>(
            IActionResult result,
            int expectedStatusCode)
            where TResponse : class
        {
            var objectResult = result as ObjectResult;
            Assert.That(objectResult, Is.Not.Null, "Expected ObjectResult but got null.");
            Assert.That(objectResult!.StatusCode, Is.EqualTo(expectedStatusCode), $"Expected status {expectedStatusCode} but got {objectResult.StatusCode}.");

            var response = objectResult.Value as TResponse;
            Assert.That(response, Is.Not.Null, $"Expected response body of type {typeof(TResponse).Name} but got null.");

            // Optional: if your responses always set TimeStamp
            if (response is Response responseWithTimestamp)
            {
                Assert.That(responseWithTimestamp.TimeStamp, Is.Not.EqualTo(default(DateTime)), "Expected TimeStamp to be set.");
            }

            return response!;
        }

        public static TResponse AssertObjectResultWithError<TResponse>(IActionResult result, int expectedStatusCode) where TResponse : Response
        {
            var response = AssertObjectResult<TResponse>(result, expectedStatusCode);
            Assert.That(response.Error, Is.Not.Null.Or.Empty, "Expected error message but got null.");
            return response;
        }
    }
}