using System;
using System.Linq;
using System.Text;
using ShoppingBasketLibrary.Models.Basket;
using ShoppingBasketLibrary.Models.Voucher;

namespace ShoppingBasketLibrary.Models.BasketFormatter
{
    public class StringFormatter : IBasketFormatter<string>
    {
        public string Format(IBasket basket)
        {
            var lines = new StringBuilder();
            var message = "";

            foreach (var voucher in basket.Vouchers)
            {
                voucher.CalculateDiscount(basket, out message);
            }
            
            foreach (var item in basket.BasketItems)
            {
                var productDescription = item.Product.ProductName;

                if (item.Product.ProductDescription != null)
                {
                    productDescription += $" {item.Product.ProductDescription}";
                }
                
                lines.AppendLine($"{item.Quantity} {productDescription} @ {item.Product.RegionPrice[basket.CultureInfo].ToString("c", basket.CultureInfo)}");
            }
            
            lines.AppendLine($"Sub Total: {basket.Subtotal.ToString("c", basket.CultureInfo)}");

            foreach (var grouping in basket.Vouchers.GroupBy(c => c.ProductId))
            {
                var voucher = grouping.First();

                if (voucher.GetType() == typeof(OfferVoucher))
                {
                    var offerVoucher = (OfferVoucher)voucher;
                    var applicableProductsString = "baskets";

                    if (offerVoucher.ProductTypesIncluded != null)
                    {
                        var productTypes = String.Join(", ", offerVoucher.ProductTypesIncluded.Select(c => c.ToString()));
                        applicableProductsString = $"{productTypes} in baskets";
                    }
                    
                    lines.AppendLine($"{grouping.Count()} x {offerVoucher.RegionPrice[basket.CultureInfo].ToString("c", basket.CultureInfo)} off {applicableProductsString} over {offerVoucher.OfferThreshold.ToString("c", basket.CultureInfo)} {offerVoucher.ProductName} {offerVoucher.ProductDescription} applied");
                }

                else
                {
                    lines.AppendLine($"{grouping.Count()} x {voucher.ProductName} {voucher.ProductDescription} applied");
                }
            }

            if (!basket.Vouchers.Any())
            {
                lines.AppendLine("No vouchers applied");
            }
            
            lines.AppendLine($"Total: {basket.Total.ToString("c", basket.CultureInfo)}");

            if (!String.IsNullOrEmpty(message))
            {
                lines.AppendLine(@$"Message: ""{message}""");
            }

            return lines.ToString().TrimEnd();
        }
    }
}