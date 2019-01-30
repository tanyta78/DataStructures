namespace ShoppingCenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class ProductCollection : IProductCollection
    {
        private Dictionary<string, OrderedBag<Product>> byName;
        private Dictionary<string, OrderedBag<Product>> byProducer;
        private Dictionary<string, Bag<Product>> byNameAndProducer;
        private OrderedDictionary<decimal, Bag<Product>> byPrice;

        public ProductCollection()
        {
            this.byName = new Dictionary<string, OrderedBag<Product>>();
            this.byPrice = new OrderedDictionary<decimal, Bag<Product>>();
            this.byProducer = new Dictionary<string, OrderedBag<Product>>();
            this.byNameAndProducer = new Dictionary<string, Bag<Product>>();
        }

        public void AddProduct(string name, decimal price, string producer)
        {
            var product = new Product(name, price, producer);

            this.byName.AppendValueToKey(name, product);
            this.byProducer.AppendValueToKey(producer, product);
            this.byPrice.AppendValueToKey(price, product);

            var key = this.GenerateKeyByNameAndProducer(name, producer);
            this.byNameAndProducer.AppendValueToKey(key, product);

            Console.WriteLine("Product added");
        }

        private string GenerateKeyByNameAndProducer(string name, string producer)
        {
            return name + producer;
        }

        public int DeleteProductsByProducer(string producer)
        {
            if (!this.byProducer.ContainsKey(producer))
            {
                return 0;
            }

            var products = this.byProducer[producer];
            this.byProducer.Remove(producer);

            foreach (var product in products)
            {
                this.byName[product.Name].Remove(product);
                
                var key = this.GenerateKeyByNameAndProducer(product.Name, producer);
                this.byNameAndProducer[key].Remove(product);
                this.byPrice[product.Price].Remove(product);
            }

            return products.Count;
        }

        public int DeleteProductByNameAndProducer(string name, string producer)
        {
            var key = this.GenerateKeyByNameAndProducer(name, producer);

            if (!this.byNameAndProducer.ContainsKey(key))
            {
                return 0;
            }

            var products = this.byNameAndProducer[key];
            this.byNameAndProducer.Remove(key);

            foreach (var product in products)
            {
                this.byName[name].Remove(product);
                this.byProducer[producer].Remove(product);
                this.byPrice[product.Price].Remove(product);
            }

            return products.Count;
        }

        public IEnumerable<Product> FindProductsByName(string name)
        {
            if (!this.byName.ContainsKey(name))
            {
                return Enumerable.Empty<Product>();
            }

            var result = this.byName[name];
            return result;
        }

        public IEnumerable<Product> FindProductsByProducer(string producer)
        {
            if (!this.byProducer.ContainsKey(producer))
            {
                return Enumerable.Empty<Product>();
            }

            var result = this.byProducer[producer];
            return result;
        }

        public IEnumerable<Product> FindProductsByPriceRange(decimal fromPrice, decimal toPrice)
        {
            var range = this.byPrice.Range(fromPrice, true, toPrice, true).Values;

            var result = new OrderedBag<Product>();

            foreach (var bag in range)
            {
                foreach (var product in bag)
                {
                    result.Add(product);
                }
            }

            return result;
        }
    }
}
