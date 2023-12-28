namespace ItemShop.Models.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
    }
}
