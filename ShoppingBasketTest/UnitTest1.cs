using System.Collections.Generic;
using System.Globalization;
using ShoppingBasketLibrary.Models.Basket;
using ShoppingBasketLibrary.Models.BasketFormatter;
using ShoppingBasketLibrary.Models.Product;
using ShoppingBasketLibrary.Models.Voucher;
using Xunit;

namespace ShoppingBasketTest
{
    public class UnitTest1
    {
        [Fact]
        public void Basket1()
        {
            var basket = new Basket("en-GB");
            basket.BasketItems.Add(new BasketItem(
                new Product("Jumper", "", ProductType.Uncategorised, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)54.65
                }),
                1)
            );
            basket.BasketItems.Add(new BasketItem(
                new Product("Head Light", "(Head Gear Category of Product)", ProductType.HeadGear, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)3.50
                }),
                1)
            );
            basket.BasketItems.Add(new BasketItem(
                new Product("£10 Gift Voucher", "", ProductType.GiftVoucher, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)10.00
                }),
                1)
            );
            var actual = new StringFormatter().Format(basket);
            var expected = @"1 Jumper @ £54.65
1 Head Light (Head Gear Category of Product) @ £3.50
1 £10 Gift Voucher @ £10.00
Sub Total: £68.15
No vouchers applied
Total: £68.15";
            Assert.Equal(expected, actual, true, true, true);
        }

        [Fact]
        public void Basket2()
        {
            var basket = new Basket("en-GB");
            basket.BasketItems.Add(new BasketItem(
                new Product("Gloves", "", ProductType.Uncategorised, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)10.50
                }),
                1)
            );
            basket.BasketItems.Add(new BasketItem(
                new Product("Jumper", "", ProductType.Uncategorised, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)54.65
                }),
                1)
            );
            basket.Vouchers.Add(new GiftVoucher("£5.00 Gift Voucher", "XXX-XXX", new CultureInfo("en-GB"), (decimal)5.00));
            var actual = new StringFormatter().Format(basket);
            var expected = @"1 Gloves @ £10.50
1 Jumper @ £54.65
Sub Total: £65.15
1 x £5.00 Gift Voucher XXX-XXX applied
Total: £60.15";
            Assert.Equal(expected, actual, true, true, true);
        }

        [Fact]
        public void Basket3()
        {
            var basket = new Basket("en-GB");
            basket.BasketItems.Add(new BasketItem(
                new Product("Gloves", "", ProductType.Uncategorised, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)25.00
                }),
                1)
            );
            basket.BasketItems.Add(new BasketItem(
                new Product("Jumper", "", ProductType.Uncategorised, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)26.00
                }),
                1)
            );
            basket.Vouchers.Add(new OfferVoucher("Offer Voucher", "YYY-YYY", new CultureInfo("en-GB"), (decimal)5.00, (decimal)50.00, new HashSet<ProductType> { ProductType.HeadGear }));
            var actual = new StringFormatter().Format(basket);
            var expected = @"1 Gloves @ £25.00
