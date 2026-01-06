# Backend API – Developer Setup Guide

This repository contains the **Backend API** for the Senior Design Project. The API is built using **ASP.NET Core (.NET 10)**, **Entity Framework Core**, and **PostgreSQL**, and is designed to run locally using **Docker** and **Docker Compose**.

This document outlines everything a developer needs to install, configure, and run the backend API locally.

---

## Tech Stack

- **Language:** C#
- **Framework:** ASP.NET Core (.NET 10)
- **ORM:** Entity Framework Core
- **Database:** PostgreSQL
- **Containerization:** Docker & Docker Compose
- **IDE:** JetBrains Rider or Visual Studio

---

## Prerequisites

Before running the project, make sure the following tools are installed on your machine.

---

### IDE

You must have **one** of the following installed:

- **JetBrains Rider** 
- **Visual Studio 2022 or 2026**

---

### .NET SDK

- **.NET SDK 10.0**

Verify installation:

```bash
dotnet --list-sdks
```

### Entity Framework Core
Install command:
```bash
dotnet tool install --global dotnet-ef
dotnet ef --version
```

### Docker/Docker Desktop
Can install Docker Desktop via the online download
Verify Installation:
```bash
docker --version
docker compose version
```

### Getting the project setup
- Clone the repo
- Cd into the project
- Run dotnet restore
- run docker compose up --build to build the docker image
    To stop the project you just run: docker compose down
- Then get the project setup in the database tool of your choice so that you can see the schema, the credentials are in the composer.yml file
