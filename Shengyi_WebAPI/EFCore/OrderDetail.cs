using System;
using System.Collections.Generic;

namespace Shengyi_WebAPI.EFCore;

public partial class OrderDetail
{
    public int Id { get; set; }

    public string OrderCode { get; set; } = null!;

    public int CommodityId { get; set; }

    public int Count { get; set; }

    public decimal PresalePrice { get; set; }

    public decimal ActualSellingPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? DeleteTime { get; set; }

    public virtual Commodity Commodity { get; set; } = null!;
}
