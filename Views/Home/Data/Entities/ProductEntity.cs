using System;
using System.Collections.Generic;

namespace quotes_project.Views.Home.Data.Entities;

public partial class ProductEntity
{
    public int IdProduct { get; set; }

    public string ProductName { get; set; } = null!;
}
