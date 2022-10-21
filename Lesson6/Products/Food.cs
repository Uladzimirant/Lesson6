using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6.Products
{
    public class Food : Product
    {
        public uint ExpirationTime { get; private set; }
        public Food(string name, decimal price, uint amount, uint expirationTime) : base(name, price, amount)
        {
            ExpirationTime = expirationTime;
        }

        public override string? ToString()
        {
            return base.ToString() + $", {ExpirationTime} day{(ExpirationTime > 1 ? "s" : "")}";
        }
    }
}
