using System;
using System.Collections.Generic;

namespace Shengyi_WebAPI.EFCore;

public partial class Commodity
{
    public int Id { get; set; }

    public string CommodityName { get; set; } = null!;

    public string? Specification { get; set; }

    public int? Width { get; set; }

    public int? Length { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public int CategoryId { get; set; }

    public string? Description { get; set; }

    public string Unit { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public int Count { get; set; }

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? DeleteTime { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<PriceHistory> PriceHistories { get; set; } = new List<PriceHistory>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<StockoutRecord> StockoutRecords { get; set; } = new List<StockoutRecord>();

    public virtual ICollection<StorageRecord> StorageRecords { get; set; } = new List<StorageRecord>();
}
