using System;
using System.Collections.Generic;
using System.Globalization;

namespace ShoppingBasketLibrary.Models.Product
{
    public interface IProduct
    {
        public Guid ProductId { get; }
        public string ProductName { get; }
        public string ProductDescription { get; }
        public ProductType ProductType { get; }
        public IDictionary<CultureInfo, decimal> RegionPrice { get; }
    }
}