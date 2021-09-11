using System.Collections.Generic;
using ShoppingBasketLibrary.Models.Product;

namespace ShoppingBasketLibrary.Models.Voucher
{
    public interface IGiftVoucher
    {
        public ISet<ProductType> ProductTypesExcluded { get; set; }
    }
}