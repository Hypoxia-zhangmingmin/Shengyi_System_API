using System;
using System.Collections.Generic;

namespace Shengyi_WebAPI.EFCore;

public partial class Credit
{
    public int Id { get; set; }

    public string CreditName { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? DeleteTime { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
