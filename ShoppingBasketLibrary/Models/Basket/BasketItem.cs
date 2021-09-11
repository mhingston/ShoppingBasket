using ShoppingBasketLibrary.Models.Product;

namespace ShoppingBasketLibrary.Models.Basket
{
    public class BasketItem : IBasketItem
    {
        public IProduct Product { get; set; }
        public int Quantity { get; set; }

        public BasketItem(IProduct product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}