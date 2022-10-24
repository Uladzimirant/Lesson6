﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6.Products
{
    public class Chemical : Product
    {
        public enum DangerLevelType
        {
            SAFE,
            SLIGHT,
            DANGEROUS,
            EXTREME
        }
        public DangerLevelType DangerLevel { get; private set; }

        public Chemical(string name, decimal price, DangerLevelType dangerLevel, float amount = 0.0f) : base(name, price, amount)
        {
            DangerLevel = dangerLevel;
            Type = ProductType.CHEMICAL;
        }

        public override string? ToString()
        {
            return base.ToString() + $", level of danger: {DangerLevel}";
        }
    }
}
