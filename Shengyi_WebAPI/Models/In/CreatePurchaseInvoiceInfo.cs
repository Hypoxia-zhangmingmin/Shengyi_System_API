namespace Shengyi_WebAPI.Models.In
{
    public class CreatePurchaseInvoiceInfo
    {
        public IFormFile File { get; set; }
        public int SupplierId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TaxRate { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public string? Remark { get; set; }
        public string Date { get; set; }
        public string Md5 { get; set; }
    }
}
