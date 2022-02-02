using System;
using System.IO;
using UnitTests.Contract;
using VendingMachine;
using Xunit;

namespace UnitTests
{
    public class VendingMachineTest
    {
        [Fact]
        public void InsertMoney_InsertCoin_ReturnsSameAmount()
        {

           var vm = new VendingMachineImpl(new TestUserInput());
            var result = 1;
            Assert.Equal(result, vm.InsertMoney("enter 1"));
        }

        [Fact]
        public void Show_TypeShow_ReturnsAllProducts()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var vm = new VendingMachineImpl(new TestUserInput());
            vm.Show();

            Assert.Contains("cola", stringWriter.ToString().ToLower());
        }

        [Fact]
        public void SelectAndPay_TypeProductId_ReturnsProductWithCahnge()
        {           
            var vm = new VendingMachineImpl(new TestUserInput());
            vm.SetInsertedAmount(1);
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            vm.SelectAndPay( "select 1");
            Assert.Contains("thank you", stringWriter.ToString().ToLower());
        }


        [Fact]
        public void ReturnCoins_TypeReturnCoin_ReturnInsertedAmount()
        {
            var vm = new VendingMachineImpl(new TestUserInput());
            vm.SetInsertedAmount(1);
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            vm.ReturnCoins();
            Assert.Contains("1", stringWriter.ToString().ToLower());
        }


    }
}
