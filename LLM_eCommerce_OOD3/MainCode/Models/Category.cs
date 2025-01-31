using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public static List<Category> CategoriesDataSet = new List<Category>()
        {
            new Category{CategoryID=1, Name="Sneaker"},
            new Category{CategoryID=2, Name="Sandals"},
            new Category{CategoryID=3, Name="T-shirt"},
            new Category{CategoryID=4, Name="Shirt"},
            new Category{CategoryID=5, Name="Jacket"},
            new Category{CategoryID=6, Name="Hoodie"},
            new Category{CategoryID=7, Name="Jersey"},
            new Category{CategoryID=8, Name="Jeans"},
            new Category{CategoryID=9, Name="Shorts"},
            new Category{CategoryID=10, Name="Hats"}
        };
    }
}
