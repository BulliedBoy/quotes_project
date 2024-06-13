using System;
using System.Collections.Generic;

namespace quotes_project.Views.Home.Data.Entities;

public partial class LocalProductEntity
{
    public int IdProduct { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal AmountNormal { get; set; }
    public decimal AmountOutsourcing { get; set; }
}
