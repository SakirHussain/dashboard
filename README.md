# Overview

This repository contains the source code for which includes components such as controllers, repositories, interfaces, and database operations. The system is designed to manage and generate various types of reports related to Einvoices.
# Table of Contents

- [Project Structure](#project-structure)
- [Configuration](#configuration)
- [Database](#database)
- [API Documentation](#api-documentation)
- [Usage](#usage)

# Project Structure

The codebase is organized into several components:

- **Controllers:** The controllers handle HTTP requests, interacting with repositories to process and respond to client requests. The Main controller is the entry point of the API, allowing it to redirect to other methods/controllers based on the request.

- **Data:** This folder contains the `ApplicationDbContext` class, representing the Entity Framework Core context for database interactions.

- **Interfaces:** The `DatabaseOperationsInterface` and `HeaderVerificationInterface` define the contracts for database operations and header verification. The interfaces are the only ones that take part in the dependency injection.

- **Repositories:** The `DatabaseOperations` and `HeaderVerification` classes implement the respective interfaces, providing the actual implementations for database operations and header verification.

# Configuration

The system is configured using the `appsettings.json` file, where connection strings for different databases are specified. Logging settings and allowed hosts are also configured in this file.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnect": "Server=`redacted`\\`redacted`; Database=GrossIncomeDatabase; Trusted_Connection=True; Encrypt=False; MultipleActiveResultSets=true",
    "EInvoice": "server=`redacted`\\`redacted`;uid=`redacted`;pwd=`redacted`;database=`redacted`;Connect Timeout=120;MultipleActiveResultSets=False; Max Pool Size=120;TrustServerCertificate=True",
    "EwayBillOfficer": "server=`redacted`\\`redacted`;uid=`redacted`;pwd=`redacted`;database=`redacted`;Connect Timeout=120;MultipleActiveResultSets=False; Max Pool Size=12;TrustServerCertificate=True"
  }
}

```

# Database
- **Software:** SQL Server Management Studio was used to create and access the underlaying data in a code-first approach.
- The essential data was exposed using the overlaying API for access to other mobile/web applications
- `Stored Procedures` were used to seamlessly fetch data from across databases

# API Documentation

## Authentication

To authenticate your requests, include the following headers:

- `Client-Id`
- `Client-Secret`
- `Login-Id`
- `Token`

## Request Model

The API accepts POST requests with the following model:

```json
{
  "action": "<action>",
  "data": "<required data>"
}
```
- Action and Data
    - Action: Specifies the action to be performed.
    - Data: Contains serialized JSON data relevant to the action.
<br>

- Actions
    - Get Token
            To obtain a new token, send a POST request with the following details:

        - `Action`: "Get Token"
        - `Data`: Serialized JSON containing :
            - `LoginId`: [Your Login Id]
            - `Password`: [Your Password]
            - `PhoneNumber`: [Your Phone Number]

        Upon successful verification, a new token will be assigned with a validity of 1 hour.  
    <br>
    
    - Get Report
    To request a report, send a POST request with the following details:

        - `Action`: "Get Report"
        - `Data`: Serialized JSON containing the required values for the report.

## Response Model

The API response will follow the model:

```json
{
    "status": "<status of execution>",
    "error": "<error details>",
    "data": "<requested data>"
}
```
- Passed
```json
{
    "status": 1,
    "error": null,
    "data": "<requested data>"
}
```
- Failed
```json
{
    "status": 0,
    "error": {
        "errorCode": 104,
        "errorMessage": "<error code description>",
        "timeStamp": "2023-12-29T13:25:59.2184446+05:30"
    },
    "data": null
}
```
## Usage

- Request Example
```json
{
    "Action": "Get Token",
    "Data": {
        "LoginId": "example_login",
        "Password": "example_password",
        "PhoneNumber": "1234567890"
    }
}
```
- Response Example (Status 1)
```json
{
    "Status": 1,
    "Error": null,
    "Data": "{...serialized JSON result...}"
    }
```
- Response Example (Status 0)
```json
{
    "Status": 0,
    "Error": {
        "ErrorCode": 1001,
        "ErrorMessage": "Invalid credentials",
        "TimeStamp": "2023-01-01T12:34:56.789Z"
    },
    "Data": null
}
```
