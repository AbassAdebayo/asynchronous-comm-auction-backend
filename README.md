# Async Auction Platform

## Table of Contents
1. [Introduction](#introduction)
2. [Technologies](#technologies)
3. [Design Decisions](#design-decisions)
4. [Getting Started](#getting-started)
5. [Running the Application](#running-the-application)
6. [Usage](#usage)
7. [Troubleshooting and FAQ](#troubleshooting-and-faq)
8. [Contributing](#contributing)
9. [License](#license)

## Introduction
**async-auction-platform** is a web application designed to provide an enhanced real-time communication system for online auctions. This platform allows users to enter a bidding room, submit bids, and receive notifications for the highest bids at the end of the auction. Additionally, it automatically generates invoices for the highest bidder. The project uses a microservices architecture with an asynchronous communication system for scalability and maintainability.

## Technologies
The following technologies and libraries are used in this project:

- **ASP.NET Core (.NET 7)**: A cross-platform framework for building web applications.
- **Duende IdentityServer**: For handling Single Sign-On (SSO) and OAuth2/OIDC authentication.
- **MassTransit**: A distributed application framework for building message-based systems.
- **SignalR**: For real-time communication.
- **YARP (Yet Another Reverse Proxy)**: For API Gateway functionality.
- **RabbitMQ**: A message broker for asynchronous messaging.
- **Clean Architecture**: For organizing code and enforcing separation of concerns.
- **CQRS (Command Query Responsibility Segregation)**: A pattern to separate read and write operations.
- **Repository Pattern**: For data access abstraction.
- **gRPC**: For high-performance communication between microservices.
- **Serilog**: For logging.
- **Paystack**: For payment processing.
- **PostgreSQL**: The primary relational database.
- **MongoDB**: For storing unstructured data.
- **OpenTelemetry and Grafana**: For observability and monitoring.
- **Docker and Kubernetes**: For containerization and orchestration.

## Design Decisions
- **Microservices Architecture**: To allow independent deployment, scaling, and development of different services.
- **Duende IdentityServer**: Chosen for secure and flexible authentication and authorization.
- **MassTransit and RabbitMQ**: Used for asynchronous communication and message handling between services.
- **YARP Gateway**: Provides a reverse proxy gateway for routing requests to appropriate services.
- **Clean Architecture**: Ensures high maintainability and testability.
- **CQRS and Repository Pattern**: For separation of command and query responsibilities and abstracted data access.

## Getting Started

To get started with the **async-auction-platform** project, follow these steps:

1. **Clone the repository**:
   ```bash
   git clone https://github.com/YusuffAhmad/async-auction-platform.git
2. **Navigate to the project directory**:

   ```bash
   cd async-auction-platform
3. **Restore the dependencies**: 
    ```bash
    dotnet restore
4. Update the database connection string in the respective `appsettings.Development.json` files to match your PostgreSQL
   database configuration.
5. Apply the database migrations for each project: `dotnet ef database update` (
   e.g., `dotnet ef database update --project src/AuctionService`)
6. Run each project individually using the `dotnet run` command (e.g., `dotnet run --project src/AuctionService`)

## Usage

Once the application is running, you can access the API documentation using Swagger UI. Open your web browser and
navigate to the respective URLs for each project:

- RoomsService: `https://localhost:5001/swagger/index.html`
- BiddingService: `https://localhost:7002/swagger/index.html`
- GatewayService: `https://localhost:6001/swagger/index.html`
- IdentityService: `https://localhost:5000`

Here, you can explore the available endpoints and test them.

## Contributing

Contributions to the AuctionNext project are welcome. To contribute, follow these steps:

1. Fork the repository
2. Create a new branch: `git checkout -b feature/your-feature-name`
3. Make your changes and commit them: `git commit -am 'Add some feature'`
4. Push the changes to your fork: `git push origin feature/your-feature-name`
5. Create a new pull request

## License

The AuctionNext project is licensed under the [MIT License](LICENSE).
