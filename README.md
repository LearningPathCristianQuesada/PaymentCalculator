# PaymentCalculator

A .NET 8 ASP.NET Core API that demonstrates clean architecture principles using the Strategy pattern for payment calculation and the Repository + Unit of Work patterns with EF Core for persistence.

## What is implemented

- Strategy pattern for payment fee calculation:
  - Credit Card
  - PayPal
  - Crypto
- Repository pattern to abstract persistence operations
- Unit of Work pattern to coordinate transactional saves
- EF Core InMemory provider for simple local persistence and testing
- ASP.NET Core Web API with Swagger support

## Project structure

- Controllers: API endpoints
- Services: business logic orchestration
- Strategies: pluggable payment calculation strategies
- Factories: strategy selection based on the requested payment method
- Data:
  - DbContext
  - Models
  - Repositories
  - Unit of Work

## How it works

The payment service receives a request containing an amount and a payment method. It selects the appropriate strategy through the factory, calculates the fee and total amount, persists the payment through the repository and unit of work, and returns a response DTO.

## Example request

```http
POST /Payment
Content-Type: application/json

{
  "amount": 100,
  "paymentMethod": 1
}
```

## Example response

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "amount": 100,
  "fee": 5,
  "totalAmount": 105,
  "paymentMethod": "CreditCard",
  "createdAt": "2026-07-08T00:00:00Z"
}
```

## Run locally

```bash
dotnet restore
dotnet build
dotnet run --project PaymentCalculator
```

Then open Swagger at:

```text
https://localhost:5001/swagger
```

## Tests

```bash
dotnet test PaymentCalculator.Tests/PaymentCalculator.Tests.csproj
```
