#!/bin/bash

readonly API_PATH=./Viagens.API/lapr5-masterdata-viagens.csproj
readonly TESTS_PATH=./Viagens.Tests/Viagens.Tests.csproj

### Check for dir, if not found create it using the mkdir ##
[ ! -d "./Viagens.API/db" ] && mkdir ./Viagens.API/db

#Install SQLite driver
dotnet add $API_PATH package Microsoft.EntityFrameworkCore.Sqlite
dotnet add $API_PATH package Microsoft.EntityFrameworkCore.Design
dotnet add $TESTS_PATH package NUnit

dotnet tool install --global dotnet-ef
dotnet ef database update --project $API_PATH
