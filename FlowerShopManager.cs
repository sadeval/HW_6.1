using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FlowerShopManagement
{
    public class FlowerShopManager
    {
        private FlowerShopContext _context;

        public FlowerShopManager(FlowerShopContext context)
        {
            _context = context;
        }

        public List<Product> GetProductsByShop(int shopId, string searchTerm)
        {
            return _context.Products
                           .Where(p => p.ShopId == shopId && EF.Functions.Like(p.Name, $"%{searchTerm}%"))
                           .ToList();
        }

        public List<Product> GetProductsBySearchTerm(string searchTerm)
        {
            return _context.Products
                           .Where(p => EF.Functions.Like(p.Name, $"%{searchTerm}%"))
                           .ToList();
        }

        public Product? GetRandomProductFromShop(int shopId)
        {
            return _context.Products
                           .Where(p => p.ShopId == shopId)
                           .OrderBy(p => Guid.NewGuid())
                           .FirstOrDefault();
        }

        public List<Product> GetProductsSortedByPrice(int shopId, bool ascending)
        {
            return _context.Products
                           .Where(p => p.ShopId == shopId)
                           .OrderBy(p => ascending ? p.Price : -p.Price)
                           .ToList();
        }

        public List<CityReport> GetReportByCity(int cityId)
        {
            return _context.Shops
                           .Where(s => s.CityId == cityId)
                           .Select(s => new CityReport
                           {
                               ShopName = s.Name,
                               TotalQuantity = s.Products.Sum(p => p.Quantity),
                               TotalValue = s.Products.Sum(p => p.Price * p.Quantity)
                           })
                           .ToList();
        }

        public class CityReport
        {
            public string? ShopName { get; set; }
            public int TotalQuantity { get; set; }
            public decimal TotalValue { get; set; }
        }

        public void DisplayBusinessInfo()
        {
            var cities = _context.Cities.Include(c => c.Shops).ThenInclude(s => s.Products).ToList();

            foreach (var city in cities)
            {
                Console.WriteLine($"Город: {city.Name}");

                foreach (var shop in city.Shops)
                {
                    Console.WriteLine($"  Магазин: {shop.Name}");
                    Console.WriteLine("    Товары:");
                    foreach (var product in shop.Products)
                    {
                        Console.WriteLine($"      {product.Name}: {product.Price} грн.");
                    }
                }
            }
        }

        public List<Product> GetExpensiveProductsFromShop(int shopId, decimal minPrice)
        {
            return _context.Products
                           .Where(p => p.ShopId == shopId && p.Price > minPrice)
                           .ToList();
        }

        public List<string> GetShopsWithMoreThanNProducts(int minQuantity)
        {
            return _context.Shops
                           .Where(s => s.Products.Count > minQuantity)
                           .Select(s => s.Name ?? string.Empty)
                           .ToList();
        }

        public List<Shop> GetShopsByCity(int cityId)
        {
            return _context.Cities
                           .Where(c => c.CityId == cityId)
                           .SelectMany(c => c.Shops)
                           .ToList();
        }

        public List<CityAveragePrice> GetAveragePriceByCity()
        {
            return _context.Shops
                           .GroupBy(s => s.City.Name)
                           .Select(g => new CityAveragePrice
                           {
                               CityName = g.Key,
                               Shops = g.Select(s => new ShopAveragePrice
                               {
                                   ShopName = s.Name,
                                   AveragePrice = s.Products.Average(p => p.Price)
                               }).ToList()
                           })
                           .ToList();
        }

        public class CityAveragePrice
        {
            public string? CityName { get; set; }
            public List<ShopAveragePrice> Shops { get; set; } = new List<ShopAveragePrice>();
        }

        public class ShopAveragePrice
        {
            public string? ShopName { get; set; }
            public decimal AveragePrice { get; set; }
        }
    }
}
