using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ShoppingBasketLibrary.Models.Voucher;

namespace ShoppingBasketLibrary.Models.Basket
{
    public class Basket : IBasket
    {
        public CultureInfo CultureInfo { get; set; }
        public ICollection<IBasketItem> BasketItems { get; set; }
        public ICollection<IVoucher> Vouchers { get; set; }

        public Basket(string cultureName)
        {
            CultureInfo = new CultureInfo(cultureName);
            BasketItems = new List<IBasketItem>();
            Vouchers = new List<IVoucher>();
        }

        public decimal Subtotal
        {
            get
            {
                return BasketItems.Sum(c => c.Product.RegionPrice[CultureInfo] * c.Quantity);
            }
        }

        public decimal Total
        {
            get
            {
                var total = Subtotal - Vouchers.Sum(c => c.CalculateDiscount(this, out _));
                return total;
            }
        }
    }
}