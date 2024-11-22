namespace Shengyi_WebAPI.Models.In
{
    public class CreateOrderInfo
    {
        public int CustomerId { get; set; }
        public int PaymentId { get; set; }
        public string Address { get; set; }

        public int ShippingCost { get; set; }
        public decimal Total { get; set; }
        public string Remark { get; set; }
        public List<AddItemInfo> Items { get; set; }
    }
}
