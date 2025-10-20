# Phoenix Subscription Service

## Overview
This project is a simple backend system for managing user subscriptions. It allows users to view available subscription plans, subscribe to a plan, and view their active subscriptions.

---

## 1. Why this structure and technologies were chosen
I chose **Onion Architecture** to separate concerns into layers (Domain, Application, Infrastructure, API) for better maintainability, testability, and scalability.  
- **.NET 9 & ASP.NET Core Web API**: For fast development, high performance, and built-in dependency injection.  
- **Entity Framework Core with SQLite**: Simple and quick database setup suitable for a small project or prototype.  
- **Swagger (OpenAPI)**: For automatic API documentation and easy testing.  
- **BCrypt**: For secure password hashing.

---

## 2. What changes would be made if the project were implemented at a large scale
For a production-scale system, I would consider:  
- Using a production-grade database such as SQL Server or PostgreSQL.  
- Adding caching (e.g., Redis) for performance improvements.  
- Implementing full JWT authentication with refresh tokens.  
- Adding logging, monitoring, and error tracking.  
- Writing comprehensive unit and integration tests.  
- Handling concurrency and transaction management for subscriptions.  
- Possibly separating Users, Plans, and Subscriptions into microservices for better scalability.

---

## 3. Time spent on the task
Approximately **4–5 hours**.

---

## 4. Where AI was used to assist in coding
I used **ChatGPT** to:  
- Generate service and controller templates.  
- Suggest EF Core entity configurations.  
- Help with Swagger documentation setup.
