using System;
using System.Collections.Generic;

namespace Shengyi_WebAPI.EFCore;

public partial class BatchStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? DeleteTime { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
