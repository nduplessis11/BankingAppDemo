# BankingApp

## Overview

BankingApp is a modern, scalable, and industry-compliant banking application built with .NET 8, C#, and Azure. This project demonstrates best practices in Domain-Driven Design (DDD), microservices architecture, vertical slice architecture, and event-driven communication. It incorporates industry standards such as Financial Data Exchange (FDX) and Banking Industry Architecture Network (BIAN) to ensure interoperability, security, and compliance within the banking domain.

## Table of Contents

- [Architecture](#architecture)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)

## Architecture

The project is designed using the following architectural principles:

- **Domain-Driven Design (DDD)**: The core business logic is encapsulated within rich domain models following DDD principles.
- **Microservices Architecture**: The application is divided into independent microservices, each responsible for a specific business domain.
- **Vertical Slice Architecture**: Each microservice is structured around specific features, encapsulating API, business logic, and data access layers within each slice.
- **Event-Driven Communication**: Services communicate asynchronously through events using RabbitMQ, ensuring loose coupling and scalability.
- **gRPC**: Utilized for efficient, high-performance synchronous communication between microservices.

## Features

- **FDX and BIAN Compliance**: Adheres to industry standards for secure and consistent financial data exchange.
- **Sharding**: Implements data sharding for enhanced performance and scalability.
- **Minimal APIs**: Utilizes .NET 8 minimal APIs for lightweight and performant API layers.
- **Azure Integration**: Leverages Azure services for hosting, security, and messaging.

## Technologies Used

- **.NET 8**: The primary framework for developing microservices.
- **C#**: The programming language used throughout the project.
- **Azure**: Cloud platform for hosting and services, including Azure Service Bus and Azure Kubernetes Service (AKS).
- **RabbitMQ**: Message broker for event-driven communication.
- **gRPC**: Framework for high-performance communication between microservices.
- **Docker**: Used for containerizing microservices.
- **Kubernetes**: Orchestrates the deployment and scaling of microservices.

## Project Structure
```
BankingApp
│
├── src
│   ├── AccountService
│   │   ├── AccountService.Api          # Minimal API and gRPC layer for account-related operations
│   │   ├── AccountService.Domain       # Domain models, entities, and value objects (FDX-compliant)
│   │   ├── AccountService.Application  # Application layer with CQRS commands, queries, gRPC handlers
│   │   ├── AccountService.Infrastructure # Infrastructure layer, repositories, RabbitMQ producers
│   │   └── AccountService.Tests        # Unit, integration, and gRPC tests for AccountService
│   │
│   ├── TransactionService
│   │   ├── TransactionService.Api      # Minimal API and gRPC layer for transaction-related operations
│   │   ├── TransactionService.Domain   # Domain models, entities, and value objects (FDX-compliant)
│   │   ├── TransactionService.Application # Application layer with CQRS commands, queries, gRPC handlers
│   │   ├── TransactionService.Infrastructure # Infrastructure layer, repositories, RabbitMQ producers and consumers
│   │   └── TransactionService.Tests    # Unit, integration, and gRPC tests for TransactionService
│   │
│   ├── CustomerService
│   │   ├── CustomerService.Api         # Minimal API and gRPC layer for customer management operations
│   │   ├── CustomerService.Domain      # Domain models, entities, and value objects (BIAN-compliant)
│   │   ├── CustomerService.Application # Application layer with CQRS commands, queries, gRPC handlers
│   │   ├── CustomerService.Infrastructure # Infrastructure layer, repositories, RabbitMQ consumers
│   │   └── CustomerService.Tests       # Unit, integration, and gRPC tests for CustomerService
│   │
│   ├── IntegrationService
│   │   ├── IntegrationService.Api      # Minimal API and gRPC layer for external system integrations
│   │   ├── IntegrationService.Domain   # Domain models, entities, and value objects (FDX/BIAN-compliant)
│   │   ├── IntegrationService.Application # Application layer with integration commands, queries, gRPC handlers
│   │   ├── IntegrationService.Infrastructure # Infrastructure layer, external API clients, RabbitMQ producers and consumers
│   │   └── IntegrationService.Tests    # Unit, integration, and gRPC tests for IntegrationService
│   │
│   ├── AuditService
│   │   ├── AuditService.Api            # Minimal API layer for auditing and logging
│   │   ├── AuditService.Domain         # Domain models, entities, and value objects for audit logs
│   │   ├── AuditService.Application    # Application layer with CQRS commands, queries
│   │   ├── AuditService.Infrastructure # Infrastructure layer, repositories, RabbitMQ consumers
│   │   └── AuditService.Tests          # Unit and integration tests for AuditService
│   │
│   ├── SharedKernel
│   │   ├── SharedKernel.Domain         # Common domain entities, value objects, exceptions
│   │   ├── SharedKernel.Infrastructure # Common infrastructure, gRPC services, RabbitMQ configurations
│   │   └── SharedKernel.Tests          # Shared kernel tests
│
└── build
    ├── Dockerfiles                     # Docker configurations for each microservice
    ├── AzurePipelines                  # CI/CD pipeline configurations for Azure DevOps
    └── Kubernetes                      # Kubernetes deployment configurations
```
