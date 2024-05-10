Set-Location "$PSScriptRoot/../ApiApp.Data"

#install dotnet-ef to .Data project

$name = Read-Host -Prompt 'Pass name of migration'
dotnet ef migrations add $name --context DatabaseContext --startup-project "../ApiApp"