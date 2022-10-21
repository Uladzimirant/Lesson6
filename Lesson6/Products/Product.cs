using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6.Products
{
    public class Product
    {
        public string Name { get; private set; }
        public decimal Price { get; set; }
        public uint Amount { get; set; }

        public Product(string name, decimal price, uint amount)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price;
            Amount = amount;
        }

        public override string? ToString()
        {
            return Name + " - " + Amount.ToString() + " for " + Price.ToString() + " ₽";
        }
    }
}
