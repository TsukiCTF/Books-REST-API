## Books REST API
***MVC REST API** for Book database. It supports **CRUD** operations (HTTP Get, Post, Put, Patch, Delete) on Book objects. Book objects contain their title, author, genre and publisher data.*

## Technologies/Techniques Used
- Written in C#
- ASP.NET Core 5.0 (Entity Framework Core ORM)
- Microsoft SQL Server as RDBMS
- MVC design pattern
- Migration to create database schema within code
- Repository (Dependency Injection) to handle DbContext
- DTOs (Data Transfer Objects) to control visibilities of model properties
- Postman & Swagger to test API endpoint

## Application Architecture
Below depicts the whole application architecture of this project (created with [draw.io][1])

![image](https://user-images.githubusercontent.com/32463233/130948341-3b89e36a-1f8b-4cb5-adc4-32a1fcb25f15.png)

## Install
1) Clone this repo to your Windows machine
2) Install .NET Core 5.0 SDK and SQL Server 2019
3) Add a new user in your SQL Server and restart server
4) Change ``User ID`` and ``Password`` fields in ``appsettings.json`` to your credentials
5) Change IP address in ``applicationURL`` field from ``Properties/launchSettings.json`` to localhost or your machine's IP
6) Run ``dotnet ef database update`` to migrate database
7) Run ``dotnet run`` and visit ``https://localhost/swagger/index.html`` on browser!

## Contributing
All contributions are welcome!

[1]: https://app.diagrams.net/
