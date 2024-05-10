Set-Location "$PSScriptRoot/../ApiApp.Data"

#install dotnet-ef to .Data project

$nameStart = Read-Host -Prompt 'Pass name of start migration'
$nameEnd = Read-Host -Prompt 'Pass name of end migration'
dotnet ef migrations script $nameStart $nameEnd --context DatabaseContext --startup-project "../ApiApp"