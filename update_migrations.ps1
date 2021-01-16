[CmdletBinding()]
param (
    [Parameter(Mandatory=$true)]
    [string]
    $name
)

$API_PATH="./Viagens.API/lapr5-masterdata-viagens.csproj"

dotnet ef migrations add $name --project $API_PATH
dotnet ef database update --project $API_PATH
