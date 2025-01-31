using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public long ProductID { get; set; }
        public int CustomerID { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public static List<Review> ReviewsDataSet = new List<Review>()
        {
            new Review() { ReviewID = 1, ProductID = 1, CustomerID = 1, Rating = 4, Title="Great Product", Comment="I liked the product", ReviewDate= new DateTime(2023,12,12,12,55,00)},
            new Review() { ReviewID = 2, ProductID = 2, CustomerID = 2, Rating = 4, Title="Great Product", Comment="I liked the product", ReviewDate= new DateTime(2017,5,22,15,45,10)},
            new Review() { ReviewID = 3, ProductID = 3, CustomerID = 3, Rating = 4, Title="Great Product", Comment="I liked the product", ReviewDate= new DateTime(2019,12,12,12,55,00)},
            new Review() { ReviewID = 4, ProductID = 4, CustomerID = 4, Rating = 4, Title="Great Product", Comment="I liked the product", ReviewDate= new DateTime(2018,6,12,12,55,00)},
            new Review() { ReviewID = 5, ProductID = 5, CustomerID = 5, Rating = 5, Title="Excellent Product", Comment="Excellent quality", ReviewDate= new DateTime(2018,5,12,21,57,50)},
            new Review() { ReviewID = 6, ProductID = 6, CustomerID = 6, Rating = 4, Title="Great Product", Comment="I liked the product", ReviewDate= new DateTime(2019,11,13,8,6,40)},
            new Review() { ReviewID = 7, ProductID = 7, CustomerID = 7, Rating = 4, Title="Great Product", Comment="I liked the product", ReviewDate= new DateTime(2021,3,2,13,45,00)},
            new Review() { ReviewID = 8, ProductID = 8, CustomerID = 8, Rating = 4, Title="Great Product", Comment="I liked the product", ReviewDate= new DateTime(2022,1,12,12,55,00)},
            new Review() { ReviewID = 9, ProductID = 99875425, CustomerID = 9, Rating = 4, Title="Great Product", Comment="I liked the product", ReviewDate= new DateTime(2023,12,10,10,50,00)},
            new Review() { ReviewID = 10, ProductID = 10000000, CustomerID = 10, Rating = 3, Title="Not a bad Product", Comment="It was okay", ReviewDate= new DateTime(2021,4,22,16,10,20)}
        };
    }
}
