using CMDMenu;
using Lesson6.ProductsClassRealization;
using System.Linq;
using static Lesson6.ProductsClassRealization.Product;

namespace Lesson6
{
    /* Attention! 
     * The code maybe a bit weird and duplicated.
     * I didn't realized the implications of using structs instead of classes
     * so I didn't have time to make it look normal.
     * Also I didn't want to use a lots of advanced materials like Generics and etc. (Ignore LINQ here)
     */
    public class Program
    {
        static CMDHandler handler = new CMDHandler();
        static ProductBasket products = new ProductBasket();
        static ProductsStructRealization.ProductBasket productsStruct = new ProductsStructRealization.ProductBasket();
        static bool useStruct = false;


        static (string name, decimal price, uint days)[] foodArray = new (string name, decimal price, uint days)[]
        {
            ("Apple", 1.57m, 3),
            ("Banana", 3.98m, 5),
            ("Bread", 1.08m, 5),
            ("Meat", 12.8m, 2),
            ("Milk", 2.05m, 4)
        };


        static (string name, decimal price, float voltage, float amperage, float power)[] ellectricalArray =
            new (string name, decimal price, float voltage, float amperage, float power)[]
        {
            ("Teapot", 59m, 220, 5, 1880),
            ("Microwave", 499m, 220, 5, 600),
            ("Charger", 20.5m, 220, 2, 25),
            ("Headphones", 18.9m, 15, 0.5f, 8),
        };


        static (string name, decimal price, string dangerLevel)[] chemicalArray = new
            (string name, decimal price, string dangerLevel)[]
        {
            ("Distilled Water", 4m, "SAFE"),
            ("PipeCleaner", 10m, "DANGEROUS"),
            ("Detergent", 19.9m, "SLIGHT"),
            ("Acid", 80m, "EXTREME")
        };


        static ProductsChoiceClassRealization productList = new ProductsChoiceClassRealization(
            foodArray.Select(e => new Food(e.name, e.price, e.days) as Product).
            Concat(ellectricalArray.Select(e => new ElectricalAppliance(e.name, e.price,e.voltage,e.amperage,e.power))).
            Concat(chemicalArray.Select(e => new Chemical(e.name,e.price,Enum.Parse<Chemical.DangerLevelType>(e.dangerLevel))))
            );


        static ProductsChoiceStructRealization productListStruct = new ProductsChoiceStructRealization(
            foodArray.Select(e => new ProductsStructRealization.Food(e.name, e.price, e.days) as ProductsStructRealization.IProduct).
            Concat(ellectricalArray.Select(
                e => new ProductsStructRealization.ElectricalAppliance(e.name, e.price, e.voltage, e.amperage, e.power)
                as ProductsStructRealization.IProduct)).
            Concat(chemicalArray.Select(
                e => new ProductsStructRealization.Chemical(e.name, e.price, Enum.Parse<ProductsStructRealization.Chemical.DangerLevelType>(e.dangerLevel))
                as ProductsStructRealization.IProduct))
            );


        static void FillDefault()
        {
            if (!useStruct)
            {
                products.Add(productList.Create("Banana", 0.4f));
                products.Add(productList.Create("Meat", 0.35f));
                products.Add(productList.Create("Bread", 1));
                products.Add(productList.Create("Milk", 2));
                products.Add(productList.Create("Charger", 1));
                products.Add(productList.Create("Headphones", 1));
                products.Add(productList.Create("Detergent", 1.5f));
            } else
            {
                productsStruct.Add(productListStruct.Create("Banana", 0.4f));
                productsStruct.Add(productListStruct.Create("Meat", 0.35f));
                productsStruct.Add(productListStruct.Create("Bread", 1));
                productsStruct.Add(productListStruct.Create("Milk", 2));
                productsStruct.Add(productListStruct.Create("Charger", 1));
                productsStruct.Add(productListStruct.Create("Headphones", 1));
                productsStruct.Add(productListStruct.Create("Detergent", 1.5f));
            }
        }


