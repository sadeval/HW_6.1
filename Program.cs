using System;
using FlowerShopManagement;

class Program
{
    static void Main()
    {
        using (var context = new FlowerShopContext())
        {
            var manager = new FlowerShopManager(context);

            var productsInShop = manager.GetProductsByShop(1, "Rose");
            var allProducts = manager.GetProductsBySearchTerm("Rose");
            var randomProduct = manager.GetRandomProductFromShop(1);
            var sortedProducts = manager.GetProductsSortedByPrice(1, true);
            var report = manager.GetReportByCity(1);
            manager.DisplayBusinessInfo();
            var expensiveProducts = manager.GetExpensiveProductsFromShop(1, 150m);
            var shopsWithMoreProducts = manager.GetShopsWithMoreThanNProducts(10);
            var shopsInCity = manager.GetShopsByCity(1);
            var averagePriceByCity = manager.GetAveragePriceByCity();

            Console.WriteLine("Продукты в магазине 1:");
            foreach (var product in productsInShop)
            {
                Console.WriteLine($"{product.Name}: {product.Price} грн.");
            }
        }
    }
}
