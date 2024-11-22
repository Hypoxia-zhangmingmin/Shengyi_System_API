using System;
using System.Collections.Generic;

namespace Shengyi_WebAPI.EFCore;

public partial class PaymentStatus
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? DeleteTime { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
