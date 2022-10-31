using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6.ProductsStructRealization
{
    public struct Food : IProduct
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        private float _amount;
        public float Amount
        {
            get { return _amount; }
            set
            {
                if (value < 0.0f) throw new ArgumentException("Amount must be posititve");
                _amount = value;
            }
        }
        public IProduct.ProductType Type { get; private set; }
        public uint ExpirationTime { get; private set; }
        public Food(string name, decimal price, uint expirationTime, float amount = 0.0f)
        {
            Name = name;
            Price = price;
            if (amount < 0.0f) throw new ArgumentException("Amount must be posititve");
            _amount = amount;
            ExpirationTime = expirationTime;
            Type = IProduct.ProductType.FOOD;
        }
        public Food(Food other) : this(other.Name, other.Price, other.ExpirationTime, other.Amount) { }
        public Food(Food other, float amount) : this(other.Name, other.Price, other.ExpirationTime, amount) { }
        public override string? ToString()
        {
            return Name + " - " + Amount.ToString() + " for " + Price.ToString() + " BYN" + $", {ExpirationTime} day{(ExpirationTime > 1 ? "s" : "")}";
        }
    }
}
