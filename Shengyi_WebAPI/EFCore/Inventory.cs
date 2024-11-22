using System;
using System.Collections.Generic;

namespace Shengyi_WebAPI.EFCore;

public partial class Inventory
{
    public int Id { get; set; }

    public int CommodityId { get; set; }

    public int Count { get; set; }

    public int BatchStatusId { get; set; }

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? DeleteTime { get; set; }

    public virtual BatchStatus BatchStatus { get; set; } = null!;

    public virtual Commodity Commodity { get; set; } = null!;
}
