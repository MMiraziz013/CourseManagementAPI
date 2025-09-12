# 🏠 Home Task: Async & Dapper

## 📌 Описание

Реализовать **Course Management API** с использованием **ASP.NET Core WebAPI**, **Dapper** и **async/await**.
Все запросы должны выполняться асинхронно (`QueryAsync`, `ExecuteAsync`).

---

## 📂 Таблицы (PostgreSQL)

```sql
CREATE TABLE Students (
    Id SERIAL PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Phone VARCHAR(20)
);

CREATE TABLE Courses (
    Id SERIAL PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    DurationMonths INT NOT NULL,
    Price DECIMAL(10,2) NOT NULL
);

CREATE TABLE Groups (
    Id SERIAL PRIMARY KEY,
    CourseId INT REFERENCES Courses(Id),
    GroupName VARCHAR(100) NOT NULL,
    StartDate DATE NOT NULL
);

CREATE TABLE StudentGroups (
    Id SERIAL PRIMARY KEY,
    StudentId INT REFERENCES Students(Id),
    GroupId INT REFERENCES Groups(Id),
    JoinedAt TIMESTAMP NOT NULL
);

CREATE TABLE Payments (
    Id SERIAL PRIMARY KEY,
    StudentGroupId INT REFERENCES StudentGroups(Id),
    Amount DECIMAL(10,2) NOT NULL,
    PaidAt TIMESTAMP NOT NULL
);

CREATE TABLE Attendance (
    Id SERIAL PRIMARY KEY,
    StudentGroupId INT REFERENCES StudentGroups(Id),
    LessonDate DATE NOT NULL,
    IsPresent BOOLEAN NOT NULL
);
```

---

## 📝 Задания

### 🔹 Task 1. Список студентов с группами

Создать эндпоинт `GET /students` → вернуть список студентов и в каких группах они учатся, включая название курса.
Использовать `JOIN` по **Students → StudentGroups → Groups → Courses**.

---

### 🔹 Task 2. Список оплат студентов

Создать эндпоинт `GET /payments` → вернуть все оплаты с полями:

- StudentName
- GroupName
- CourseTitle
- Amount
- PaidAt

Использовать `JOIN` по **Payments → StudentGroups → Students → Groups → Courses**.

---

### 🔹 Task 3. Статистика по курсам

Создать эндпоинт `GET /courses/stats` → вернуть статистику:

- название курса
- количество студентов (через StudentGroups)
- общая сумма оплат по этому курсу

Использовать `JOIN + GROUP BY`.

---

### 🔹 Task 4. Поиск студентов по фильтрам

Создать эндпоинт `GET /students/search?name=...&course=...&minPayment=...`
Фильтрация:

- имя студента содержит `name`
- курс совпадает с `course`
- общая сумма оплат > `minPayment`

Использовать `JOIN` по таблицам.

---

### 🔹 Task 5. Отсутствующие студенты

Создать эндпоинт `GET /attendance/missing?date=...`
Вернуть список студентов, которые **не пришли** на занятие в указанную дату, включая:

- StudentName
- GroupName
- CourseTitle
- LessonDate

Использовать `JOIN` по **Attendance → StudentGroups → Students → Groups → Courses**, фильтровать `IsPresent = false` и дату.

---

## 🎯 Критерии оценки

- Все методы реализованы **асинхронно** (`async/await`).
- Использованы методы Dapper: `QueryAsync`, `ExecuteAsync`.
- SQL-запросы содержат `JOIN`, `GROUP BY`, фильтрацию.
- API протестировано через Swagger/Postman.

---
