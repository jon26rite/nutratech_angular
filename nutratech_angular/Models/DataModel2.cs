using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace vikaro_angular.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public String Description { get; set; }
        public String DebitCreditMode { get; set; }
        public Double Amount { get; set; }
        

        public String AuditUser { get; set; }
        public DateTime AuditDate { get; set; }

        //[XmlIgnore]
        //[IgnoreDataMember]
        public virtual Account Account { get; set; }

    }

    public class Account
    {
        public int Id{get; set;}
        public String Name {get; set;}

        public String AuditUser { get; set; }
        public DateTime AuditDate { get; set; }

    }
}