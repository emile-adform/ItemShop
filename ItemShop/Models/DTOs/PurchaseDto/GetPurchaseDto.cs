namespace ItemShop.Models.DTOs.PurchaseDto
{
    public class GetPurchaseDto
    {
        public int UserId { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
    }
}
