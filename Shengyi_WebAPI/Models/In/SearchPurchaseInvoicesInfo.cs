namespace Shengyi_WebAPI.Models.In
{
    public class SearchPurchaseInvoicesInfo
    {
        public string supplier { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int CurrentPage { get; set; }
    }
}
