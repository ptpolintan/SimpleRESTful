using Microsoft.EntityFrameworkCore;
using SimpleRESTful.Domain.Employees.Repository;
using SimpleRESTful.Application.Employees.Services;
using SimpleRESTful.Application.Employees.Interfaces;
using SimpleRESTful.Infrastructure.Persistence.DbContext;
using SimpleRESTful.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
var x = builder.Configuration.GetConnectionString("MSSQL");
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")));


builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
