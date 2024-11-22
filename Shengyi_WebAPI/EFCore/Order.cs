using System;
using System.Collections.Generic;

namespace Shengyi_WebAPI.EFCore;

public partial class Order
{
    public string OrderCode { get; set; } = null!;

    public int CustomerId { get; set; }

    public string Address { get; set; } = null!;

    public int StatusId { get; set; }

    public decimal TotalPrice { get; set; }

    public int ShippingCost { get; set; }

    public string Remark { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? DeleteTime { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual PaymentStatus Status { get; set; } = null!;
}
