using System;
using System.Collections.Generic;

namespace quotes_project.Views.Home.Data.Entities;

public partial class UserEntity
{
    public int Id { get; set; }

    public string? User { get; set; }

    public string? Position { get; set; }
}