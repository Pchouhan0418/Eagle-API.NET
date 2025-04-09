# **Eagle-API.NET**

Welcome to **Eagle-API.NET**, an API framework built with **.NET 8** and following the principles of **Clean Architecture**. This project is designed to deliver scalable, maintainable, and robust solutions that adhere to modern software design practices.

## **Features**
- **Built with .NET 8**: Leverages the latest features and performance enhancements of .NET 8.
- **Clean Architecture**: Promotes separation of concerns, making the codebase easier to maintain and extend.
- **RESTful API Design**: Implements best practices for building modern APIs.
- **Middleware Support**: Integrates custom middleware for streamlined processing.
- **Error Handling**: Centralized and consistent error management.
- **Modular Structure**: Organized structure for better maintainability and extensibility.

## **Installation**
To get started with **Eagle-API.NET**, follow these steps:

1. **Clone the repository**:
   ```bash
   git clone https://github.com/Pchouhan0418/Eagle-API.NET.git
   cd Eagle-API.NET
   ```

2. **Set up your environment**:
   - Install [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
   - Ensure you have a suitable editor like Visual Studio or VS Code.

3. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

4. **Run the project**:
   ```bash
   dotnet run
   ```

5. **Access the API locally**:
   ```
   https://localhost:7126/swagger
   ```

## **Project Structure**
The **Eagle-API.NET** project is organized into a **Clean Architecture** style with the following layers:

- **Eagle.Application**: Contains the application logic, including use cases and DTOs (Data Transfer Objects).
- **Eagle.Domain**: Defines the core business logic and domain entities.
- **Eagle.Infrastructure**: Manages data access and third-party dependencies like databases, APIs, and external services.
- **EagleApi**: Serves as the API layer, containing controllers, middleware, and the main entry point for the application.
- **EagleApiUnitTest**: Includes unit tests for ensuring code reliability and correctness.

This structure ensures separation of concerns, scalability, and maintainability, making it easy to add new features and adapt to changing requirements.

## **Usage**
### **Endpoints**
The API includes sample endpoints to get you started. You can explore all available endpoints using the built-in Swagger UI:
```
https://localhost:7126/swagger
```

### **Configurations**
Modify the `appsettings.json` file to customize:
- Database connections
- Logging levels
- Environment-specific settings

## **Technologies Used**
- **.NET 8**: The latest version for high performance and modern features.
- **Entity Framework Core**: For database interactions.
- **Clean Architecture Principles**: Promotes maintainability and scalability.
- **Swagger/OpenAPI**: For API documentation and testing.

## **Contributing**
We welcome contributions to improve **Eagle-API.NET**! Here's how you can get involved:
1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add your feature description"
   ```
4. Push your changes:
   ```bash
   git push origin feature/your-feature-name
   ```
5. Submit a pull request with a description of your work.

## **License**
This project is licensed under the [MIT License](LICENSE).

## **Contact**
For more information or questions, feel free to reach out:
- **Repository Owner**: [Pchouhan0418](https://github.com/Pchouhan0418)

