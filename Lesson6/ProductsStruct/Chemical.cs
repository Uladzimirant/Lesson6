using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6.ProductsStruct
{
    public struct Chemical : IProduct
    {
        public enum DangerLevelType
        {
            SAFE,
            SLIGHT,
            DANGEROUS,
            EXTREME
        }
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
        public DangerLevelType DangerLevel { get; private set; }

        public Chemical(string name, decimal price, DangerLevelType dangerLevel, float amount = 0.0f)
        {
            Name = name;
            Price = price;
            DangerLevel = dangerLevel;
            if (amount < 0.0f) throw new ArgumentException("Amount must be posititve");
            _amount = amount;
            Type = IProduct.ProductType.CHEMICAL;
        }
        public Chemical(Chemical other) : this(other.Name, other.Price, other.DangerLevel, other.Amount) { }
        public Chemical(Chemical other, float amount) : this(other.Name, other.Price, other.DangerLevel, amount) { }

        public override string? ToString()
        {
            return Name + " - " + Amount.ToString() + " for " + Price.ToString() + " BYN" + $", level of danger: {DangerLevel}";
        }
    }
}
