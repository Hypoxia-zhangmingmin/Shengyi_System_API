namespace Shengyi_WebAPI.Models.In
{
    public class AddItemInfo
    {
        public int Id { get; set; }
        public int CommodityId { get; set; }
        public string CommodityName { get; set; }
        public string? Specification { get; set; }
        public string? Length { get; set; }

        public string? Width{ get; set; }
        public string? Height { get; set; }
        public string Unit { get; set; }
        public int Count { get; set; }
        public decimal SalePrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
