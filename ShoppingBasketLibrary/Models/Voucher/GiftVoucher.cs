using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ShoppingBasketLibrary.Models.Basket;
using ShoppingBasketLibrary.Models.Product;

namespace ShoppingBasketLibrary.Models.Voucher
{
    /// <summary>
    /// Gift Vouchers can be redeemed against the value of a basket.
    /// Multiple gift vouchers can be applied to a basket.
    /// Gift vouchers can only be redeemed against non gift voucher products.
    /// The purchase of a gift voucher does not contribute to the discountable basket total.
    /// </summary>
    public class GiftVoucher : Voucher, IGiftVoucher
    {
        public ISet<ProductType> ProductTypesExcluded { get; set; }

        public GiftVoucher(string name, string productDescription, CultureInfo cultureInfo, decimal price)
        : base(name, productDescription, ProductType.GiftVoucher, new Dictionary<CultureInfo, decimal> { [cultureInfo] = price }, true)
        {
            ProductTypesExcluded = new HashSet<ProductType>()
            {
                ProductType.GiftVoucher
            };
        }

        public override decimal CalculateDiscount(IBasket basket, out string message)
        {
            message = "";
            
            // Get non applicable products (i.e. gift vouchers)
            var nonApplicableProducts = basket.BasketItems.Where(c => ProductTypesExcluded != null && ProductTypesExcluded.Contains(c.Product.ProductType));
            var nonApplicableTotal = nonApplicableProducts.Sum(c => c.Product.RegionPrice[basket.CultureInfo] * c.Quantity);
            
            // Update the applicable products to exclude those just filtered out
            var applicableProducts = basket.BasketItems.Where(c => !nonApplicableProducts.Contains(c));
            var applicableTotal = applicableProducts.Sum(c => c.Product.RegionPrice[basket.CultureInfo] * c.Quantity);
            
            // If the applicable total that the voucher can be redeemed against is greater than the value of the voucher
            // then the discount is equal to the value of the voucher
            // otherwise the discount is equal to the applicable total.
            // i.e. the voucher is used up even the discount applied is less than the value of the voucher
            // if applicable total is £3.50 and the voucher is for £5.00 then the discount is £3.50)
            if (applicableTotal >= RegionPrice[basket.CultureInfo])
            {
                applicableTotal = RegionPrice[basket.CultureInfo];
            }

            // The discount only applies if we have applicable products in the basket
            // and multiple gift vouchers can be used (currently the case)
            // or only one gift voucher can be used (currently the case) and this gift voucher is the first one applied to the basket
            if (applicableProducts.Any()
                && (AllowMultiple || (!AllowMultiple && this == basket.Vouchers.FirstOrDefault(c => Equals(c.ProductType, ProductType.GiftVoucher)))))
            {
                return applicableTotal;
            }
            
            message = $"There are no products in your basket applicable to {ProductName} {ProductDescription}.";
            return 0;
        }
    }
}