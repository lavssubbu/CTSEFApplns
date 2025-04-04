using System;
using System.Collections.Generic;

namespace CtsDBFirstEF.Models;

public partial class Customer
{
    public int Custid { get; set; }

    public string Custname { get; set; } = null!;

    public int? Custage { get; set; }

    public string? Custloc { get; set; }

   // public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
