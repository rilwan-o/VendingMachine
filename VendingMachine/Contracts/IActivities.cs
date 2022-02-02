using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Models;

namespace VendingMachine.Contracts
{
    public interface IActivities
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        int GetProductQuantityLeft(int id);
        void ReduceStockQuantity(int id);
      //  int GetChange(decimal coin);
      //  int GetCoin();


    }
}
