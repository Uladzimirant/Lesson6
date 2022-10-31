using Lesson6.ProductsClassRealization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6.ProductsStructRealization
{
    public class ProductBasket
    {
        private List<IProduct> products = new List<IProduct>();

        public decimal? GetFullPrice()
        {
            return products.Aggregate(decimal.Zero, (sum, prod) => sum + prod.Price * Convert.ToDecimal(prod.Amount));
        }

        public string PrintProducts()
        {
            if (products.Count == 0) return "There is no products" + Environment.NewLine;
            StringBuilder b = new StringBuilder();
            b.AppendJoin(Environment.NewLine, products).AppendLine();
            return b.ToString();
        }


        public List<IProduct> GetListOfProductsByCondition(Func<IProduct, bool> condition)
        {
            List<IProduct> list = new List<IProduct>();
            foreach (IProduct product in products)
            {
                if (condition.Invoke(product))
                {
                    list.Add(product);
                }
            };
            return list;
        }


        public List<T> GetListOfProductsByClass<T>()
        {
            return GetListOfProductsByCondition(e => e is T).Cast<T>().ToList();
        }


        //Planned private but why not make it public
        public string PrintProductsByCondition(Func<IProduct, bool> condition)
        {
            var list = GetListOfProductsByCondition(condition);
            if (list.Count == 0) return "There is no products by this condition" + Environment.NewLine;
            return string.Join(Environment.NewLine, GetListOfProductsByCondition(condition));
        }
        public string PrintProductsByType(IProduct.ProductType e)
        {
            return PrintProductsByCondition(product => product.Type.Equals(e));
        }

        public string PrintProductsByClass<T>()
        {
            return PrintProductsByCondition(product => product is T);
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
        public void Clear()
        {
            products.Clear();
        }
    }
}
