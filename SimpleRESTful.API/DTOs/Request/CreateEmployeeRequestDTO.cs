﻿namespace SimpleRESTful.API.DTOs.Request
{
    public class CreateEmployeeRequestDTO
    {
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
    }
}
