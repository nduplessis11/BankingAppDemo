# BankingApp

## Overview

BankingApp is a modern, scalable, and industry-compliant banking application built with .NET 8, C#, and Azure. This project demonstrates best practices in Domain-Driven Design (DDD), microservices architecture, vertical slice architecture, and event-driven communication. It incorporates industry standards such as Financial Data Exchange (FDX) and Banking Industry Architecture Network (BIAN) to ensure interoperability, security, and compliance within the banking domain.

## Table of Contents

- [Architecture](#architecture)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Microservices](#microservices)
  - [AccountService](#accountservice)
  - [TransactionService](#transactionservice)
  - [CustomerService](#customerservice)
  - [IntegrationService](#integrationservice)
  - [AuditService](#auditservice)

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
