using System;
using System.Collections.Generic;
using System.Globalization;
using ShoppingBasketLibrary.Models.Basket;
using ShoppingBasketLibrary.Models.Product;

namespace ShoppingBasketLibrary.Models.Voucher
{
    public abstract class Voucher : IVoucher
    {
        public Guid ProductId { get; }
        public string ProductName { get; }
        public string ProductDescription { get; }
        public ProductType ProductType { get; }
        public IDictionary<CultureInfo, decimal> RegionPrice { get; }
        public bool AllowMultiple { get; set; }

        protected Voucher(string productName, string productDescription, ProductType productType, IDictionary<CultureInfo, decimal> regionPrice, bool allowMultiple)
        {
            ProductId = Guid.NewGuid();
            ProductName = productName;
            ProductDescription = productDescription;
            ProductType = productType;
            RegionPrice = regionPrice;
            AllowMultiple = allowMultiple;
        }

        public virtual decimal CalculateDiscount(IBasket basket, out string message)
        {
            throw new NotImplementedException();
        }
    }
}