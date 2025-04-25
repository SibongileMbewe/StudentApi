# StudentApi

**Test 3 - REST API**  
A RESTful API that manages student data using CRUD operations. This API is designed to interact with students' data via HTTP requests and supports JSON responses.

## Features

- Create, Read, Update, and Delete (CRUD) student data
- JSON format for requests and responses
- Simple API for managing student records

## Technologies

- C#
- ASP.NET Core
- Entity Framework Core
- xUnit for Unit Testing

## Project Structure

- `StudentApi.Controllers`: API controllers
- `StudentApi.Models`: Models for student data
- `StudentApi.Data`: Database context
- `StudentApi.Tests`: Contains unit tests for the API

## How to Run

1. Clone this repository
2. Open the solution in Visual Studio
3. Run the API project (`StudentApi`)
4. Test API endpoints using Postman or any HTTP client

## Unit Tests

Unit tests are located in the `/Tests` folder (`StudentApiTests.cs`).

## Assumptions

- Students are uniquely identified by an ID
- CRUD operations are supported for student records

## Time Taken

Approximately 5 hours
