using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6.ProductsStruct
{
    public struct ElectricalAppliance : IProduct
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
        public float Voltage { get; private set; }
        public float Amperage { get; private set; }
        public float Power { get; private set; }

        public ElectricalAppliance(string name, decimal price, float voltage, float amperage, float power, uint amount = 0)
        {
            Name = name;
            Price = price;
            if (amount < 0.0f) throw new ArgumentException("Amount must be posititve");
            _amount = amount;
            Voltage = voltage;
            Amperage = amperage;
            Power = power;
            Type = IProduct.ProductType.ELECTRICAL_APPLIANCE;
        }

        public override string? ToString()
        {
            return Name + " - " + Amount.ToString() + " for " + Price.ToString() + " BYN" + $", {Voltage} V, {Amperage} A, {Power} W";
        }
    }
}
