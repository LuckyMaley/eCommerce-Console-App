using MainCode.Models;
using MainCode.Repository;
using static MainCode.MainCodeStaticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainCode.Repository.CustomerMenuOptions;

namespace MainCode
{
    internal partial class MenuOptions
    {
        public static void CustomerMenu()
        {
            CartOptions cartOptions = new CartOptions();
            CustomersRepository customersRepository = new CustomersRepository();
            OrderDetailsRepository orderDetailsRepository = new OrderDetailsRepository();
            OrdersRepository ordersRepository = new OrdersRepository();
            ProductsRepository productsRepository = new ProductsRepository();
            ShippingsRepository shippingsRepository = new ShippingsRepository();
            WishlistsRepository wishlistsRepository = new WishlistsRepository();
            string exitMenu = "99";
            string menuOption = "";

            do
            {
                Console.WriteLine("\nInstant Order eCommerce System Customer Main Menu\n==================================================================");
                Person.GetDetails();
                ShoppingCart.GetCartNumItems();
                Console.WriteLine("==================================================================");
                Console.WriteLine("1. Browse All Products");
                Console.WriteLine("2. Browse Products by Type");
                Console.WriteLine("3. Browse Products by Category");
                Console.WriteLine("4. Add to Cart");
                Console.WriteLine("5. View Cart");
                Console.WriteLine("6. Remove from Cart");
                Console.WriteLine("7. Clear Cart");
                Console.WriteLine("8. Checkout");
                Console.WriteLine("9. View Order");
                Console.WriteLine("10. View Shipping Status");
                Console.WriteLine("11. Add Product to Wishlist");
                Console.WriteLine("12. View Wishlist Items");
                Console.WriteLine("13. Remove Wishlist Item");
                Console.WriteLine("99. Back to Main Menu");
                Console.WriteLine("x. Exit\nEnter your selected menu option: ");
                menuOption = Console.ReadLine();



                switch (menuOption)
                {
                    case "1":
                        cartOptions.BrowseItems();
                        break;
                    case "2":
                        cartOptions.BrowseItemsByType();
                        break;
                    case "3":
                        cartOptions.BrowseItemsByCategory();
                        break;
                    case "4":
                        cartOptions.AddItemToCart();
                        break;
                    case "5":
                        cartOptions.ViewCartItems();
                        break;
                    case "6":
                        Console.WriteLine(cartOptions.RemoveItemFromCart());
                        break;
                    case "7":
                        Console.WriteLine(cartOptions.ClearAllCartItems());
                        break;
                    case "8":
                        Console.WriteLine(cartOptions.ProceedToCheckOut(ordersRepository, orderDetailsRepository, customersRepository));
                        break;
                    case "9":
                        Console.WriteLine(cartOptions.ViewOrder(ordersRepository, orderDetailsRepository, customersRepository, productsRepository));
                        break;
                    case "10":
                        Console.WriteLine(cartOptions.ViewShippingStatus(shippingsRepository, ordersRepository));
                        break;
                    case "11":
                        Console.WriteLine(cartOptions.AddItemToWishlist(wishlistsRepository, productsRepository));
                        break;
                    case "12":
                        Console.WriteLine(cartOptions.ViewWishlist(wishlistsRepository, productsRepository));
                        break;
                    case "13":
                        Console.WriteLine(cartOptions.RemoveWishlistItem(wishlistsRepository));
                        break;
                    case "99":
                        return;
                    case "x":
                        Console.WriteLine("Goodbye");
                        Console.WriteLine("\nPress any key to continue ...");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please review the menu and Enter your selected menu option");
                        break;
                }
                Console.WriteLine("\nPress any key to continue ...");
                Console.ReadKey();
            }
            while (menuOption != exitMenu);
        }
    }
}
