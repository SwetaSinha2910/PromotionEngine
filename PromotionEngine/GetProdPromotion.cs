using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    public class GetProdPromotion
    {
        public PromoInfo GetPromoAB(string prodName)
        {
            PromoInfo info = new PromoInfo();
            if (prodName == "A")
            {               
                info.DiscountPrice = 130;
                info.DiscountQty = 3;
            }
            else if (prodName == "B")
            {
                info.DiscountPrice = 45;
                info.DiscountQty = 2;
            }
            return info;
        }       

        public PromoInfo GetPromoCD()
        {
            PromoInfo info = new PromoInfo();
            info.DiscountPrice = 30;
            info.DiscountQty = 1;
            return info;
        }

    }

    public class PromoInfo
    {
        public int DiscountPrice { get; set; }

        public int DiscountQty { get; set; }
    }
}
