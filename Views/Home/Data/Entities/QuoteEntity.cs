using System;
using System.Collections.Generic;

namespace quotes_project.Views.Home.Data.Entities;

public partial class QuoteEntity
{
    public int IdQuote { get; set; }

    public int IdCustomer { get; set; }

    public string CustomerName { get; set; }//AQUI

    public int IdProduct { get; set; }

    public int IdUser { get; set; }

    public decimal Amount { get; set; }

    public DateOnly DDate { get; set; }
}
