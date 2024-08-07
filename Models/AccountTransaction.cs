using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DebuggingAndRefactoringTask1.Enums.Enums;

namespace DebuggingAndRefactoringTask1.Models
{
    public class AccountTransaction
    {
        public int Id { get; set; }
        public int AccountCodeTo { get; set; }
        public int AccountCode { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public AccountInteractionType TranactionType { get; set; }
    }
}
