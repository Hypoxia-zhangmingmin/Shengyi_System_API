namespace Shengyi_WebAPI.Models.In
{
    public class CreateSaleInvoiceInfo
    {
        //string localFilePath, string customerId, string totalPrice, string taxRate,string price,string tax,string date
        public IFormFile File { get; set; } 
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TaxRate { get; set; }
        public decimal  Price { get; set; }
        public decimal Tax { get; set; }
        public string? Remark { get; set; }
        public string Date { get; set; }
        public string Md5 { get; set; }
    }
}
