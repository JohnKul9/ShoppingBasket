# Shopping Basket REST API (.NET 8)

## Task Description

Develop a **REST-based Web API** for an example **online shopping basket** with the following features:

- Add an item to the basket
- Add multiple items to the basket
- Remove an item from the basket
- Add multiple quantities of the same item
- Get the total cost including **20% VAT**
- Get the total cost excluding VAT
- Add a discounted item
- Add a discount code (**excluding discounted items**)
- Add shipping cost to the UK
- Add shipping cost to other countries

### Technical Requirements

- Written in **C#**
- Uses an **in-memory store** or database. If using a database then the ability to create/run the database should also be provided (e.g. SQL Script or Dockerfile)

  (this solution uses a **thread-safe in-memory store**)

---

## Solution Overview

This project is a **RESTful Web API** built with **.NET 8** using an **in-memory, thread-safe storage**.

No database or Docker setup is required.

---

## How to Set Up and Run

### Prerequisites

- **.NET 8 SDK**

Verify installation:

```bash
dotnet --version
```

Clone the repository:

```bash
git clone <repository-url>
cd ShoppingBasket.Api
```

Restore dependencies:

```bash
dotnet restore
```

Run the application:

```bash
dotnet run
```

The application will start and listen on HTTPS.
Example output:

```bash
Now listening on: https://localhost:7206
```

Swagger is enabled by default.
Open in browser:

```bash
https://localhost:7206/swagger
```

or:

```bash
https://localhost:7206/swagger/index.html
```
