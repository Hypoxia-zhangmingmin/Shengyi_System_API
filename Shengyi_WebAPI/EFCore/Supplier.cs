using System;
using System.Collections.Generic;

namespace Shengyi_WebAPI.EFCore;

public partial class Supplier
{
    public int Id { get; set; }

    public string SupplierName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? DeleteTime { get; set; }

    public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();

    public virtual ICollection<StorageRecord> StorageRecords { get; set; } = new List<StorageRecord>();
}
