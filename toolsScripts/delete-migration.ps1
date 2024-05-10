Set-Location "$PSScriptRoot/../ApiApp.Data"

#install dotnet-ef to .Data project
#dotnet ef database update FullName_Change --context DatabaseContext --startup-project "../ApiApp.App"
dotnet ef migrations remove --context DatabaseContext --startup-project "../ApiApp"