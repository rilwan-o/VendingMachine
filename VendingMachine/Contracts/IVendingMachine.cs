using System.Collections.Generic;
using VendingMachine.Models;

namespace VendingMachine
{
    public interface IVendingMachine
    {
        Product GetProductById(int id);
        List<ActualProduct> GetProducts();
        bool IsValidAmount(decimal amount);
        bool PayForProduct(int productId, decimal amount);
        Product RemoveProduct(int id);
    }
}