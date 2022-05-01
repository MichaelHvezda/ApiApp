using ApiApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Riganti.Utils.Infrastructure.Core;

namespace ApiApp.Data.Entities
{
    public partial class DatabaseContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
        }
    }


}
