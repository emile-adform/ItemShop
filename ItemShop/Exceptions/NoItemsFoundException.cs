namespace ItemShop.Exceptions
{
    public class NoItemsFoundException : Exception
    {
        public NoItemsFoundException() : base("No items found") { }
    }
}
