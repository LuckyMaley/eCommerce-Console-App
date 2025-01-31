using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Models
{
    public class Product
    {
        public long ProductID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public int CategoryID { get; set; }
        public int StockQuantity { get; set; }
        public DateTime ModifiedDate { get; set; }

        public static List<Product> ProductsDataSet = new List<Product>
        {
            new Product { ProductID = 1, Name = "Nike Air Force 1", Brand = "Nike", Description = "Nike Air Force 1 sneakers for track wear", Type="Men", Price = 2100.99f, CategoryID = 1, StockQuantity = 100, ModifiedDate= new DateTime(2018,11,12,12,45,00) },
            new Product { ProductID = 2, Name = "Adidas Yeezy", Brand = "Adidas", Description = "Adidas Yeezy sneakers for summer wear", Type="Women",  Price = 2700.99f, CategoryID = 1, StockQuantity = 100, ModifiedDate= new DateTime(2017,1,22,15,35,10) },
            new Product { ProductID = 3, Name = "Puma T-shirt", Brand = "Puma", Description = "Puma T-shirt for the summer and track wear", Type="Women",  Price = 900.99f, CategoryID = 3, StockQuantity = 100, ModifiedDate= new DateTime(2019,10,12,12,45,00) },
            new Product { ProductID = 4, Name = "Reebok T-shirt", Brand = "Reebok", Description = "Reebok T-shirt for sportswear", Type="Men",  Price = 1000.99f, CategoryID = 3, StockQuantity = 100, ModifiedDate= new DateTime(2018,1,12,12,45,00) },
            new Product { ProductID = 5, Name = "Redbat Jean", Brand = "Redbat", Description = "Redbat Jean for casual wear", Type="Men",  Price = 799.99f, CategoryID = 8, StockQuantity = 100, ModifiedDate= new DateTime(2018,2,12,21,47,50) },
            new Product { ProductID = 6, Name = "Relay Jeans", Brand = "Relay", Description = "Relay jeans for causal  wear", Type="Men",  Price = 800.99f, CategoryID = 8, StockQuantity = 100, ModifiedDate= new DateTime(2019,9,13,8,5,40) },
            new Product { ProductID = 7, Name = "Replay Sandals", Brand = "Replay", Description = "Replay sandals for summer wear", Type="Men",  Price = 200.99f, CategoryID = 2, StockQuantity = 100, ModifiedDate= new DateTime(2020,1,2,13,35,00) },
            new Product { ProductID = 8, Name = "Raw T-shirt", Brand = "Raw", Description = "Raw T-shirt for casual wear", Type="Men",  Price = 2200.99f, CategoryID = 3, StockQuantity = 100, ModifiedDate= new DateTime(2021,11,12,12,45,00) },
            new Product { ProductID = 99875425, Name = "Gant shirt", Brand = "Gant", Description = "Gant shirt for formal wear", Type="Men",  Price = 1100.99f, CategoryID = 4, StockQuantity = 100, ModifiedDate= new DateTime(2022,11,10,10,40,00) },
            new Product { ProductID = 10000000, Name = "Converse Allstar", Brand = "Converse", Description = "Converse sneakers for casual wear", Type="Men",  Price = 2700.99f, CategoryID = 1, StockQuantity = 100, ModifiedDate= new DateTime(2019,3,22,16,5,20) }
        };
    }
}
