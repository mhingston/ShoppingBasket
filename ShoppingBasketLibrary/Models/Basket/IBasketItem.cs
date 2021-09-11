using ShoppingBasketLibrary.Models.Product;

namespace ShoppingBasketLibrary.Models.Basket
{
    public interface IBasketItem
    {
        public IProduct Product { get; }
        public int Quantity { get; }
    }
}