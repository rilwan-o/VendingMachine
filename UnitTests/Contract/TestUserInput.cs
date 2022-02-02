using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Contracts;

namespace UnitTests.Contract
{
    public class TestUserInput : IUserInput
    {
        public string GetUserInput()
        {
            return Console.ReadLine();
        }

        public void PrintUserOutput(string output)
        {
            Console.WriteLine(output);
        }
    }
}
