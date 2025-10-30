# Student_Management

# TryMVC — Student Management (Simple ASP.NET Core MVC)

A small ASP.NET Core MVC sample demonstrating basic account flows and student records:
- Session-based login
- Two-step signup (Sign -> Form using TempData)
- Student list (Index) — username `admin` shows all students, others see only their record

## Features
- Login (username + password)
- Sign up via Sign -> Form (temp data between steps)
- Persist student records with EF Core DbContext (`AppDbContext`)
- Simple admin gate using username `"admin"`

## Prerequisites
- .NET SDK 6.0+ installed (Windows)
- Optional: EF Core tools (`dotnet tool install --global dotnet-ef`) if using migrations
- Database: SQL Server / LocalDB or SQLite (adjust connection string in appsettings.json)

## Setup & Run (development)
1. Clone the repo:
   - git clone <your-repo-url>
2. Configure connection string:
   - Update `appsettings.json` (do not commit production credentials)
3. (Optional) Apply EF migrations:
   - dotnet ef database update --project TryMVC
4. Build and run:
   - dotnet build
   - dotnet run --project TryMVC
5. Browse to the app URL printed in the console, or use:
   - /Account/Login
   - /Account/Sign (Sign -> Form)
   - /Account/Index (requires login)

## Important Security Notes (must include in report)
- Passwords are currently stored and compared in plaintext (critical security risk). Do not use this code in production.
- TempData is used to transfer raw passwords between actions — avoid transferring credentials in TempData/session.
- No role-based authorization or logout action implemented.
- Do not commit `appsettings.json` with secrets; use Secret Manager or environment variables.

## Project layout
- Controllers: `Controllers\HomeController.cs` (AccountController)
- Models: `Models\FormModel`, `Models\LoginModel`, `Models\SignModel`
- Data: `Models\AppDbContext` (EF Core DbContext)
