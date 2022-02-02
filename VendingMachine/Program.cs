using System;
using System.Collections.Generic;
using VendingMachine.Contracts;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserInput userInput = new UserInput();
            var vm = new VendingMachineImpl(userInput);


            vm.Run(Console.ReadLine());

        }


      
    }
}
