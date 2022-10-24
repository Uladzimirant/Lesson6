using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6.Products
{
    public class Product
    {
        public enum ProductType
        {
            GENERAL,
            FOOD,
            ELECTRICAL_APPLIANCE,
            CHEMICAL
        }

        public string Name { get; private set; }
        public decimal Price { get; set; }

        private float _amount;
        public float Amount { 
            get { return _amount; }
            set {
                if (value < 0.0f) throw new ArgumentException("Amount must be posititve");
                _amount = value;
            } }
        public ProductType Type { get; protected set; }

        public Product(string name, decimal price, float amount = 0.0f)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price;
            Amount = amount;
            Type = ProductType.GENERAL;
        }

        public override string? ToString()
        {
            return Name + " - " + Amount.ToString() + " for " + Price.ToString() + " BYN";
        }
    }
}
