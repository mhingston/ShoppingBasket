using System;
using System.Collections.Generic;
using System.Globalization;

namespace ShoppingBasketLibrary.Models.Product
{
    public class Product : IProduct
    {
        public Guid ProductId { get; }
        public string ProductName { get; }
        public string ProductDescription { get; }
        public ProductType ProductType { get; }
        public IDictionary<CultureInfo, decimal> RegionPrice { get; }

        public Product(string productName, string productDescription, ProductType productType, IDictionary<CultureInfo, decimal> regionPrice)
        {
            ProductId = Guid.NewGuid();
            ProductName = productName;
            ProductDescription = productDescription;
            ProductType = productType;
            RegionPrice = regionPrice;
        }
    }
}