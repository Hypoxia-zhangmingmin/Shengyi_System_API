using System;
using System.Collections.Generic;

namespace Shengyi_WebAPI.EFCore;

public partial class Debt
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string OrderCode { get; set; } = null!;

    public decimal AmountReceivable { get; set; }

    public decimal AmountPaid { get; set; }

    public decimal AmountUnpaid { get; set; }

    public DateTime Deadline { get; set; }

    public DateTime LastPaidDate { get; set; }

    public string Remark { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? DeleteTime { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
