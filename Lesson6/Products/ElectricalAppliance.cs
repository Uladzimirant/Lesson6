using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6.Products
{
    public class ElectricalAppliance : Product
    {
        public uint Voltage { get; private set; }
        public uint Amperage { get; private set; }
        public uint Power { get; private set; }

        public ElectricalAppliance(string name, decimal price, uint amount, uint voltage, uint amperage, uint power) : base(name, price, amount)
        {
            Voltage = voltage;
            Amperage = amperage;
            Power = power;
        }

        public override string? ToString()
        {
            return base.ToString() + $", {Voltage} V, {Amperage} A, {Power} W";
        }
    }
}
