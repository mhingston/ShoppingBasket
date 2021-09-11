using System.Collections.Generic;
using ShoppingBasketLibrary.Models.Product;

namespace ShoppingBasketLibrary.Models.Voucher
{
    public interface IOfferVoucher : IVoucher
    {
        public decimal OfferThreshold { get; set; }
        public ISet<ProductType> ProductTypesIncluded { get; set; }
    }
}