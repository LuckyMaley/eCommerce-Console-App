using MainCode.Models;
using MainCode.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository.AdminMenuOptions
{
    public class WishlistOptions
    {
        public string FindWishlistbyCusID(WishlistsRepository repository, ProductsRepository prodRepository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Wishlist> allOfTheWishlists = repository.ReadGetAllRows();
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();
            int cusId = 0;
            Console.WriteLine("Please enter customer ID: ");
            bool result = int.TryParse(Console.ReadLine(), out cusId);
            CultureInfo ci = new CultureInfo("en-za");
            if (result)
            {
                HashSet<Wishlist> hashSetWishilist = repository.GetCustWishlistItems(cusId);
                if (hashSetWishilist.Count > 0)
                {
                    foreach (var item in hashSetWishilist)
                    {
                        string productName = "";
                        productName = allOfTheProducts.FirstOrDefault(z => z.ProductID == item.ProductID).Name;
                        stringBuilder.AppendLine($"ID: {item.WishlistID}, Product Name: {productName}, Added Date: {item.AddedDate.ToString("dd MMMM yyyy HH:mm")}");
                    }
                }
                else
                {
                    stringBuilder.AppendLine("No Items in customer wishlist");
                }
            }
            else
            {
                stringBuilder.AppendLine("Please enter a number");
            }
            return stringBuilder.ToString();
        }

        public string FindWishlistbyDate(WishlistsRepository repository, ProductsRepository prodRepository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Wishlist> allOfTheWishlists = repository.ReadGetAllRows();
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();
            Console.WriteLine("Please type in the wishlist added date (dd/MM/yyy): ");
            string response = Console.ReadLine();
            if (response != "")
            {
                var wishlist = repository.ReadRowByDate(response);
                if (wishlist != null)
                {
                    foreach (var item in wishlist)
                    {
                        string productName = "";
                        productName = allOfTheProducts.FirstOrDefault(z => z.ProductID == item.ProductID).Name;
                        stringBuilder.AppendLine($"ID: {item.WishlistID}, Product Name: {productName}, Added Date: {item.AddedDate.ToString("dd MMMM yyyy HH:mm")}");
                    }
                }
                else
                {
                    stringBuilder.AppendLine("no wishlist exists for that date, Please try again");
                }
            }
            else
            {
                stringBuilder.AppendLine("Please enter a wishlist date");
            }
            return stringBuilder.ToString();
        }

        public string FindWishlistbetweenDates(WishlistsRepository repository, ProductsRepository prodRepository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Wishlist> allOfTheWishlists = repository.ReadGetAllRows();
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();
            Console.WriteLine("Please type in the starting wishlist date (dd/MM/yyy): ");
            string response = Console.ReadLine();

            Console.WriteLine("Please type in the ending wishlist date (dd/MM/yyy): ");
            string responseTwo = Console.ReadLine();
            if (response != "" || responseTwo != "")
            {
                var wishlist = repository.ReadRowByDate(response, responseTwo);
                if (wishlist != null)
                {
                    foreach (var item in wishlist)
                    {
                        string productName = "";
                        productName = allOfTheProducts.FirstOrDefault(z => z.ProductID == item.ProductID).Name;
                        stringBuilder.AppendLine($"ID: {item.WishlistID}, Product Name: {productName}, Added Date: {item.AddedDate.ToString("dd MMMM yyyy HH:mm")}");
                    }
                }
                else
                {
                    stringBuilder.AppendLine("no wishlist not exists for those dates, Please try again");
                }
            }
            else
            {
                stringBuilder.AppendLine("Please enter both wishlist dates");
            }
            return stringBuilder.ToString();
        }
    }
}
