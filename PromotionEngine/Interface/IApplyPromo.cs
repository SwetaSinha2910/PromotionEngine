using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.Interface
{
    public interface IApplyPromo
    {
        int FixedPrice(Product prod);

        int ComboPrice(List<Product> products);

        int FinalPrice(List<Product> products);
    }
}
