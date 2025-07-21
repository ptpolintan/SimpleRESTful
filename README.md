I'm putting this out for visibility. This was submitted to a specific company as a hands-on exam. They manage to fail me with these requirements:


<h1>Design and Implement a RESTful API for an Employee Management System</h1>
 
<h3>This project involves building a RESTful API to manage employee data, incorporating the following endpoints:</h3>
 
1. GET /employees – Retrieve a list of all employees.
2. GET /employees/{id} – Retrieve detailed information for a specific employee by their unique ID.
3. POST /employees – Add a new employee to the system. 
4. PUT /employees/{id} – Update the information of an existing employee identified by ID. 
5. DELETE /employees/{id} – Remove an employee from the system by their ID.
 
 
<h3>Employee Resource Structure</h3>
 
<h3>The employee data will be modeled based on the following schema:</h3>
 
Employee Table:
1.  Id (integer)
2.  FirstName (string)
3.  MiddleName (string)
4.  LastName (string)
 
<h3>Implementation Requirements</h3>
 
1. Technology Stack

The API will be developed using either .NET Framework or .NET Core, following REST architectural principles to ensure proper resource management and scalability.
 
 
2. Design Pattern

The repository pattern will be used to handle data access logic, promoting a clear separation from business logic. This enhances maintainability and simplifies future modifications.
 
 
3. Database

A relational database management system (RDBMS) will store employee data. The choice of database—such as Microsoft SQL Server, MySQL, PostgreSQL, or SQLite—will be based on project-specific requirements. The schema outlined above will be used as the basis for the database model.
 
 
4. Error Handling

The API will include robust error handling to ensure that all responses are accompanied by appropriate HTTP status codes and informative error messages. This will facilitate better understanding and debugging for API clients.
