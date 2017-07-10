namespace WhereoWareTest.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WhereoWareTest.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WhereoWareDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WhereoWareTest.Models.WhereoWareDb context)
        {
            var Products = new List<Product>
            {
             new Product{ SKU = "1234576K-P",Name="Coat Mountain Steal1", Overview="Test Data",ImagePath="1234576K-P.jpg"},
             new Product{ SKU = "2134567L-O",Name="Coat Mountain Steal2", Overview="Test Data",ImagePath="2134567L-O.jpg"},
             new Product{ SKU = "7654321L-T", Name ="Coat Mountain Steal3", Overview = "Test Data", ImagePath = "7654321L-T.jpg" }
            };
            Products.ForEach(p => context.Products.AddOrUpdate(item => item.SKU, p));
            context.SaveChanges();
        }
    }
}
