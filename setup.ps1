$API_PATH="./lapr5-masterdata-viagens.csproj"

### Check for dir, if not found create it using the mkdir ##
$path = "./db"
If(!(Test-Path $path))
{
      New-Item -ItemType Directory -Force -Path $path
}

#Install SQLite driver
dotnet add $API_PATH package Microsoft.EntityFrameworkCore.Sqlite -v 5.0.1
dotnet add $API_PATH package Microsoft.EntityFrameworkCore.SqlServer -v 5.0.1
dotnet add $API_PATH package Microsoft.EntityFrameworkCore.Design -v 5.0.1
dotnet add $API_PATH package NUnit -v 3.12.0

dotnet tool install --global dotnet-ef
dotnet ef database update --project $API_PATH
