﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6.Products
{
    public class ElectricalAppliance : Product
    {
        public float Voltage { get; private set; }
        public float Amperage { get; private set; }
        public float Power { get; private set; }

        public ElectricalAppliance(string name, decimal price, float voltage, float amperage, float power, uint amount = 0) : base(name, price, amount)
        {
            Voltage = voltage;
            Amperage = amperage;
            Power = power;
            Type = ProductType.ELECTRICAL_APPLIANCE;
        }

        public override string? ToString()
        {
            return base.ToString() + $", {Voltage} V, {Amperage} A, {Power} W";
        }
    }
}
