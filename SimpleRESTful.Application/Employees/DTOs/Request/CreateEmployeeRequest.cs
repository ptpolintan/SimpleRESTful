﻿namespace SimpleRESTful.Application.Employees.DTOs.Request
{
    public class CreateEmployeeRequest
    {
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
    }
}
