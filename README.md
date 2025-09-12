# 📘 Course Management API

----

## 🎯 Project Goal

The Course Management API is a learning project built to practice ASP.NET Core Web API, PostgreSQL, and Dapper ORM.
It simulates a small education management system where administrators can manage:

Students 👨‍🎓

* Courses 📚
* Groups 🏫
* Payments 
* 💵 Attendance 📅

The project also demonstrates clean architecture principles, async/await usage, and modern REST API design.

----
## ⚙️ Technologies Used

* **.NET 8 / ASP.NET Core Web API** – for building RESTful endpoints 
* **PostgreSQL** – relational database for persisting data 
* **Dapper ORM** – lightweight, fast data access with SQL 
* **Swagger / OpenAPI** – API documentation & testing 
* **Async/Await** – asynchronous database queries for scalability

**Clean Architecture – separation of concerns with Application, Infrastructure, Domain, and API layers**

----

## 🗄️ Database Schema

### The system uses several related tables:

**_Students Table_**
```postgresql
CREATE TABLE Students (
      Id SERIAL PRIMARY KEY,
      FullName VARCHAR(100) NOT NULL,
      Phone VARCHAR(20)
);
```
**_Courses Table_**

```postgresql
   CREATE TABLE Courses (
   Id SERIAL PRIMARY KEY,
   Title VARCHAR(100) NOT NULL,
   DurationMonths INT NOT NULL,
   Price DECIMAL(10,2) NOT NULL
   );
```
**_Groups Table_**
```postgresql
   CREATE TABLE Groups (
   Id SERIAL PRIMARY KEY,
   CourseId INT REFERENCES Courses(Id) ON DELETE CASCADE,
   GroupName VARCHAR(100) NOT NULL,
   StartDate DATE NOT NULL
   );
```
**_Student Groups Table (many-to-many)_**
```postgresql 
   CREATE TABLE StudentGroups (
   Id SERIAL PRIMARY KEY,
   StudentId INT REFERENCES Students(Id) ON DELETE CASCADE,
   GroupId INT REFERENCES Groups(Id) ON DELETE CASCADE,
   JoinedAt TIMESTAMP NOT NULL
   );
```
**_Payments Table_**
```postgresql
   CREATE TABLE Payments (
   Id SERIAL PRIMARY KEY,
   StudentGroupId INT REFERENCES StudentGroups(Id) ON DELETE CASCADE,
   Amount DECIMAL(10,2) NOT NULL,
   PaidAt TIMESTAMP NOT NULL
   );
```
**_Attendance Table_**
```postgresql
   CREATE TABLE Attendance (
   Id SERIAL PRIMARY KEY,
   StudentGroupId INT REFERENCES StudentGroups(Id) ON DELETE CASCADE,
   LessonDate DATE NOT NULL,
   IsPresent BOOLEAN NOT NULL
   );
```

## 🔄 Async/Await Usage

### The project makes asynchronous database queries to improve scalability.

**_Example:_**

```csharp
using (var conn = _context.GetConnection())
{
    var sql = """
        SELECT
        c.title AS Title,
        c.durationmonths AS DurationMonths,
        c.price AS Price,
        COUNT(s.id) AS StudentCount,
        SUM(p.amount) AS TotalPayments
        FROM studentgroups sg
        LEFT JOIN groups g ON sg.groupid = g.id
        LEFT JOIN courses c ON g.courseid = c.id
        LEFT JOIN payments p ON sg.id = p.studentgroupid
        LEFT JOIN students s ON sg.studentid = s.id
        GROUP BY c.title, c.durationmonths, c.price """;

    var list = await conn.QueryAsync<CourseDto>(sql);
    return list.ToList();
}
```

**This ensures that while waiting for the DB, the thread is not blocked and the API can handle more requests _concurrently_.**

## 📡 API Overview

**_Some example endpoints:_**

```
GET /students → Get all students

GET /students/search?name=...&course=...&minPayment=... → Filter students

POST /students → Add new student

GET /courses → Get course statistics (students, total payments, etc.)

POST /attendance → Record student attendance

GET /attendance/missed?date=2025-09-12 → Get missing attendance for a given date
```

## 🚀 Running the Project

**1. Clone the repo**

```
git clone <repo-url>
cd CourseManagementAPI
```
**2. Set up PostgreSQL and run the table creation scripts.**

**3. Configure the connection string in appsettings.json:**

```
"ConnectionStrings": {
"DefaultConnection": "Host=localhost;Port=5432;Database=course_db;Username=postgres;Password=yourpassword"
}
```
**4. Run the API:**

```
dotnet run
```



**5. Open Swagger UI:**

``` 
http://localhost:xxxx/swagger
```


**✅ With this setup, you can test all API methods via Swagger or Postman.**
