# ExpenseTrackerApi

A RESTful API built using ASP.NET Core Web API for managing personal expenses with secure authentication and user-specific data handling.

---

## 🚀 Features

* User registration and login with **BCrypt password hashing**
* **JWT-based authentication** for secure access
* Add, update, delete, and view transactions (Income/Expense)
* Category-based expense tracking
* User-specific data using JWT claims
* Clean architecture using DTOs and services
* Async/await for efficient database operations

---

## 🔐 Authentication

* Implemented **JWT (JSON Web Token)** authentication
* Protected APIs using `[Authorize]`
* Secure password storage using BCrypt

---

## 🛠️ Tech Stack

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* BCrypt (Password Hashing)
* JWT Authentication

---

## 📂 Project Structure

* Controllers – Handle API requests
* Models – Database entities
* DTOs – Data transfer objects
* Services – Business logic
* Data – DbContext and database configuration

---

## ▶️ How to Run

1. Clone the repository
2. Open in Visual Studio 2022
3. Update connection string in `appsettings.json`
4. Run migrations:

   ```
   Add-Migration InitialCreate
   Update-Database
   ```
5. Run the project
6. Use Postman to test APIs

---

## 📌 API Endpoints

### 🔐 Auth

* POST `/api/auth/register`
* POST `/api/auth/login`

### 💰 Transactions (Protected)

* GET `/api/transactions`
* POST `/api/transactions`
* PUT `/api/transactions/{id}`
* DELETE `/api/transactions/{id}`

---

## 📊 Project Status

✅ Completed (Core features implemented)

---

## 💡 Learning Outcomes

* Built RESTful APIs using ASP.NET Core
* Implemented JWT authentication and authorization
* Applied DTO pattern for clean API responses
* Used async/await for scalable backend operations
* Worked with Entity Framework Core and relational data
* Implemented user-specific data handling using claims

---

## 📌 Future Improvements

* Expense reports (monthly/category-wise)
* Pagination and filtering
* Role-based authorization
* Swagger documentation

---
