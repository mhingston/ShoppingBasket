using System;
using System.Globalization;

namespace ShoppingBasketLibrary.Models
{
    public static class Helpers
    {
        /// <summary>
        /// Add the lowest currency unit for a given instance of NumberFormatInfo
        /// </summary>
        public static decimal GetLowestCurrencyUnit(NumberFormatInfo numberFormatInfo)
        {
            if (numberFormatInfo.CurrencyDecimalDigits == 0)
            {
                return 1;
            }

            var inputString = "0.";

            for (var i = 0; i < numberFormatInfo.CurrencyDecimalDigits-1; i++)
            {
                inputString += "0";
            }

            inputString += "1";
            return Decimal.Parse(inputString);
        }
    }
}