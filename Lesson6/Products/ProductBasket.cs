using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6.Products
{
    public class ProductBasket
    {
        private class ProductNameComparer : EqualityComparer<Product>
        {
            public override bool Equals(Product? x, Product? y)
            {
                return EqualityComparer<string>.Default.Equals(x?.Name, y?.Name);
            }

            public override int GetHashCode([DisallowNull] Product obj)
            {
                return obj.Name.GetHashCode();
            }
        }

        private HashSet<Product> products = new HashSet<Product>(new ProductNameComparer());

        public ProductBasket(){ }
        public ProductBasket(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                Add(product);
            }
        }

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
        public string ListProductsByCondition(Func<Product, bool> condition)
        {
            if (products.Count == 0) return "There is no products" + Environment.NewLine;
            StringBuilder b = new StringBuilder();
            foreach (Product product in products)
            { 
                if (condition.Invoke(product))
                {
                    b.AppendLine(product.ToString());
                }
            };
            return b.ToString();
        }
        public string ListProductsByType(Product.ProductType e)
        {
            return ListProductsByCondition(product => product.Type.Equals(e));
        }

        public string ListProductsByClass<T>()
        {
            return ListProductsByCondition(product => product is T);
        }
        
        public void Add(Product p)
        {
            if (products.TryGetValue(p, out Product? existingProduct))
            {
                existingProduct.Amount += p.Amount;
            } 
            else
            {
                products.Add(p);
            }
        }
        public void Remove(string productName)
        {
            products.RemoveWhere(p => p.Name == productName);
        }
        public void Remove(Product p)
        {
            Remove(p.Name);
        }
        public void Clear()
        {
            products.Clear();
        }
        public void SetAmount(string productName, uint newAmount)
        {
            products.Where(p => p.Name == productName).First().Amount = newAmount;
        }
    }
}
