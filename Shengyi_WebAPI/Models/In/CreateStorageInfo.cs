namespace Shengyi_WebAPI.Models.In
{
    public class CreateStorageInfo
    {
        public int UnitId { get; set; }
        public int CommodityId { get; set; }
        public int SupplierId { get; set; }

        public int Count { get; set; }
        public decimal Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
