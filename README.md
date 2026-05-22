# Motocross Lap Time Tracker

ASP.NET Core 8 Web API + React frontend. Riders register, log in, and manage their own lap time records. Built with Clean Architecture, ADO.NET (no EF/Dapper), JWT authentication, and TDD.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [Node.js 18+](https://nodejs.org/)
- SQL Server (Express or full) — connection string uses Windows auth by default
- [SQL Server Management Studio](https://aka.ms/ssmsfullsetup) or `sqlcmd`

---

## 1 — Database Setup

Run the scripts in order against your SQL Server instance:

```sql
-- Step 1: create the database (if it doesn't exist)
CREATE DATABASE Tracker;
USE Tracker;

-- Step 2: create tables
-- Run contents of: create_tables.txt

-- Step 3: create stored procedures
-- Run contents of: stored_procedures.txt

-- Step 4 (optional): load demo data
-- Run contents of: seed_data.sql
```

> All three script files (`create_tables.txt`, `stored_procedures.txt`, `seed_data.sql`) are in the root of this repository.

---

## 2 — Backend Configuration

Open `src/API/appsettings.json` and fill in the `DefaultConnection` value with your SQL Server instance:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER\\SQLEXPRESS;Database=Tracker;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "y23or/I/3TArs54YdouWDcNk9ftdkhqXQl2ViKF2ZyqFVQNta+U4WKEqGueFGsnc",
    "Issuer": "MotocrossTracker",
    "Audience": "MotocrossTrackerUsers"
  }
}
```

Replace `YOUR_SERVER` with your SQL Server instance name (e.g. `DESKTOP-ABC123\SQLEXPRESS`).

> **Note:** The connection string in `appsettings.json` is intentionally left empty. You must add your own value before running the API. The JWT key is included for convenience in this demo project — in production it should be stored in environment variables or a secrets manager.

---

## 3 — Run the Backend

```bash
# From the repo root
dotnet run --project src/API
```

The API starts on `https://localhost:7169` by default.

Swagger UI is available at: `https://localhost:7169/swagger`

To test JWT-protected endpoints in Swagger:
1. Call `POST /api/auth/register` or `POST /api/auth/login`
2. Copy the `token` from the response
3. Click **Authorize** (top right), enter `Bearer <token>`

---

## 4 — Run the Frontend

```bash
# From the frontend/ folder
cd frontend
npm install
npm run dev
```

The dev server starts on `http://localhost:5173`. All `/api/*` requests are proxied to the backend automatically (configured in `vite.config.js`).

---

## 5 — Run the Tests

```bash
# All tests
dotnet test

# Unit tests only
dotnet test tests/UnitTests

# Specific test
dotnet test --filter "FullyQualifiedName~LapTimeServiceTests"
```

There are 18 unit tests covering domain validation, service business logic, and JWT token generation.

---

## Demo Credentials (after running seed_data.sql)

> **Note:** The seed script inserts pre-hashed passwords. The values below are for reference only — register a new account via the UI for a fully working login.

| Username | Email |
|---|---|
| `jorge_mx` | `jorge@example.com` |
| `alex_rider` | `alex@example.com` |

To create a working demo account, register through the app at `http://localhost:5173`.

---

## Project Structure

```
src/API/
  Domain/         # Entities (LapTime, User), repository interfaces
  Application/    # Service interfaces, DTOs, business logic services
  Infrastructure/ # ADO.NET repositories, SqlConnectionFactory, TokenService
  Controllers/    # LapTimesController, AuthController
  Program.cs      # DI wiring, middleware, JWT config

tests/
  UnitTests/      # xUnit tests — Services, Auth, Domain

frontend/
  src/
    api/          # client.js — central fetch wrapper with JWT header
    context/      # AuthContext — global auth state, localStorage
    components/   # LapTimeForm, LapTimeTable
    pages/        # AuthPage, DashboardPage
```

---

## Architecture Notes

- **Clean Architecture** — dependency rule enforced via namespaces (Domain ← Application ← Infrastructure ← API)
- **ADO.NET + Stored Procedures** — no ORM; SQL lives in the database, not scattered across C# files
- **IDbConnectionFactory** — repositories receive this interface; switching databases requires one line change in `Program.cs`
- **JWT (HS256)** — stateless auth; secret key in `appsettings.json` (move to environment variables in production)
- **Ownership enforcement** — two layers: service checks `UserId` before calling repo; SQL `WHERE` clause also filters by `UserId`
- **TDD** — test files were written before implementation; interfaces were shaped by test requirements
