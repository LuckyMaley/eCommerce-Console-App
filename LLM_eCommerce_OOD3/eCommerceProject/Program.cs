using MainCode.Models;
using MainCode.Repository;
using MainCode;
using static MainCode.MainCodeStaticObjects;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace eCommerceProject;

class Program
{
    
    static void Main(string[] args)
    {

        Console.Title = "OOD Project - Instant Order eCommerce System";
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;

        Console.Clear();

        string exitMenu = "x";
        string menuOption = "";

        AddProductsToDict();

        while (menuOption != exitMenu)
        {
            Console.WriteLine("\nInstant Order eCommerce System Main Menu\n==================================================================");
            Console.WriteLine("1. Customer Menu");
            Console.WriteLine("2. Admin Menu");
            Console.WriteLine("x. Exit\nEnter your selected menu option: ");
            menuOption = Console.ReadLine();

            if (menuOption == "X")
            {
                menuOption = menuOption.ToLower();
            }

            switch (menuOption)
            {
                case "1":
                    CustomerLogin();
                    break;
                case "2":
                    AdminLogin();
                    break;
                case "x":
                    Console.WriteLine("Goodbye");
                    break;
                default:
                    Console.WriteLine("Please review the menu and Enter your selected menu option");
                    break;
            }
            Console.WriteLine("\nPress any key to continue ...");
            Console.ReadKey();
        }
    }

    static void CustomerLogin()
    {
        CustomersRepository customersRepository = new CustomersRepository();
        MainCodeStaticObjects person = new MainCodeStaticObjects();
        customersRepository.Login();
    }

    static void AdminLogin()
    {
        AdminsRepository adminsRepository = new AdminsRepository();
        adminsRepository.Login();
    }

    static void AddProductsToDict()
    {
        ProductsRepository prodRepository = new ProductsRepository();
        List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();
        foreach (Product product in allOfTheProducts)
        {
            products.Add(product.ProductID, product);
        }
    }

}

