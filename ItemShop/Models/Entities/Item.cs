namespace ItemShop.Models.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int? ShopId { get; set; }
        public Shop? Shop { get; set; }
    }
}
