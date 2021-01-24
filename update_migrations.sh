#!/bin/bash

readonly API_PATH=./lapr5-masterdata-viagens.csproj

if [ $# -eq 0 ]
  then
    echo "Migration name is missing"
    exit 1
fi

dotnet ef migrations add $1 --project $API_PATH
dotnet ef database update --project $API_PATH
