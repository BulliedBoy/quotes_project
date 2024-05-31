using System;
using System.Collections.Generic;

namespace quotes_project.Views.Home.Data.Entities;

public partial class CustomerEntity
{
    public int IdCustomer { get; set; }
    public string CustomerName { get; set; } = null!;
    public string CustomerType { get; set; } = null!;
    public string LicenceType { get; set; } = null!;
}
