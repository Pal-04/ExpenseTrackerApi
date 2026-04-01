# ExpenseTrackerApi

A RESTful API built using ASP.NET Core Web API for managing personal expenses.

## 🚀 Features
- User registration and login (password hashing using bcrypt)
- Expense and income tracking
- Category management
- Secure authentication (JWT - coming next)
- Clean architecture using DTOs and services

## 🛠️ Tech Stack
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- BCrypt for password hashing

## 📂 Project Structure
- Controllers – Handle API requests
- Models – Database entities
- DTOs – Data transfer objects
- Services – Business logic
- Data – DbContext and database configuration

## ▶️ How to Run
1. Clone the repository
2. Open in Visual Studio 2022
3. Update connection string in `appsettings.json`
4. Run migration:
5. Run the project:

## 📌 API Endpoints
### Auth
- POST /api/auth/register
- POST /api/auth/login

## 📊 Project Status
🚧 In progress (currently implementing JWT authentication)

## 💡 Learning Outcomes
- Built RESTful APIs using ASP.NET Core
- Implemented authentication with bcrypt
- Used Entity Framework Core for database operations
- Applied clean architecture principles