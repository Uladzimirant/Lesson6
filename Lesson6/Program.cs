using CMDMenu;
using Lesson6.Products;

namespace Lesson6
{
    public class Program
    {
        static CMDHandler handler = new CMDHandler();
        static ProductBasket products = new ProductBasket();
        public static void Main(string[] args)
        {
            products.Add(new Food("Apple", 2.57m, 4, 5));
            products.Add(new Food("Milk", 2.05m, 4, 3));
            products.Add(new Food("Meat", 8.2m, 1, 2));
            products.Add(new ElectricalAppliance("Teapot", 60m, 1, 5, 220, 1880));
            products.Add(new ElectricalAppliance("Phone Charger", 20.5m, 1, 220, 2, 25));
            products.Add(new Chemical("Distilled Water", 4m, 2, Chemical.DangerLevelType.SAFE));
            products.Add(new Chemical("Pipe cleaner", 10m, 1, Chemical.DangerLevelType.DANGEROUS));
            products.Add(new Chemical("Acid", 80m, 1, Chemical.DangerLevelType.EXTREME));


            handler.RegisterCommand("show sum", () =>
            {
                Console.WriteLine($"Current price of product basket is {products.GetFullPrice()}");
            }, "Shows current price of busket");

            handler.RegisterCommand($"list", () =>
            {
                Console.Write(products.ListProducts());
            }, "Lists all products");

            foreach (ProductType ptype in Enum.GetValues(typeof(ProductType)))
            {
                string typeName = ptype.ToString().ToLower();
                handler.RegisterCommand($"list {typeName}", () =>
                {
                    Console.Write(products.ListProducts(ptype));
                }, $"Lists products of type {typeName}");
            }

            handler.PrintHelp();
            handler.Run();
        }
    }
}