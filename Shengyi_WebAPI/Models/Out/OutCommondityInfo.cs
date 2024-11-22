namespace Shengyi_WebAPI.Models.Out
{
    public class OutCommondityInfo
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CommodityName { get; set; }
        public string Specification { get; set; }

        public string Length { get; set; }
        public string Width { get; set; }
        public decimal Height { get; set; }
        public string CategoryName { get; set; }
        public string Unit { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }

        public decimal NowTonPrice { get; set; }
        public decimal Price { get; set;}

    }
}
