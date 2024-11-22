using System;
using System.Collections.Generic;

namespace Shengyi_WebAPI.EFCore;

public partial class Sale
{
    public int Id { get; set; }

    public int CommodityId { get; set; }

    public decimal NowTonPrice { get; set; }

    public decimal SalePrice { get; set; }

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? DeleteTime { get; set; }

    public virtual Commodity Commodity { get; set; } = null!;
}
