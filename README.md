# Mobile Wallet REST API

A lightweight RESTful API for a mobile wallet system built with ASP.NET Core, Entity Framework Core, and PostgreSQL.

This API allows you to create users with associated wallets, process deposits, and execute withdrawals with full transaction history and balance validation checks.

---

## Features

- **Wallet Management**: Register a user and automatically provision an active USD wallet.
- **Deposit Flow**: Credit an active wallet with balance validation (> 0).
- **Withdrawal Flow**: Debit an active wallet with insufficient balance validation (> balance) and transaction logging.
- **Audit Ledger**: All deposits and withdrawals generate transaction ledger entries linked to the wallet.

---

## Tech Stack

- **Framework**: ASP.NET Core 10.0 (Web API using Controllers)
- **Database ORM**: Entity Framework Core (EF Core)
- **Database Engine**: PostgreSQL
- **Database Driver**: `Npgsql.EntityFrameworkCore.PostgreSQL`

---

## Prerequisites

Ensure you have the following installed locally:
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [PostgreSQL](https://www.postgresql.org/) database server
- [EF Core CLI Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) (`dotnet-ef`)

---

## Getting Started

### 1. Clone & Navigate to Project
```bash
cd MobileWalletApi
```

### 2. Configure Database Connection
Update the connection string in `appsettings.json` (or `appsettings.Development.json`) to match your PostgreSQL server credentials:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=mobile_wallet;Username=your_username;Password=your_password"
}
```

### 3. Initialize the Database
If you do not have the database created, run:
```bash
createdb mobile_wallet
```
Alternatively, EF Core will automatically create it when applying migrations.

### 4. Apply Migrations
Apply the EF Core database migrations to set up the tables (`Users`, `Wallets`, `Transactions`):
```bash
dotnet ef database update
```

### 5. Build and Run the App
Restore dependencies and build:
```bash
dotnet build
```

Run the application:
```bash
dotnet run
```
The API will start hosting (default URL: `http://localhost:5141`).

---

## API Documentation & Usage Examples

### 1. Create a Wallet
Registers a new user and provisions a wallet.

* **Endpoint**: `POST /api/wallets`
* **Request Body**:
  ```json
  {
    "firstName": "Josphat",
    "lastName": "Ndhlovu",
    "phoneNumber": "0770000000"
  }
  ```

* **cURL Request**:
  ```bash
  curl -X POST "http://localhost:5141/api/wallets" \
    -H "Content-Type: application/json" \
    -d '{
      "firstName": "Josphat",
      "lastName": "Ndhlovu",
      "phoneNumber": "0770000000"
    }'
  ```

* **Example Response**:
  ```json
  {
    "walletId": "a2f3d8df-f5d0-40cc-b9a9-bab39f0b8546",
    "userId": "d748f219-c6bb-49e0-827c-3f2d019f18a6",
    "firstName": "Josphat",
    "lastName": "Ndhlovu",
    "phoneNumber": "0770000000",
    "balance": 0.00,
    "currency": "USD",
    "isActive": true
  }
  ```

---

### 2. Deposit Funds
Credits the balance of a wallet.

* **Endpoint**: `POST /api/wallets/{walletId}/deposit`
* **Request Body**:
  ```json
  {
    "amount": 500.00,
    "reference": "DEP-100234"
  }
  ```

* **cURL Request**:
  ```bash
  curl -X POST "http://localhost:5141/api/wallets/a2f3d8df-f5d0-40cc-b9a9-bab39f0b8546/deposit" \
    -H "Content-Type: application/json" \
    -d '{
      "amount": 500.00,
      "reference": "DEP-100234"
    }'
  ```

* **Example Response**:
  ```json
  {
    "id": "e932ba81-229d-4720-9bf3-5d51381de0f1",
    "walletId": "a2f3d8df-f5d0-40cc-b9a9-bab39f0b8546",
    "amount": 500.00,
    "type": "Deposit",
    "status": "Completed",
    "reference": "DEP-100234",
    "createdAt": "2026-08-14T20:00:00Z"
  }
  ```

---

### 3. Withdraw Funds
Debits the balance of a wallet if sufficient funds exist.

* **Endpoint**: `POST /api/wallets/{walletId}/withdrawal`
* **Request Body**:
  ```json
  {
    "amount": 50.00,
    "reference": "WITH-990312"
  }
  ```

* **cURL Request**:
  ```bash
  curl -X POST "http://localhost:5141/api/wallets/a2f3d8df-f5d0-40cc-b9a9-bab39f0b8546/withdrawal" \
    -H "Content-Type: application/json" \
    -d '{
      "amount": 50.00,
      "reference": "WITH-990312"
    }'
  ```

* **Example Response**:
  ```json
  {
    "id": "f5195ea4-3a52-4467-bc5b-ee4ea2d7083a",
    "walletId": "a2f3d8df-f5d0-40cc-b9a9-bab39f0b8546",
    "amount": 50.00,
    "type": "Withdrawal",
    "status": "Completed",
    "reference": "WITH-990312",
    "createdAt": "2026-08-14T20:05:00Z"
  }
  ```