using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Contracts
{
    public interface IUserInput
    {
        string GetUserInput();
        void PrintUserOutput(string output);
    }


}
