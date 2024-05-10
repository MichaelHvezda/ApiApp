Set-Location "$PSScriptRoot/../ApiApp.Data"

#install dotnet-ef to .Data project

dotnet ef dbcontext scaffold "Name=DB" Microsoft.EntityFrameworkCore.SqlServer `
    --startup-project "../ApiApp" `
    --force `
    --no-build `
    --context DatabaseContext `
    --schema dbo `
    --data-annotations `
    --use-database-names `
    --output-dir Entities `
    --verbose