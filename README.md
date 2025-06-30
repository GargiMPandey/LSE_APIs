**Tech Stack**

·	Backend Framework: ASP.NET Core 8 (Web API)

·	Language: C#

·	ORM: Entity Framework Core (with MySQL provider)

·	Database: MySQL

·	Authentication: JWT Bearer Tokens

·	API Documentation: Swagger (OpenAPI)

·	Dependency Injection: Built-in .NET DI

·	Logging & Error Handling: Custom middleware with structured logging


**Architecture**

·	Layered/Clean Architecture:

·	API Layer: Handles HTTP requests, authentication, and response formatting.

·	Core Layer: Contains business logic, DTOs, and service contracts.

·	Infrastructure Layer: Implements repositories, database context, and data access logic.

·	Dependency Injection: All services and repositories are injected for testability and maintainability.

·	Separation of Concerns: Each layer is responsible for a specific part of the application.


	
**Functionality**

·	Authentication:
·	Register and login endpoints for brokers.

·	Secure endpoints using JWT Bearer authentication.

·	Broker Management: Register and authenticate brokers.

·	Stock Management:
·	Retrieve stock values by ticker.

·	Retrieve all stocks or a range of stocks.

·	Trade Management: Submit new trades (authenticated).

·	Error Handling: Centralized exception handling middleware for consistent error responses.

·	API Documentation: Interactive Swagger UI for exploring and testing endpoints.



--------------------------------------------------------------------------------------------------
**Potential Bottlenecks in current Implementation-**


Single Database Instance:
The architecture relies on a single database. As load increases, the database can become a bottleneck. The queries and filtering will become slower.

No Caching Layer:
Frequently accessed data (e.g., stock values) is always fetched from the database. This increases database load and latency.
We can consider caching for traded stocks within the trading window. 

Synchronous Data Processing:
Trade data is processed by the http calls through APIs, once the data is completely processed within DB then the response is sent back to client.

No Rate Limiting or Throttling:
Without rate limiting, the API is vulnerable to abuse and accidental overload.

Logging & Monitoring:
Lack of detailed logs can make troubleshooting and monitoring harder at scale.

Exception Handling:
Proper error handling for production can be implemented.

Authentication & Secrets storage:
Secrets can be stored in keyvault, user password can be hashed and processed.

-------------------------------------------------------------------------------------------------------------------------
**Scalability considerations for future enhancements-**


**Event-Driven Architecture:** Use a message broker such as Kafka, RabbitMQ, or Azure Service Bus to publish price changes. Consumers (ex- SignalR) can then broadcast updates to connected clients.

**Enabling horizontal scaling:** Configure auto-scaling rules based on CPU, memory, or queue length.

**Distributed Caching:** Use a distributed cache (ex- Redis) to store frequently accessed stock prices and reduce database load.

**DB optimization:** Consider adding indexes for fast search. For very large tables, consider partitioning by date or ticker. For heavy read operations, use database read replicas to distribute load.

**CQRS Pattern:** Separate read and write models. Reads can be optimized for speed and scale, while writes can be handled asynchronously if needed.

**High Availability & Fault Tolerance :** Implement health checks for all services and dependencies. Use retry and circuit breaker patterns for transient failures. Serve cached or last-known prices if the database is temporarily unavailable.

**Cloud-Native & DevOps considerations:** Use Docker etc and orchestrate with Kubernetes,AKS for auto-scaling and resilience.  For observability integrate distributed tracing, logging, and metrics using azure application Insights.

**API gateway Usage:** We can consider implementing rate limiting and throttling through Api gateway by azure api management, it will help to cope with high traffic and prevents individual users or clients from overwhelming the system.
