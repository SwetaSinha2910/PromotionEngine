using PromotionEngine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    public class ApplyPromo : IApplyPromo
    {
        GetProdPromotion prodpromo = new GetProdPromotion();
        PromoInfo info = new PromoInfo();

        public int FixedPrice(Product prod)
        {
            info = prodpromo.GetPromoAB(prod.ProdSKU);
            int remainder;
            int quotient = Math.DivRem(prod.Quantity, info.DiscountQty, out remainder);
            int final_price = quotient * info.DiscountPrice + remainder * prod.UnitPrice;
            return final_price;
        }

        public int ComboPrice(List<Product> products)
        {
            GetProdPromotion prodpromo = new GetProdPromotion();
            PromoInfo info = prodpromo.GetPromoCD();
            int final_price = 0;            
            
            var prod_c = (from item in products
                        where item.ProdSKU == "C"
                        select item).First();
            var prod_d = (from item in products
                         where item.ProdSKU == "D"
                         select item).First();

            if (string.IsNullOrEmpty(prod_c.ToString()))
            {
                final_price = (prod_d.Quantity) * (prod_d.UnitPrice);
            }
            else if (string.IsNullOrEmpty(prod_d.ToString()))
            {
                final_price = (prod_c.Quantity) * (prod_c.UnitPrice);
            }
            else
            {
                int qty_c = prod_c.Quantity;
                int qty_d = prod_d.Quantity;

                if (qty_c == qty_d)
                {
                    final_price = info.DiscountPrice * qty_c;
                }
                else if (qty_c > qty_d)
                {
                    int extra_price = prod_c.UnitPrice * (qty_c - qty_d);
                    final_price = info.DiscountPrice * qty_d + extra_price;
                }
                else if (qty_c < qty_d)
                {
                    int extra_price = prod_d.UnitPrice * (qty_d - qty_c);
                    final_price = info.DiscountPrice * qty_c + extra_price;
                }
            }
            return final_price;
        }

        public int FinalPrice(List<Product> products)
        {
            int total_price = 0;
            foreach (var prod in products)
            {                
                if (prod.ProdSKU == "A" || prod.ProdSKU == "B")
                {
                    int price1 = FixedPrice(prod);
                    total_price = total_price + price1;
                }
            }

            var IsExistsQSC = (from prod in products
                              select prod.ProdSKU).Contains("C");
            var IsExistsQSD = (from prod in products
                               select prod.ProdSKU).Contains("D");
            if (IsExistsQSC == true || IsExistsQSD == true)
            {
                int price2 = ComboPrice(products);
                total_price = total_price + price2;
            }
            return total_price;
        }
    }
}
