namespace Shengyi_WebAPI.Models.In
{
    public class EditCommodityInfo
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CommodityName { get; set; }
        public string? Specification { get; set; }
        public string? Length { get; set; }
        public string? Width { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public int Count { get; set; }
        public decimal NowTonPrice { get; set; }
        public decimal SalePrice { get; set; }

    }
}
