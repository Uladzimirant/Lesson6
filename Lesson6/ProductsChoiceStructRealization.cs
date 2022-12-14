using CMDMenu;
using Lesson6.ProductsStructRealization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6
{
    /* Same description as ProductList */
    internal class ProductsChoiceStructRealization
    {
        Dictionary<string, IProduct> _allowedProducts;

        public ProductsChoiceStructRealization(IEnumerable<IProduct> list)
        {
            _allowedProducts = list.ToDictionary(p => p.Name.ToLower());
        }

        public string ListAllowed()
        {
            StringBuilder b = new StringBuilder();
            b.AppendJoin(Environment.NewLine, _allowedProducts.Keys);
            return b.ToString();
        }
        public IProduct Create(string name, float amount)
        {
            if (_allowedProducts.TryGetValue(name.ToLower(), out IProduct? product))
            {

                if (product is Food) 
                {
                    var p = (Food)product;
                    return new Food(p, amount);
                }
                if (product is ElectricalAppliance)
                {
                    var p = (ElectricalAppliance)product;
                    return new ElectricalAppliance(p,
                        Math.Abs(amount - Math.Round(amount)) < 0.01 ?
                        Convert.ToUInt32(amount) :
                        throw new MessageException("Devices cannot be divided (their amount must be int)"));
                }
                if (product is Chemical)
                {
                    var p = (Chemical) product;
                    return new Chemical(p, amount);
                }
            }
            throw new MessageException("Choose allowed product");
        }
    }
}
