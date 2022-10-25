using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMDMenu;
using Lesson6.Products;

namespace Lesson6
{
    /* This class list avaiable products that you can put in the basket,
     * it stores information about products, for example name, price and expiration date for food
     */
    internal class ProductList
    {
        Dictionary<string, Product> _allowedProducts;

        public ProductList(IEnumerable<Product> list)
        {
            _allowedProducts = list.ToDictionary(p => p.Name.ToLower());
        }

        public string ListAllowed()
        {
            StringBuilder b = new StringBuilder();
            b.AppendJoin(Environment.NewLine, _allowedProducts.Keys);
            return b.ToString();
        }
        public Product Create(string name, float amount)
        {
            if (_allowedProducts.TryGetValue(name.ToLower(), out Product? product))
            {
                {
                    var p = product as Food;
                    if (p != null) return new Food(p, amount);
                }
                {
                    var p = product as ElectricalAppliance;
                    if (p != null) return new ElectricalAppliance(p,
                        Math.Abs(amount - Math.Round(amount)) < 0.01 ? 
                        Convert.ToUInt32(amount) : 
                        throw new MessageException("Devices cannot be divided (their amount must be int)"));
                }
                {
                    var p = product as Chemical;
                    if (p != null) return new Chemical(p, amount);
                }
            }
            throw new MessageException("Choose allowed product");
        }
    }
}
