namespace ShoppingCenter
{
    using System.Collections.Generic;

    interface IProductCollection
    {
        void AddProduct(string name, decimal price, string producer);
        int DeleteProductsByProducer(string producer);
        int DeleteProductByNameAndProducer(string name, string producer);
        IEnumerable<Product> FindProductsByName(string name);
        IEnumerable<Product> FindProductsByProducer(string producer);
        IEnumerable<Product> FindProductsByPriceRange(decimal fromPrice, decimal toPrice);
    }
}
