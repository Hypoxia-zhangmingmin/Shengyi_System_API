using System;
using System.Collections.Generic;

namespace Shengyi_WebAPI.EFCore;

public partial class PurchaseInvoice
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int SupplierId { get; set; }

    public decimal Amount { get; set; }

    public decimal TaxRate { get; set; }

    public decimal Tax { get; set; }

    public decimal TotalAmount { get; set; }

    public string FileName { get; set; } = null!;

    public string? Remark { get; set; }

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? DeleteTime { get; set; }

    public virtual Supplier Supplier { get; set; } = null!;
}
