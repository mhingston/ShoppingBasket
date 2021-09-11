using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ShoppingBasketLibrary.Models.Basket;
using ShoppingBasketLibrary.Models.Product;

namespace ShoppingBasketLibrary.Models.Voucher
{
    public class OfferVoucher : Voucher, IOfferVoucher
    {
        public decimal OfferThreshold { get; set; }
        public ISet<ProductType> ProductTypesIncluded { get; set; }

        /// <summary>
        /// Offer vouchers requires a threshold that needs to be exceeded before a discount can be applied e.g. £5.00 off of baskets over £50.
        /// Only a single offer voucher can be applied to a basket
        /// Can be applicable to only a subset of products.
        /// </summary>
        public OfferVoucher(string name, string productDescription, CultureInfo cultureInfo, decimal price, decimal offerThreshold, ISet<ProductType> productTypesIncluded)
        : base(name, productDescription, ProductType.OfferVoucher, new Dictionary<CultureInfo, decimal> { [cultureInfo] = price }, false)
        {
            OfferThreshold = offerThreshold;
            ProductTypesIncluded = productTypesIncluded;
        }

        public override decimal CalculateDiscount(IBasket basket, out string message)
        {
            message = "";
            
            // Get applicable products if product types are defined for this voucher
            var applicableProducts = basket.BasketItems.Where(c => ProductTypesIncluded == null || ProductTypesIncluded.Contains(c.Product.ProductType));
            
            // Get non applicable products (i.e. gift vouchers)
            var nonApplicableProducts = applicableProducts.Where(c => Equals(c.Product.ProductType, ProductType.GiftVoucher));
            var nonApplicableTotal = nonApplicableProducts.Sum(c => c.Product.RegionPrice[basket.CultureInfo] * c.Quantity);
            
            // Update the applicable products to exclude those just filtered out
            applicableProducts = applicableProducts.Where(c => !nonApplicableProducts.Contains(c));
            var applicableTotal = applicableProducts.Sum(c => c.Product.RegionPrice[basket.CultureInfo] * c.Quantity);

            // If the applicable total that the voucher can be redeemed against is greater than the value of the voucher
            // then the discount is equal to the value of the voucher
            // i.e. the voucher is used up even the discount applied is less than the value of the voucher
            // if applicable total is £3.50 and the voucher is for £5.00 then the discount is £3.50)
            if (applicableTotal >= RegionPrice[basket.CultureInfo])
            {
                applicableTotal = RegionPrice[basket.CultureInfo];
            }
            
            // If the basket subtotal less the value of non applicable products is greater than the voucher offer threshold
            // and multiple offer vouchers can be used (not currently the case)
            // or only one offer voucher can be used (currently the case) and this offer voucher is the first one applied to the basket
            if (basket.Subtotal - nonApplicableTotal > OfferThreshold
                && applicableProducts.Any()
                && (AllowMultiple || (!AllowMultiple && this == basket.Vouchers.FirstOrDefault(c => Equals(c.ProductType, ProductType.OfferVoucher)))))
            {
                return applicableTotal;
            }

            // If the basket subtotal less the value of non applicable products is less than ore equal to the offer threshold show a message
            if (basket.Subtotal - nonApplicableTotal <= OfferThreshold)
            {
                var difference = basket.Subtotal - nonApplicableTotal;
                difference += Helpers.GetLowestCurrencyUnit(basket.CultureInfo.NumberFormat);
                message = $"You have not reached the spend threshold for {ProductName} {ProductDescription}. Spend another {difference.ToString("c", basket.CultureInfo)} to receive {RegionPrice[basket.CultureInfo].ToString("c", basket.CultureInfo)} discount from your basket total.";
            }

            // Show the default message
            else
            {
                message = $"There are no products in your basket applicable to {ProductName} {ProductDescription}.";
            }

            return 0;
        }
    }
}