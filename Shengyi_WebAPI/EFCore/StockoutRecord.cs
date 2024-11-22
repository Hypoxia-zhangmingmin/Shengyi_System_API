using System;
using System.Collections.Generic;

namespace Shengyi_WebAPI.EFCore;

public partial class StockoutRecord
{
    public int Id { get; set; }

    public int CommodityId { get; set; }

    public int StaffId { get; set; }

    public int Count { get; set; }

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? DeleteTime { get; set; }

    public virtual Commodity Commodity { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
