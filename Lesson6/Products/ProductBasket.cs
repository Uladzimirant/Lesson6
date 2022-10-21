using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6.Products
{
    public class ProductBasket : List<Product>
    {
        public decimal? GetFullPrice()
        {
            return this.Aggregate(decimal.Zero, (sum, prod) => sum + prod.Price * prod.Amount);
        }

        public string ListProducts()
        {
            StringBuilder b = new StringBuilder();
            this.ForEach((product) => b.AppendLine(product.ToString()));
            return b.ToString();
        }

        public string ListProducts(ProductType e)
        {
            StringBuilder b = new StringBuilder();
            this.ForEach((product) => {
                if 
                (
                    e == ProductType.FOOD && product is Food ||
                    e == ProductType.ELECTRICAL_APPLIANCE && product is ElectricalAppliance ||
                    e == ProductType.CHEMICAL && product is Chemical
                )
                {
                    b.AppendLine(product.ToString());
                }
            });
            return b.ToString();
        }

    }
}
