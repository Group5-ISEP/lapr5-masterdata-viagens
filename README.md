# Master Data Viagens

.NET Core 5 Rest API using MVC structure. Entity Framework and SQLite used for persistence.

## Run project

```
dotnet run
```

## Entity Framework and Migrations

Must run this the first time. 

```bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef database update
```

If any changes are made to the domain schema, use this: 
```
dotnet ef migrations add <whatever name to give>
```
