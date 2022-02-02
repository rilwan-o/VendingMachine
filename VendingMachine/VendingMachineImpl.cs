using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Contracts;

namespace VendingMachine
{
    public class VendingMachineImpl
    {
        private readonly IUserInput _userInput;
        private readonly IVendingMachine vm;
        private readonly IActivities _activities;
        private decimal totalVmCoins = 0.00m;
        private decimal insertedAmount = 0.00m;
        private bool exit = true;
        public string result = string.Empty;

        public VendingMachineImpl(IUserInput userInput)
        {
            _userInput = userInput;
            _activities = new Activities();
            vm = new VendingMachine(_activities);
        }

        public void Run(string userInput)
        {
            do
            {
                if (string.IsNullOrEmpty(userInput.Trim()))
                {
                    _userInput.PrintUserOutput("Invalid input. Please start by typing in 'show'");
                    userInput = _userInput.GetUserInput();
                }

                else if (userInput.ToLower().Contains("enter"))
                {
                    insertedAmount = InsertMoney(userInput);
                    userInput = _userInput.GetUserInput();

                }

                else if (userInput.ToLower().Equals("return coins"))
                {
                    ReturnCoins();
                    insertedAmount = InsertCoin("userInput");
                    userInput = _userInput.GetUserInput();

                }

                if (userInput.ToLower().Equals("show"))
                {
                    Show();
                    userInput = _userInput.GetUserInput();
                }

                else if (userInput.ToLower().Contains("select"))
                {
                    SelectAndPay(userInput);
                    userInput = _userInput.GetUserInput();
                }

                else if (userInput.ToLower().Contains("exit")) exit = false;
                else
                {
                    _userInput.PrintUserOutput("Invalid input. Please start by typing in 'show'");
                    if (insertedAmount > 0.00m)
                    {
                        ReturnCoins();
                    }
                    userInput = _userInput.GetUserInput();
                }

            }
            while (exit);

        }

        public void SelectAndPay(string command)
        {
            string selectedId = command.Trim()[command.Length - 1].ToString();
            int id;
            while (!Int32.TryParse(selectedId, out id))
            {
                Console.Write("Invalid item number selected ");
                command = _userInput.GetUserInput();
                selectedId = command.Trim()[command.Length - 1].ToString();
            }

            var change = BuyProduct(id, insertedAmount);
            if (change != -0.1m && change != -0.2m)
            {
                var product = vm.GetProductById(id);
                if ((totalVmCoins + product.Price) >= insertedAmount)
                {
                    totalVmCoins = totalVmCoins + product.Price;
                    _userInput.PrintUserOutput($"Take {product.Name}");
                    if (change > 0.0m)
                    {
                        _userInput.PrintUserOutput("Please take your change: " + change);
                    }

                    _userInput.PrintUserOutput("THANK YOU");
                    insertedAmount = 0.00m;
                }
                else
                {
                    _userInput.PrintUserOutput("EXACT CHANGE ONLY");
                }

            }
            else if (change == -0.2m)
            {
                _userInput.PrintUserOutput("SOLD OUT");
            }
            else
            {
                insertedAmount = InsertCoin("amount");
                InsertMoney("enter " + insertedAmount);

            }

        }

        public void Show()
        {
            var products = vm.GetProducts();
            foreach (var p in products)
            {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                string quantity = p.Quantity == 0 ? "SOLD OUT" : p.Quantity.ToString() + " Item Left";
                _userInput.PrintUserOutput($"{p.Product.Id} {p.Product.Name} {p.Product.Price}€ -  {quantity}");

            }
        }

        public void ReturnCoins()
        {
            _userInput.PrintUserOutput("Take your coins: " + insertedAmount);
            insertedAmount = 0.00m;
        }

        public decimal InsertMoney(string command)
        {
            var amount = command[command.Length - 1].ToString();
            insertedAmount = InsertCoin(amount);
            while (!vm.IsValidAmount(insertedAmount))
            {
                _userInput.PrintUserOutput("Invalid Amount entered and rejected : " + insertedAmount);
                insertedAmount = InsertCoin(amount);
            }

            _userInput.PrintUserOutput("Amount entered: " + insertedAmount);
            return insertedAmount;
        }

        public decimal BuyProduct(int id, decimal amount)
        {
            var payed = vm.PayForProduct(id, amount);
            const decimal insufficientPay = -0.1m;
            const decimal outOfStock = -0.2m;
            if (payed)
            {
                try
                {
                    var prod = vm.RemoveProduct(id);
                    var change = amount - prod.Price;
                    return change;
                }
                catch (Exception ex)
                {
                    return outOfStock;
                }
            }

            return insufficientPay;
        }

        public decimal InsertCoin(string amount)
        {
            while (!decimal.TryParse(amount, out insertedAmount))
            {
                _userInput.PrintUserOutput("INSERT COIN");

                var command = _userInput.GetUserInput();
                amount = command[command.Length - 1].ToString();
            }

            return insertedAmount;
        }

        public void SetInsertedAmount(decimal amount) { insertedAmount = Math.Abs(amount); }

    }
}
