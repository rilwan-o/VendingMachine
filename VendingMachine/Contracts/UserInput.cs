using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Contracts
{
    public class UserInput : IUserInput
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
