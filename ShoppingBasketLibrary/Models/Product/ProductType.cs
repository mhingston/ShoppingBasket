namespace ShoppingBasketLibrary.Models.Product
{
    public class ProductType: ProductTypeEnum
    {
        public static readonly ProductType GiftVoucher = new(1, "Gift Voucher");
        public static readonly ProductType OfferVoucher = new(2, "Offer Voucher");
        public static readonly ProductType HeadGear = new(3, "Head Gear");
        public static readonly ProductType Uncategorised = new(4, "Uncategorised");

        private ProductType(int id, string name) : base(id, name)
        {
            
        }
    }
}