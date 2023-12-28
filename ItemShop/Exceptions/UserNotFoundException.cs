namespace ItemShop.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("Item not found") { }
    }
}
