using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebuggingAndRefactoringTask1.Enums
{
    public static class Enums
    {
        public enum MessageType
        {
            Information = 0,
            Success = 1,
            Warning = 2,
            Error = 3,
            MenuItem = 4,
            Input = 5

        }
        public enum AccountInteractionType
        {
            Deposit = 0,
            Withdraw = 1
        }
    }
}
