using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Contracts;
using VendingMachine.Models;

namespace VendingMachine
{
    public class VendingMachine : IVendingMachine
    {
        private readonly IActivities _activities;
        public VendingMachine(IActivities activities)
        {
            _activities = activities;
        }

        public List<ActualProduct> GetProducts()
        {
            var actualProducts = new List<ActualProduct>();
            var products = _activities.GetAllProducts();
            foreach (var prod in products)
            {
                var actualProduct = new ActualProduct();
                actualProduct.Product = prod;
                actualProduct.Quantity = _activities.GetProductQuantityLeft(prod.Id);
                actualProducts.Add(actualProduct);
            }

            return actualProducts;
        }

        public Product RemoveProduct(int id)
        {
            var product = _activities.GetProductById(id);
            if (product == null) throw new Exception("Invalid Product Id");

            _activities.ReduceStockQuantity(product.Id);

            return product;
        }

        public Product GetProductById(int id)
        {
            var product = _activities.GetProductById(id);

            return product;
        }

        public bool PayForProduct(int productId, decimal amount)
        {
            var product = _activities.GetProductById(productId);
            return amount >= product.Price;
        }

        public bool IsValidAmount(decimal amount)
        {
            //valid coins (5cts to 2€) and reject invalid ones (1 and 2 cts).
            return (amount >= 0.05m && amount <= 2 && amount != 0.01m && amount != 0.02m);
        }

    }
}
