using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Models;

namespace VendingMachine.Contracts
{
    public class Activities : IActivities
    {
        List<Product> products = new List<Product> { 
            new Product { Id=1, Name = "COLA", Price = 1.00m},
            new Product { Id=2, Name = "Chips", Price = 0.50m},
            new Product { Id=3, Name = "Candy", Price = 0.65m}
        };
        
        List<Stock> stocks = new List<Stock> { 
            new Stock{ProductId=1, Quantity=15},
            new Stock{ProductId=2, Quantity=10},
            new Stock{ProductId=3, Quantity=20}

        };

        public List<Product> GetAllProducts()
        {
            return products;
        }

        public Product GetProductById(int id)
        {
            return products.FirstOrDefault(x => x.Id.Equals(id));
        }

        public int GetProductQuantityLeft(int id)
        {
            var stock = stocks.FirstOrDefault(x => x.ProductId.Equals(id));
            return stock.Quantity;
        }

        public void ReduceStockQuantity(int id)
        {
            var stock = stocks.FirstOrDefault(x => x.ProductId.Equals(id));
            if (stock.Quantity > 0) stock.Quantity--;
            else
            {
                throw new Exception("Out of stock");
            }
        }

      
    }
}
