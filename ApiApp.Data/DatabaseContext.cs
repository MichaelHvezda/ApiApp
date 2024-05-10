using ApiApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Riganti.Utils.Infrastructure.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.SqlServer.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text;

namespace ApiApp.Data.Entities
{
    public partial class DatabaseContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DB");
            }
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ReplaceService<IHistoryRepository, HistoryRepository>();
        }
    }
    public class HistoryRepository : SqlServerHistoryRepository
    {
        public HistoryRepository(HistoryRepositoryDependencies dependencies)
            : base(dependencies)
        {
        }

        //private readonly string AppVersion = ThisAssembly.Git.BaseVersion.Major + "." + ThisAssembly.Git.BaseVersion.Minor + "." + ThisAssembly.Git.BaseVersion.Patch;
        private readonly DateTime AddDate = DateTime.UtcNow;

        protected override void ConfigureTable(EntityTypeBuilder<HistoryRow> history)
        {
            base.ConfigureTable(history);
            history.Property<DateTime>(nameof(AddDate));
        }
        public override string GetInsertScript(HistoryRow row)
        {
            var stringTypeMapping = Dependencies.TypeMappingSource.GetMapping(typeof(string));
            return new StringBuilder().Append("INSERT INTO ")
                .Append(SqlGenerationHelper.DelimitIdentifier(TableName, TableSchema))
                .Append(" (")
                .Append(SqlGenerationHelper.DelimitIdentifier(MigrationIdColumnName))
                .Append(", ")
                .Append(SqlGenerationHelper.DelimitIdentifier(ProductVersionColumnName))
                .Append(", ")
                .Append(SqlGenerationHelper.DelimitIdentifier(nameof(AddDate)))
                .Append(" )")
                .Append("VALUES (")
                .Append(stringTypeMapping.GenerateSqlLiteral(row.MigrationId))
                .Append(", ")
                .Append(stringTypeMapping.GenerateSqlLiteral(row.ProductVersion))
                .Append(", ")
                .Append(stringTypeMapping.GenerateSqlLiteral(AddDate.ToString("yyyy-MM-dd HH:mm:ss.fffffff")))
                .Append(" )")
                .AppendLine(SqlGenerationHelper.StatementTerminator)
                .ToString();
        }
    }

}
