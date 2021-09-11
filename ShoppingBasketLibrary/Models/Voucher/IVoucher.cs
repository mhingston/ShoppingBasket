using ShoppingBasketLibrary.Models.Basket;
using ShoppingBasketLibrary.Models.Product;

namespace ShoppingBasketLibrary.Models.Voucher
{
    public interface IVoucher : IProduct
    {
        public bool AllowMultiple { get; set; }
        public decimal CalculateDiscount(IBasket basket, out string message);
    }
}