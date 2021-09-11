using System.Collections.Generic;
using System.Globalization;
using ShoppingBasketLibrary.Models.Voucher;

namespace ShoppingBasketLibrary.Models.Basket
{
    public interface IBasket
    {
        public CultureInfo CultureInfo { get; }
        public ICollection<IBasketItem> BasketItems { get; }
        public ICollection<IVoucher> Vouchers { get; }
        public decimal Subtotal { get; }
        public decimal Total { get; }
    }
}