1 Jumper @ £26.00
Sub Total: £51.00
1 x £5.00 off Head Gear in baskets over £50.00 Offer Voucher YYY-YYY applied
Total: £51.00
Message: ""There are no products in your basket applicable to Offer Voucher YYY-YYY.""";
            Assert.Equal(expected, actual, true, true, true);
        }

        [Fact]
        public void Basket4()
        {
            var basket = new Basket("en-GB");
            basket.BasketItems.Add(new BasketItem(
                new Product("Gloves", "", ProductType.Uncategorised, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)25.00
                }),
                1)
            );
            basket.BasketItems.Add(new BasketItem(
                new Product("Jumper", "", ProductType.Uncategorised, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)26.00
                }),
                1)
            );
            basket.BasketItems.Add(new BasketItem(
                new Product("Head Light", "(Head Gear Category of Product)", ProductType.HeadGear, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)3.50
                }),
                1)
            );
            basket.BasketItems.Add(new BasketItem(
                new Product("£10 Gift Voucher", "", ProductType.GiftVoucher, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)10.00
                }),
                1)
            );
            basket.Vouchers.Add(new OfferVoucher("Offer Voucher", "YYY-YYY", new CultureInfo("en-GB"), (decimal)5.00, (decimal)50.00, new HashSet<ProductType> { ProductType.HeadGear }));
            var actual = new StringFormatter().Format(basket);
            var expected = @"1 Gloves @ £25.00
1 Jumper @ £26.00
1 Head Light (Head Gear Category of Product) @ £3.50
1 £10 Gift Voucher @ £10.00
Sub Total: £64.50
1 x £5.00 off Head Gear in baskets over £50.00 Offer Voucher YYY-YYY applied
Total: £61.00";
            Assert.Equal(expected, actual, true, true, true);
        }
        
        [Fact]
        public void Basket5()
        {
            var basket = new Basket("en-GB");
            basket.BasketItems.Add(new BasketItem(
                new Product("Gloves", "", ProductType.Uncategorised, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)25.00
                }),
                1)
            );
            basket.BasketItems.Add(new BasketItem(
                new Product("Jumper", "", ProductType.Uncategorised, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)26.00
                }),
                1)
            );
            basket.Vouchers.Add(new GiftVoucher("£5.00 Gift Voucher", "XXX-XXX", new CultureInfo("en-GB"), (decimal)5.00));
            basket.Vouchers.Add(new OfferVoucher("Offer Voucher", "YYY-YYY", new CultureInfo("en-GB"), (decimal)5.00, (decimal)50.00, null));
            var actual = new StringFormatter().Format(basket);
            var expected = @"1 Gloves @ £25.00
1 Jumper @ £26.00
Sub Total: £51.00
1 x £5.00 Gift Voucher XXX-XXX applied
1 x £5.00 off baskets over £50.00 Offer Voucher YYY-YYY applied
Total: £41.00";
            Assert.Equal(expected, actual, true, true, true);
        }
        
        [Fact]
        public void Basket6()
        {
            var basket = new Basket("en-GB");
            basket.BasketItems.Add(new BasketItem(
                new Product("Gloves", "", ProductType.Uncategorised, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)25.00
                }),
                1)
            );
            basket.BasketItems.Add(new BasketItem(
                new Product("£30 Gift Voucher", "", ProductType.GiftVoucher, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)30.00
                }),
                1)
            );
            basket.Vouchers.Add(new OfferVoucher("Offer Voucher", "YYY-YYY", new CultureInfo("en-GB"), (decimal)5.00, (decimal)50.00, null));
            var actual = new StringFormatter().Format(basket);
            var expected = @"1 Gloves @ £25.00
1 £30 Gift Voucher @ £30.00
Sub Total: £55.00
1 x £5.00 off baskets over £50.00 Offer Voucher YYY-YYY applied
Total: £55.00
Message: ""You have not reached the spend threshold for Offer Voucher YYY-YYY. Spend another £25.01 to receive £5.00 discount from your basket total.""";
            Assert.Equal(expected, actual, true, true, true);
        }
        
        [Fact]
        public void Basket7()
        {
            var basket = new Basket("en-GB");
            basket.BasketItems.Add(new BasketItem(
                new Product("Gloves", "", ProductType.Uncategorised, new Dictionary<CultureInfo, decimal>
                {
                    [new CultureInfo("en-GB")] = (decimal)25.00
                }),
                1)
            );
            basket.Vouchers.Add(new GiftVoucher("£30.00 Gift Voucher", "XXX-XXX", new CultureInfo("en-GB"), (decimal)30.00));

            var actual = new StringFormatter().Format(basket);
            var expected = @"1 Gloves @ £25.00
Sub Total: £25.00
1 x £30.00 Gift Voucher XXX-XXX applied
Total: £0.00";
            Assert.Equal(expected, actual, true, true, true);
        }
    }
}