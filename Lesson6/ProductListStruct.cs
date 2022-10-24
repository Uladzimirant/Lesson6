using CMDMenu;
using Lesson6.ProductsStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6
{
    /* Same description as ProductList */
    internal class ProductListStruct
    {
        Dictionary<string, IProduct> _allowedProducts;

        public ProductListStruct(IEnumerable<IProduct> list)
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
                    return new Food(p.Name, p.Price, p.ExpirationTime, amount);
                }
                if (product is ElectricalAppliance)
                {
                    var p = (ElectricalAppliance)product;
                    return new ElectricalAppliance(p.Name, p.Price, p.Voltage, p.Amperage, p.Power,
                        Math.Abs(amount - Math.Round(amount)) < 0.01 ?
                        Convert.ToUInt32(amount) :
                        throw new MessageException("Devices cannot be divided (their amount must be int)"));
                }
                if (product is Chemical)
                {
                    var p = (Chemical) product;
                    return new Chemical(p.Name, p.Price, p.DangerLevel, amount);
                }
            }
            throw new MessageException("Choose allowed product");
        }
    }
}
