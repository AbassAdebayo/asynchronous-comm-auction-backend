# Asynchronous Communication Auction Chat

## Table of Contents
1. [Introduction](#introduction)
2. [Technologies](#technologies)
3. [Design Decisions](#design-decisions)
4. [Setup Instructions](#setup-instructions)
5. [Running the Application](#running-the-application)
6. [How to Use](#how-to-use)
7. [FAQ and Troubleshooting](#faq-and-troubleshooting)
8. [Contributing](#contributing)
9. [License](#license)

## Introduction
**Asynchronous Communication Auction Chat** is a web-based platform developed to facilitate real-time communication during online auctions. Users can enter bidding rooms, place bids, and receive notifications on the highest bids in real-time. Once an auction ends, the system generates an invoice for the winning bidder automatically. The project utilizes microservices architecture with asynchronous messaging to ensure scalability and efficient communication.

## Technologies
The project leverages the following technologies and libraries:

- **ASP.NET Core (.NET 7)**: For building cross-platform web applications.
- **Duende IdentityServer**: For Single Sign-On (SSO) and authentication using OAuth2/OIDC protocols.
- **MassTransit**: Enables message-based communication in distributed systems.
- **SignalR**: Facilitates real-time communication.
- **YARP (Yet Another Reverse Proxy)**: Acts as an API Gateway.
- **RabbitMQ**: Used for asynchronous message queuing.
- **Clean Architecture**: Maintains separation of concerns and organizes code effectively.
- **CQRS (Command Query Responsibility Segregation)**: Separates read and write operations for better scalability.
- **Repository Pattern**: Abstraction layer for data access.
- **gRPC**: Ensures high-performance communication between services.
- **Serilog**: Manages logging.
- **Paystack**: Integrates payment processing.
- **SQL Server**: The primary relational database.
- **MongoDB**: Stores unstructured data.
- **OpenTelemetry and Grafana**: Provides observability and monitoring.
- **Docker and Kubernetes**: Enables containerization and orchestration.

## Design Decisions
- **Microservices**: Independent services allow separate deployment, scaling, and development of features.
- **Duende IdentityServer**: Provides secure and flexible user authentication and authorization.
- **MassTransit with RabbitMQ**: Used for asynchronous communication between services to improve scalability.
- **YARP Gateway**: Functions as a reverse proxy, routing requests to respective services.
- **Clean Architecture**: Ensures high testability, maintainability, and separation of concerns.
- **CQRS and Repository Pattern**: Offers efficient separation of queries and commands, with abstracted data access.

## Setup Instructions

To start working with the **Asynchronous Communication Auction Chat** project, follow these steps:

1. **Clone the repository**:

   git clone https://github.com/AbassAdebayo/asynchronous-comm-auction-backend.git


2. **Move to the project folder**:

   cd asynchronous-comm-auction-backend
  

3. **Restore the required dependencies**:
  
   dotnet restore
   

4. **Update database connection strings**: 
   Modify the `appsettings.Development.json` files in each service to reflect your SQL Server database configuration.

5. **Apply database migrations**: 
   Run migrations for each service using `dotnet ef database update`. Example:
   
   **dotnet ef database update --project src/AuctionService**
   

6. **Run the projects**:
   Start each service individually:
   
   dotnet run --project src/AuctionService


## How to Use

After the application is running, you can explore its API using Swagger UI. Open a browser and access the documentation for each service at the following URLs:

- **Rooms Service**: `https://localhost:5001/swagger/index.html`
- **Bidding Service**: `https://localhost:7002/swagger/index.html`
- **Gateway Service**: `https://localhost:6001/swagger/index.html`
- **Identity Service**: `https://localhost:5000`

This allows you to interact with and test the available API endpoints.

## Contributing

We welcome contributions to improve the project. To contribute:

1. Fork the repository.
2. Create a new branch: 
   
   git checkout -b feature/your-feature-name
   
3. Implement your changes and commit:
  
   git commit -am 'Added a new feature'
   
4. Push your branch:

   git push origin feature/your-feature-name

5. Submit a pull request for review.

## License

This project is distributed under the [MIT License](LICENSE).
