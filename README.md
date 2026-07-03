# SimpleOrderingAPI

> A robust, microservices-based ordering API built with .NET 8. Developed for OJT (On-the-Job Training), this project demonstrates scalable backend architecture, raw database operations, and secure authentication flows.

## Overview

SimpleOrderingAPI is designed to handle the core functionalities of an e-commerce or ordering platform. Instead of relying on an ORM like Entity Framework Core, this project utilizes **ADO.NET** for highly optimized, raw SQL database interactions. It implements a microservices architecture routed through an API Gateway, ensuring that individual domains (Users, Products, Orders) remain decoupled and independently scalable.

## Key Features

* **Microservices Architecture:** Independent services handling distinct business domains.
* **API Gateway:** A single, unified entry point for all client requests, routing traffic to the appropriate downstream microservices.
* **Raw Data Access:** Direct and performant database operations using ADO.NET.
* **Secure Authentication:** 
    * **JWT (JSON Web Tokens):** For secure, stateless API authorization.
    * **Google OAuth 2.0:** Integrated third-party authentication.
* **API Versioning:** Ensures backward compatibility for clients as the API evolves.
* **Unit Testing:** Comprehensive test coverage to ensure reliability across services.

## Architecture & Services

The solution is divided into several focused projects:

* `ApiGateway`: The central routing hub for incoming requests.
* `UserService`: Manages user profiles, registration, JWT generation, and Google OAuth 2 login flows.
* `ProductService`: Handles the product catalog, inventory status, and product management endpoints.
* `OrderService`: Manages cart operations, order placement, and payment/checkout processing.
* `CryptService`: A dedicated service for handling cryptographic operations, hashing, and encryption.
* `OrderingAPI.Shared`: A common class library containing shared DTOs, interfaces, and utility functions used across all microservices.
* `OrderingAPI.Tests`: The unit testing project ensuring the stability and correctness of the services.

## Tech Stack

* **Framework:** .NET 8 (C#)
* **Data Access:** ADO.NET
* **Database:** MySQL *(via `sql_activities.sql`)*
* **Authentication:** JWT Bearer Auth & Google OAuth 2.0 Identity
* **Architecture:** Microservices, API Gateway Pattern
* **Testing:** xUnit / Moq

## Getting Started

### Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* MySQL Server
* A Google Cloud Console project (for OAuth 2.0 Client ID and Secret)

### Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/Jccqt/SimpleOrderingAPI.git](https://github.com/Jccqt/SimpleOrderingAPI.git)
    cd SimpleOrderingAPI
    ```

2.  **Database Setup:**
    * Locate the `sql_activities.sql` file in the root directory.
    * Execute this script in your MySQL instance to generate the necessary tables and schema.

3.  **Configuration:**
    * Navigate to the `appsettings.json` (or use .NET User Secrets) in your respective services (`UserService`, `ProductService`, `OrderService`, etc.).
    * Update the configuration with your local database connection strings and security keys:
    
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=YourDbName;User=root;Password=YourPassword;"
    },
    "JwtSettings": {
      "Secret": "YOUR_SUPER_SECRET_JWT_KEY",
      "Issuer": "SimpleOrderingAPI",
      "Audience": "SimpleOrderingAPIClients"
    },
    "Authentication": {
      "Google": {
        "ClientId": "YOUR_GOOGLE_CLIENT_ID",
        "ClientSecret": "YOUR_GOOGLE_CLIENT_SECRET"
      }
    }
    ```

4.  **Run the Application:**
    Since this is a microservices architecture, you will need to start the `ApiGateway` along with the necessary background services. You can configure multiple startup projects in Visual Studio (`OrderingAPI.slnx`) or run them via CLI:
    ```bash
    dotnet run --project OrderingAPI/ApiGateway/ApiGateway.csproj
    dotnet run --project OrderingAPI/UserService/UserService.csproj
    # Repeat for other essential services
    ```

## Testing

To execute the unit tests and ensure everything is functioning correctly:

```bash
dotnet test OrderingAPI/OrderingAPI.Tests/OrderingAPI.Tests.csproj
