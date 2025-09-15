# 🏠 Home Task: Async, Dapper & QR Code

## 📌 Описание

Реализовать **Course Management API** с использованием:

- **ASP.NET Core WebAPI**
- **Dapper**
- **async/await** (`QueryAsync`, `ExecuteAsync`)
- **QR Code Generator** (например, библиотека [QRCoder](https://github.com/codebude/QRCoder))

Все запросы выполняются асинхронно.

---

## 📂 Таблицы (PostgreSQL)

(Без изменений, см. твой вариант)
`Students`, `Courses`, `Groups`, `StudentGroups`, `Payments`, `Attendance`

---

## 📝 Задания

### 🔹 Task 1. Список студентов с группами

`GET /students` → вернуть список студентов и групп с курсами.
JOIN: **Students → StudentGroups → Groups → Courses**.

---

### 🔹 Task 2. Список оплат студентов

`GET /payments` → вернуть оплаты с полями:

- StudentName
- GroupName
- CourseTitle
- Amount
- PaidAt
  JOIN: **Payments → StudentGroups → Students → Groups → Courses**.

---

### 🔹 Task 3. Статистика по курсам

`GET /courses/stats` → вернуть:

- название курса
- количество студентов
- общая сумма оплат
  JOIN + `GROUP BY`.

---

### 🔹 Task 4. Поиск студентов по фильтрам

`GET /students/search?name=...&course=...&minPayment=...`
Фильтры:

- имя содержит `name`
- курс = `course`
- сумма оплат > `minPayment`

---

### 🔹 Task 5. Отсутствующие студенты

`GET /attendance/missing?date=...` → вернуть студентов, которые **не пришли** на занятие.
JOIN: **Attendance → StudentGroups → Students → Groups → Courses**
Фильтр: `IsPresent = false` и дата.

---

### 🔹 Task 6. Генерация QR-кода (NEW 🎉)

Красавчик 👍 Теперь перепишем твой Task 6 так, чтобы студенты генерировали **QR-код со ссылкой на профиль студента**. Это будет реально похоже на живую систему.

---

# 🏠 Home Task: Async, Dapper & QR Code

## 📌 Описание

Реализовать **Course Management API** с использованием:

- **ASP.NET Core WebAPI**
- **Dapper**
- **async/await** (`QueryAsync`, `ExecuteAsync`)
- **QR Code Generator** (через `MemoryStream`)

Все запросы выполняются асинхронно.

---

## 📂 Таблицы (PostgreSQL)

Таблицы остаются прежними:
`Students`, `Courses`, `Groups`, `StudentGroups`, `Payments`, `Attendance`

---

## 📝 Задания

### 🔹 Task 1–5

(без изменений, см. предыдущие задания: студенты, оплаты, статистика, фильтрация, посещаемость).

---

### 🔹 Task 6. Генерация QR-кода профиля студента

**API:**

1. `GET /students/{id}/qrcode`

   - Находит студента в БД.
   - Генерирует QR-код в памяти (**MemoryStream**).
   - QR-код должен содержать **ссылку на профиль студента**.

     - Например:

       ```
       https://api.yourwebsite.tj/students/{id}
       ```

       или (если есть фронт):

       ```
       https://yourwebsite.tj/student/{id}
       ```

   - Возвращает QR-код как `FileResult` (`image/png`).
   - Ничего на диск не сохраняется.

---

## 🎯 Критерии оценки

- Все методы реализованы асинхронно (`async/await`).
- Использовать Dapper (`QueryAsync`, `ExecuteAsync`).
- SQL-запросы содержат `JOIN`, `GROUP BY`, фильтрацию.
- QR-код генерируется **только в памяти** и содержит **ссылку на профиль студента**.
- API протестировано через Swagger/Postman.
