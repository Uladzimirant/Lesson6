using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6.ProductsStructRealization
{
    /* This task is terrible, doing it with structs is painful, they cannot inherit, why does I must do it like that */
    public interface IProduct
    {
        public enum ProductType
        {
            GENERAL,
            FOOD,
            ELECTRICAL_APPLIANCE,
            CHEMICAL
        }

        ProductType Type { get; }
        string Name { get; }
        decimal Price { get; }
        float Amount { get; set; }
    }
}
