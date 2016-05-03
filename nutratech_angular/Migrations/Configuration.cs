namespace vikaro_angular.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<vikaro_angular.Models.VikaroContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(vikaro_angular.Models.VikaroContext context)
        {
            
            context.Users.AddOrUpdate(x => x.Id,
                new Users()
                {
                    Id = 1,
                    UserName = "vikaro",
                    Password = "vikaro",
                    FirstName = "Vikaro",
                    MidddleName = "Vikaro",
                    LastName = "Vikaro",
                    Suffix = "",
                    Status = true
                }
                );
            context.CustomerTypes.AddOrUpdate(x => x.Id,
                new CustomerType()
                { 
                    Id = 1,
                    Description="Pets"
                },
                new CustomerType()
                {
                    Id = 2,
                    Description = "Human"
                },
                new CustomerType()
                {
                    Id = 3,
                    Description = "Livestocks"
                }
                );

            context.Businesses.AddOrUpdate(x => x.Id,
                new Business()
                {
                    Id = 1,
                    BusinessName = "Pet Supplier",
                    ContactPerson = "Pets",
                    BusinessAddress = "Pets",
                    ShippingAddress = "Pets",
                    BusinessType = "supplier",
                    CustomerType = 1,
                    Remarks = "None"
                }
                );
            
        }
    }
}
