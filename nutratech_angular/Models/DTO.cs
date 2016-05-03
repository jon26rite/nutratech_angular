using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vikaro_angular.Models
{
    public class JournalDTO
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public String Description { get; set; }
        public String DebitCreditMode { get; set; }
        public Double Amount { get; set; }

       

        public AccountDTO Account { get; set; }
    }

    public class AccountDTO
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }
}