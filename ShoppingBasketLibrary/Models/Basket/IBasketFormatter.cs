namespace ShoppingBasketLibrary.Models.Basket
{
    public interface IBasketFormatter<T>
    {
        public T Format(IBasket basket);
    }
}