using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebuggingAndRefactoringTask1.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int AccountCode { get; set; }
        public string AccountName { get; set; }
        public double AccountBalance { get; set; }
    }
}
