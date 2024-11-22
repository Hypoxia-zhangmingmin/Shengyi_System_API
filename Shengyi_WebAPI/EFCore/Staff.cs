using System;
using System.Collections.Generic;

namespace Shengyi_WebAPI.EFCore;

public partial class Staff
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? DeleteTime { get; set; }

    public virtual ICollection<StockoutRecord> StockoutRecords { get; set; } = new List<StockoutRecord>();

    public virtual ICollection<StorageRecord> StorageRecords { get; set; } = new List<StorageRecord>();
}
