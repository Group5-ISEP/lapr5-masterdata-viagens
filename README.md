# Master Data Viagens

.NET Core 5 Rest API using MVC structure. Entity Framework and SQLite used for persistence.

## Setup

**Install .NET Core 5 and make sure .NET CLI is on the PATH using**
```
dotnet --version
```

The setup script at the root folder installs dependecies using .NET CLI commands.

```
#BASH
./setup.sh

or

#POWERSHELL
./setup.ps
```

## Run project

```
dotnet run
```

## Test project

Runs **NUnit** project, which runs tests to the API project at `./Viagens.Tests/`

```
dotnet test
```

## Entity Framework and Migrations

If any changes are made to the domain schema, use this: 
```
#BASH
./update_migrations.sh <whatever name>

or

#POWERSHELL
./update_migrations.ps <whatever name>
```


## Build

```
dotnet build
```
