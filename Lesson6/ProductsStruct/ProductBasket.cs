using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6.ProductsStruct
{
    public class ProductBasket
    {
        private List<IProduct> products = new List<IProduct>();

        public decimal? GetFullPrice()
        {
            return products.Aggregate(decimal.Zero, (sum, prod) => sum + prod.Price * Convert.ToDecimal(prod.Amount));
        }

        public string ListProducts()
        {
            if (products.Count == 0) return "There is no products" + Environment.NewLine;
            StringBuilder b = new StringBuilder();
            b.AppendJoin(Environment.NewLine, products).AppendLine();
            return b.ToString();
        }

        //Planned private but why not make it public
        public string ListProductsByCondition(Func<IProduct, bool> condition)
        {
            if (products.Count == 0) return "There is no products" + Environment.NewLine;
            StringBuilder b = new StringBuilder();
            foreach (IProduct product in products)
            { 
                if (condition.Invoke(product))
                {
                    b.AppendLine(product.ToString());
                }
            };
            return b.ToString();
        }
        public string ListProductsByType(IProduct.ProductType e)
        {
            return ListProductsByCondition(product => product.Type.Equals(e));
        }

        public string ListProductsByClass<T>()
        {
            return ListProductsByCondition(product => product is T);
        }
        
        public void Add(IProduct p)
        {
            IProduct? elem = products.Find(e => e.Name.Equals(p.Name));
            if (elem != null)
            {
                elem.Amount += p.Amount;
            }
            else
            {
                products.Add(p);
            }
        }
        public void Remove(string productName)
        {
            products.RemoveAt(products.FindIndex(p => p.Name == productName));
        }
        public void SetAmount(string productName, uint newAmount)
        {
            products.Where(p => p.Name == productName).First().Amount = newAmount;
        }
    }
}
