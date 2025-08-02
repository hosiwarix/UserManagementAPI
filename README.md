# UserManagementAPI

A simple ASP.NET Core Web API for managing users, featuring:

- **Request/response logging middleware**
- **Standardized error handling**
- **Token-based authentication**
- **Swagger/OpenAPI documentation**

## Features

- Add, retrieve, update, and delete users via RESTful endpoints
- Middleware for logging HTTP method, path, and response status code
- Consistent JSON error responses for unhandled exceptions
- Simple token authentication (hardcoded for demo: `mysecrettoken`)
- In-memory user storage (no database required)

## Getting Started

### Prerequisites

- [.NET 6 SDK or later](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/downloads) (optional, for version control)

### Running the API

1. **Clone the repository:**
   ```sh
   git clone https://github.com/hosiwarix/UserManagementAPI.git
   cd UserManagementAPI
   ```

2. **Restore and run the project:**
   ```sh
   dotnet run
   ```

3. **Open Swagger UI:**  
   Visit [http://localhost:5130/swagger](http://localhost:5130/swagger) in your browser.

## Authentication

All endpoints require a bearer token:

```
Authorization: Bearer mysecrettoken
```

## Example Requests

See [`requests.http`](./requests.http) for ready-to-use HTTP requests.

## Project Structure

- `Program.cs` – Configures middleware and API pipeline
- `Controllers/UsersController.cs` – User management endpoints
- `Models/User.cs` – User data model

## Middleware Order

1. **Error-handling middleware** – returns JSON error responses
2. **Authentication middleware** – checks for valid token
3. **Logging middleware** – logs requests and responses

## License

MIT License
