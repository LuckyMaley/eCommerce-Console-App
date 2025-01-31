using MainCode.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository
{
    public class WishlistsRepository 
    {
        protected static List<Wishlist> allWishlists;

        public WishlistsRepository()
        {
            allWishlists = ReadGetAllRows();
        }

        public virtual List<Wishlist> ReadGetAllRows()
        {
            allWishlists = new List<Wishlist>();
            foreach (var wish in Wishlist.WishlistsDataSet)
            {
                allWishlists.Add(new Wishlist()
                {
                   WishlistID = wish.WishlistID,
                   CustomerID = wish.CustomerID,
                   ProductID = wish.ProductID,
                   AddedDate = wish.AddedDate,
                });
            }
            return allWishlists;
        }

        public bool CheckEntitycusID(int wId, int cusId)
        {
            int count = allWishlists.Where(c => c.CustomerID == cusId && c.WishlistID == wId).Count();
            return count != 0 ? true : false;
        }

        public void AddEntity(Wishlist entity)
        {
            allWishlists.Add(entity);
            Wishlist.WishlistsDataSet.Add(entity);
            Console.WriteLine("Item has been added to Wishlist");
        }

        public void DeleteRow(int id)
        {
            var ds = allWishlists.FirstOrDefault(c => c.WishlistID == id);
            if (ds != null)
            {
                allWishlists.RemoveAll(c => c.WishlistID == id);
                Wishlist.WishlistsDataSet.RemoveAll(c => c.WishlistID == id);
                Console.WriteLine("Item has been removed from Wishlist");
            }
            else
            {
                Console.WriteLine("No item to remove");
            }
        }

        public HashSet<Wishlist> GetCustWishlistItems( int cusId)
        {
            var ds = allWishlists.Where(c => c.CustomerID == cusId).ToList();
            HashSet<Wishlist> wishlistItems = new HashSet<Wishlist>();
            foreach (var item in ds)
            {
                wishlistItems.Add(item);
            }
            return wishlistItems;
        }

        public List<Wishlist> ReadRowByDate(string date)
        {
            WishlistsRepository repository = new WishlistsRepository();
            List<Wishlist> allOfTheWishlists = repository.ReadGetAllRows();

            string defaultDateString = "10/08/2008";
            string format = "dd/MM/yyyy";
            CultureInfo ci = new CultureInfo("en-za");

            DateTime dateTime;
            if (!DateTime.TryParseExact(date, format, ci, System.Globalization.DateTimeStyles.None, out dateTime))
            {
                dateTime = DateTime.ParseExact(defaultDateString, format, ci);
            }
            IEnumerable<Wishlist> wishlist = allOfTheWishlists.Where(c => c.AddedDate.Date == dateTime.Date);
            List<Wishlist> wishlists = new List<Wishlist>();
            if (wishlist != null)
            {
                wishlists = wishlist.ToList();
            }
            else
            {
                wishlists = null;
            }
            return wishlists;
        }

        public List<Wishlist> ReadRowByDate(string beginDate, string endDate)
        {
            WishlistsRepository repository = new WishlistsRepository();
            List<Wishlist> allOfTheWishlists = repository.ReadGetAllRows();


            string defaultDateString = "10/08/2008";
            string format = "dd/MM/yyyy";
            CultureInfo ci = new CultureInfo("en-za");

            DateTime beginDateTime;
            if (!DateTime.TryParseExact(beginDate, format, ci, System.Globalization.DateTimeStyles.None, out beginDateTime))
            {
                beginDateTime = DateTime.ParseExact(defaultDateString, format, ci);
            }

            DateTime endDateTime;
            if (!DateTime.TryParseExact(endDate, format, ci, System.Globalization.DateTimeStyles.None, out endDateTime))
            {
                endDateTime = DateTime.ParseExact(defaultDateString, format, ci);
            }
            IEnumerable<Wishlist> wishlist = allOfTheWishlists.Where(c => c.AddedDate.Date >= beginDateTime.Date && c.AddedDate.Date <= endDateTime.Date);
            List<Wishlist> wishlists = new List<Wishlist>();
            if (wishlist != null)
            {
                wishlists = wishlist.ToList();
            }
            else
            {
                wishlists = null;
            }
            return wishlists;
        }
    }
}
