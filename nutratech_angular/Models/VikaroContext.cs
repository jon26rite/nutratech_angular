using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace vikaro_angular.Models
{
    public class VikaroContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public VikaroContext() : base("name=VikaroContext")
        {
        }

        public System.Data.Entity.DbSet<vikaro_angular.Models.Users> Users { get; set; }

        public System.Data.Entity.DbSet<vikaro_angular.Models.CustomerType> CustomerTypes { get; set; }

        public System.Data.Entity.DbSet<vikaro_angular.Models.Business> Businesses { get; set; }

    }
}
