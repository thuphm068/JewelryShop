﻿namespace JewelryShop.Helper
{
    public static class PriceFormatter
    {
        public static string FormatPrice(double price)
        {
            string formattedPrice = price.ToString("#,0").Replace(",", ".");
            return formattedPrice;
        }
    }

}
