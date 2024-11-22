using System;
using System.Collections.Generic;

namespace Shengyi_WebAPI.EFCore;

public partial class Category
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? DeleteTime { get; set; }

    public virtual ICollection<Commodity> Commodities { get; set; } = new List<Commodity>();
}
