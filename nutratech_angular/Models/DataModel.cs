using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace vikaro_angular.Models
{
    public class Users
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MidddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public bool Status { get; set; }
      
    }

    public class Business
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string BusinessName { get; set; }
        [Required]
        public string ContactPerson { get; set; }
        public string BusinessAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string BusinessType { get; set; }
        public int CustomerType { get; set; }
        public string Remarks { get; set; }
    }

    public class BusinessContactInfo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int BusinessId { get; set; }
        [Required]
        public string ContactType { get; set; }
        public string ContactDetails { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class CustomerType
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }

}
