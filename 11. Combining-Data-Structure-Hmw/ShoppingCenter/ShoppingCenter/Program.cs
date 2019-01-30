namespace ShoppingCenter
{
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            int commandsNumber = int.Parse(Console.ReadLine());
            var center = new ProductCollection();

            for (var i = 0; i < commandsNumber; i++)
            {

                var line = Console.ReadLine();
                if (line == null)
                {
                    break;
                }

                var command = line.Substring(0, line.IndexOf(" "));
                var tokens = line.Substring(line.IndexOf(" ") + 1).Split(';');

                switch (command)
                {
                    case "AddProduct":
                        var name = tokens[0];
                        var price = decimal.Parse(tokens[1]);
                        var producer = tokens[2];
                        center.AddProduct(name, price, producer);
                        break;
                    case "DeleteProducts":
                        int count;
                        if (tokens.Length == 1)
                        {
                            count = center.DeleteProductsByProducer(tokens[0]);
                        }
                        else
                        {
                            count = center.DeleteProductByNameAndProducer(
                                tokens[0],
                                tokens[1]);
                        }

                        Console.WriteLine(count != 0
                            ? $"{count} products deleted"
                            : "No products found");
                        break;
                    case "FindProductsByName":
                        var result =
                            center.FindProductsByName(tokens[0]).ToList();
                        Console.WriteLine(result.Count != 0
                            ? string.Join(Environment.NewLine, result)
                            : "No products found");
                        break;
                    case "FindProductsByProducer":
                        result =
                            center.FindProductsByProducer(tokens[0]).ToList();
                        Console.WriteLine(result.Count != 0
                            ? string.Join(Environment.NewLine, result)
                            : "No products found");
                        break;
                    case "FindProductsByPriceRange":
                        result =
                            center.FindProductsByPriceRange(
                                decimal.Parse(tokens[0]),
                                decimal.Parse(tokens[1])).ToList();
                        Console.WriteLine(result.Any()
                            ? string.Join(Environment.NewLine, result)
                            : "No products found");
                        break;
                }
            }
        }
    }
}
