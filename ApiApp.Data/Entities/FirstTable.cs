using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ApiApp.Data.Entities;

[Table("FirstTable")]
public partial class FirstTable
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; }
}