        public static void AskAdd()
        {
            string[] input = handler.AskForInput("Enter product name to add and amount").Split();
            if (input.Length < 2)
            {
                string[] fullerInput = handler.AskForInput("Enter amount").Split();
                input = new string[2] { input[0], fullerInput[0] };
            }
            try
            {
                if (!useStruct) products.Add(productList.Create(input[0], Convert.ToSingle(input[1])));
                else productsStruct.Add(productListStruct.Create(input[0], Convert.ToSingle(input[1])));
            }
            catch (FormatException e)
            {
                throw new MessageException("Enter correct values, couldn't convert: " + e.Message);
            }
        }


        public static void Main(string[] args)
        {
            handler.RegisterCommand("add", AskAdd, "Adds one of allowed products to basket");
            handler.RegisterCommand("show allowed", () => Console.WriteLine(!useStruct ? 
                productList.ListAllowed() : productListStruct.ListAllowed()), "Shows allowed products");
            handler.RegisterCommand("fill default", FillDefault, "Adds default products to basket");


            handler.RegisterCommand("show sum", () =>
            {
                var res = !useStruct ?
                    products.GetFullPrice() : productsStruct.GetFullPrice();
                Console.WriteLine($"Current price of product basket is {res} BYN");
            }, "Shows current price of busket");


            handler.RegisterCommand($"list", () =>
            {
                Console.Write(!useStruct ?
                    products.PrintProducts() :
                    productsStruct.PrintProducts());
            }, "Lists all products");


            foreach (Product.ProductType ptype in Enum.GetValues(typeof(ProductType)))
            {
                if (ptype == ProductType.GENERAL) continue;
                string typeName = ptype.ToString().ToLower();
                handler.RegisterCommand($"list type {typeName.ToLower()}", () =>
                {
                    if (useStruct) throw new MessageException("This command for classes use only");
                    Console.Write(products.PrintProductsByType(ptype));
                }, $"Lists products of type {typeName} (using enum)");
            }
            foreach (Type ctype in new Type[] { typeof(Food), typeof(ElectricalAppliance), typeof(Chemical) })
            {
                string typeName = ctype.Name;
                handler.RegisterCommand($"list class {typeName.ToLower()}", () =>
                {
                    if (useStruct) throw new MessageException("This command for classes use only");
                    Console.Write(
                        ctype == typeof(Food) ? products.PrintProductsByClass<Food>() :
                        ctype == typeof(ElectricalAppliance) ? products.PrintProductsByClass<ElectricalAppliance>() :
                        ctype == typeof(Chemical) ? products.PrintProductsByClass<Chemical>() :
                        "No such class");
                }, $"Lists products of class {typeName} (using is (product is ClassName))");
            }
            foreach (ProductsStructRealization.IProduct.ProductType ptype in Enum.GetValues(typeof(ProductsStructRealization.IProduct.ProductType)))
            {
                
                if (ptype == ProductsStructRealization.IProduct.ProductType.GENERAL) continue;
                string typeName = ptype.ToString().ToLower();
                handler.RegisterCommand($"list struct {typeName.ToLower()}", () =>
                {
                    if (!useStruct) throw new MessageException("This command for structs use only");
                    Console.Write(productsStruct.PrintProductsByType(ptype));
                }, $"Lists products of type {typeName} (using enum in structs)");
            }

            handler.RegisterCommand("clear", () =>
            {
                if (!useStruct) products.Clear(); else productsStruct.Clear();
            }, "Clears current product list");

            handler.RegisterCommand("switch", () =>
            {
                useStruct = !useStruct;
                Console.WriteLine("Using " + (useStruct ? "structs" : "classes"));
            }, "Switches between using structs and classes");


            handler.PrintHelp();
            handler.Run();
        }
    }
}