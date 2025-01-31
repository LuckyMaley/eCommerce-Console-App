using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Models
{
    public class Wishlist
    {
        public int WishlistID { get; set; }
        public int CustomerID { get; set; }
        public long ProductID { get; set; }
        public DateTime AddedDate { get; set; }

        public static List<Wishlist> WishlistsDataSet = new List<Wishlist>()
        {
            new Wishlist() { WishlistID = 1, CustomerID = 1, ProductID = 1, AddedDate = new DateTime(2020,12,14,12,55,00)},
            new Wishlist() { WishlistID = 2, CustomerID = 2, ProductID = 2, AddedDate = new DateTime(2023,1,14,12,55,00)},
            new Wishlist() { WishlistID = 3, CustomerID = 3, ProductID = 3, AddedDate = new DateTime(2023,12,15,12,55,00)},
            new Wishlist() { WishlistID = 4, CustomerID = 4, ProductID = 4, AddedDate = new DateTime(2023,3,17,12,55,00)},
            new Wishlist() { WishlistID = 5, CustomerID = 5, ProductID = 5, AddedDate = new DateTime(2023,8,18,12,55,00)},
            new Wishlist() { WishlistID = 6, CustomerID = 6, ProductID = 6, AddedDate = new DateTime(2023,7,12,12,55,00)},
            new Wishlist() { WishlistID = 7, CustomerID = 7, ProductID = 7, AddedDate = new DateTime(2023,7,1,12,55,00)},
            new Wishlist() { WishlistID = 8, CustomerID = 8, ProductID = 8, AddedDate = new DateTime(2023,2,2,12,55,00)},
            new Wishlist() { WishlistID = 9, CustomerID = 9, ProductID = 99875425, AddedDate = new DateTime(2023,6,14,12,55,00)},
            new Wishlist() { WishlistID = 10, CustomerID = 10, ProductID = 10000000, AddedDate = new DateTime(2023,5,13,12,55,00)}
        };
    }
}
