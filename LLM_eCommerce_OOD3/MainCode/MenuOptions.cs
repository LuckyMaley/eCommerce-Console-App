using MainCode;
using MainCode.Models;
using MainCode.Repository;
using static MainCode.MainCodeStaticObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainCode.Repository.AdminMenuOptions;

namespace MainCode
{
    internal partial class MenuOptions
    {
        public static void AdminMenu()
        {
            string exitMenu = "99";
            string menuOption = "";
            int count = 0;
            if (count == 0)
            {
                while (menuOption != exitMenu)
                {
                    count = 0;
                    Console.Clear();
                    Console.WriteLine("\nInstant Order eCommerce System Admin Main Menu\n==================================================================");
                    Person.GetDetails();
                    Console.WriteLine("==================================================================");
                    Console.WriteLine("1. Admins");
                    Console.WriteLine("2. Customers");
                    Console.WriteLine("3. Products");
                    Console.WriteLine("4. Categories");
                    Console.WriteLine("5. Orders");
                    Console.WriteLine("6. More\nEnter your selected menu option: ");
                    menuOption = Console.ReadLine();



                    switch (menuOption)
                    {
                        case "1":
                            AdminOptionOneSubMenu();
                            break;
                        case "2":
                            AdminOptionTwoSubMenu();
                            break;
                        case "3":
                            AdminOptionThreeSubMenu();
                            break;
                        case "4":
                            AdminOptionFourSubMenu();
                            break;
                        case "5":
                            AdminOptionFiveSubMenu();
                            break;
                        case "6":
                            AdminMoreMenu(ref count);
                            if(count == 1)
                            {
                                menuOption = "99";
                            }
                            break;
                        default:
                            Console.WriteLine("Please review the menu and Enter your selected menu option");
                            break;
                    }
                }
            }
        }

        public static void AdminMoreMenu(ref int count)
        {
            string exitMenuMore = "x";
            string menuOption = "";

            while (menuOption != exitMenuMore )
            {
                Console.Clear();
                Console.WriteLine("\nInstant Order eCommerce System Admin Main Menu\n==================================================================");
                Person.GetDetails();
                Console.WriteLine("==================================================================");
                Console.WriteLine("7. Order Details");
                Console.WriteLine("8. Payments");
                Console.WriteLine("9. Shippings");
                Console.WriteLine("10. Reviews");
                Console.WriteLine("11. Wishlists");
                Console.WriteLine("99. Back");
                Console.WriteLine("#. Main Menu");
                Console.WriteLine("x. Exit\nEnter your selected menu option: ");
                menuOption = Console.ReadLine();

                if (menuOption == "X")
                {
                    menuOption = menuOption.ToLower();
                }


                switch (menuOption)
                {
                    case "7":
                        AdminOptionSevenSubMenu();
                        break;
                    case "8":
                        AdminOptionEightSubMenu();
                        break;
                    case "9":
                        AdminOptionNineSubMenu();
                        break;
                    case "10":
                        AdminOptionTenSubMenu();
                        break;
                    case "11":
                        AdminOptionElevenSubMenu();
                        break;
                    case "99":
                        menuOption = "x";
                        break;
                    case "#":
                        ++count;
                        menuOption = "x";
                        break;
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
            }
        }

        public static void AdminOptionOneSubMenu()
        {
            AdminsRepository adminsRepository = new AdminsRepository();
            RepositoryBase<Admin> adminsRepositoryBase = new AdminsRepository();
            AdminOptions adminOptions = new AdminOptions(adminsRepository, adminsRepositoryBase);
            string exitMenu = "99";
            string menuOption = "";

            while (menuOption != exitMenu)
            {
                Console.Clear();
                Console.WriteLine("\nInstant Order eCommerce System Sub Menu\n==================================================================");
                Person.GetDetails();
                Console.WriteLine("==================================================================");
                Console.WriteLine("1. Display All Admins");
                Console.WriteLine("2. Find Admin by ID");
                Console.WriteLine("3. Find Admin by First Name");
                Console.WriteLine("4. Find Admin by First Name and Surname");
                Console.WriteLine("5. Add New Admin");
                Console.WriteLine("6. Update Admin Details");
                Console.WriteLine("7. Remove Admin");
                Console.WriteLine("99. Back\nEnter your selected menu option: ");
                menuOption = Console.ReadLine();


                switch (menuOption)
                {
                    case "1":
                        ShowAllAdmins();
                        break;
                    case "2":
                        Console.WriteLine("Please enter the admin ID");
                        int adminID = 0;
                        bool result = int.TryParse(Console.ReadLine(), out adminID);
                        
                        if (result)
                        {
                            Console.WriteLine(adminOptions.FindAdminByID(adminID));
                        }
                        else
                        {
                           Console.WriteLine("Please make sure that you insert a number for the id");
                        }

                        break;
                    case "3":
                        Console.WriteLine("Please enter admin first name: ");
                        string adName = Console.ReadLine();
                        Console.WriteLine(adminOptions.FindAdminByFirstName(adName));
                        break;
                    case "4":
                        Console.WriteLine("Please enter admin first name: ");
                        string fName = Console.ReadLine();
                        Console.WriteLine("Please enter admin surname: ");
                        string lName = Console.ReadLine();
                        Console.WriteLine(adminOptions.FindAdminByFullName(fName, lName));
                        break;
                    case "5":
                        Admin admin = new Admin();
                        List<Admin> admins = adminsRepository.ReadGetAllRows();
                        int newID = admins.Max(c => c.AdminID) + 1;

                        string firstName = "";
                        while (firstName.Trim() == "")
                        {
                            Console.WriteLine("Please enter first name: ");
                            firstName = Console.ReadLine();
                        }
                        string surname = "";
                        while (surname.Trim() == "")
                        {
                            Console.WriteLine("Please enter surname: ");
                            surname = Console.ReadLine();
                        }

                        string email = "";
                        while (email.Trim() == "")
                        {
                            Console.WriteLine("Please enter email (include @ in email): ");
                            email = Console.ReadLine();
                            while (!email.Contains("@"))
                            {
                                Console.WriteLine("Please enter email (include @ in email): ");
                                email = Console.ReadLine();
                            }
                        }

                        string username = "";
                        username = email.Substring(0, email.IndexOf("@"));


                        string address = "";
                        while (address.Trim() == "")
                        {
                            Console.WriteLine("Please enter address (format to follow: {street number}, {street name}, {postal code}): ");
                            address = Console.ReadLine();
                        }

                        string phoneNumber = "";
                        while (phoneNumber.Trim() == "")
                        {
                            Console.WriteLine("Please enter phone number");
                            phoneNumber = Console.ReadLine();
                        }

                        admin.AdminID = newID;
                        admin.FirstName = firstName;
                        admin.Surname = surname;
                        admin.Email = email;
                        admin.Username = username;
                        admin.Address = address;
                        admin.PhoneNumber = phoneNumber;
                        admin.Role = "Admin";
                        int count = 0;
                        do
                        {
                            Console.WriteLine("Are you sure you want to add this new admin (Y/N): ");
                            string response = Console.ReadLine();
                            if (response == "y" || response == "Y")
                            {
                                response = response.ToLower();
                            }
                            else if (response == "n" || response == "N")
                            {
                                response = response.ToLower();
                            }

                            switch (response)
                            {
                                case "y":
                                    Console.WriteLine(adminOptions.AddNewAdmin(admin));
                                    count++;
                                    break;
                                case "n":
                                    Console.WriteLine("Insertion has been cancelled");
                                    count++;
                                    break;
                                default:
                                    Console.WriteLine("Wrong option, please try again");
                                    break;
                            }
                        }
                        while (count == 0);
                        break;
                    case "6":
                        StringBuilder stringBuilder = new StringBuilder();
                        Console.WriteLine("Please enter a admin id:");
                        int adminID1 = 0;
                        bool valid = int.TryParse(Console.ReadLine(), out adminID1);
                        if (valid)
                        {
                            Admin admin1 = new Admin();
                            List<Admin> admins1 = adminsRepository.ReadGetAllRows();
                            admin1 = admins1.FirstOrDefault(c => c.AdminID == adminID1);
                            if (admin1 != null)
                            {


                                string firstName1 = "";
                                Console.WriteLine("Please enter first name (Leave blank if you do not want to update): ");
                                firstName1 = Console.ReadLine();
                                if (firstName1.Trim() == "")
                                {
                                    firstName1 = admin1.FirstName;
                                }
                                string surname1 = "";

                                Console.WriteLine("Please enter surname (Leave blank if you do not want to update): ");
                                surname1 = Console.ReadLine();
                                if (surname1.Trim() == "")
                                {
                                    surname1 = admin1.Surname;
                                }

                                string address1 = "";

                                Console.WriteLine("Please enter address (format to follow: {street number}, {street name}, {postal code}) (Leave blank if you do not want to update): ");
                                address1 = Console.ReadLine();
                                if (address1.Trim() == "")
                                {
                                    address1 = admin1.Address;
                                }

                                string phoneNumber1 = "";
                                Console.WriteLine("Please enter phone number");
                                phoneNumber1 = Console.ReadLine();
                                if (phoneNumber1.Trim() == "")
                                {
                                    phoneNumber1 = admin1.PhoneNumber;
                                }

                                admin1.FirstName = firstName1;
                                admin1.Surname = surname1;
                                admin1.Address = address1;
                                admin1.PhoneNumber = phoneNumber1;

                                int count1 = 0;
                                do
                                {
                                    Console.WriteLine("Are you sure you want to update this admin (Y/N): ");
                                    string response = Console.ReadLine();
                                    if (response == "y" || response == "Y")
                                    {
                                        response = response.ToLower();
                                    }
                                    else if (response == "n" || response == "N")
                                    {
                                        response = response.ToLower();
                                    }

                                    switch (response)
                                    {
                                        case "y":
                                            Console.WriteLine(adminOptions.UpdateAdmin(admin1));
                                            count1++;
                                            break;
                                        case "n":
                                            stringBuilder.AppendLine("Update has been cancelled");
                                            count1++;
                                            break;
                                        default:
                                            Console.WriteLine("Wrong option, please try again");
                                            break;
                                    }
                                }
                                while (count1 == 0);
                            }
                            else
                            {
                                stringBuilder.AppendLine("Admin does not exist");
                            }

                        }
                        else
                        {
                            stringBuilder.AppendLine("Incorrect format of admin id, please try again");
                        }
                        Console.WriteLine(stringBuilder.ToString());
                        break;
                    case "7":
                        List<Admin> allOfTheAdmins = adminsRepository.ReadGetAllRows();
                        StringBuilder stringBuilder2 = new StringBuilder();
                        Console.WriteLine("Please enter the admin ID: ");
                        int adminId = 0;
                        bool validNum = int.TryParse(Console.ReadLine(), out adminId);
                        if (validNum)
                        {
                            bool exists = adminsRepository.CheckIfIdExists(adminId);
                            if (exists)
                            {
                                int count2 = 0;
                                do
                                {
                                    Console.WriteLine("Are you sure you want to remove this admin (Y/N): ");
                                    string response = Console.ReadLine();
                                    if (response == "y" || response == "Y")
                                    {
                                        response = response.ToLower();
                                    }
                                    else if (response == "n" || response == "N")
                                    {
                                        response = response.ToLower();
                                    }

                                    switch (response)
                                    {
                                        case "y":
                                            stringBuilder2.AppendLine(adminOptions.RemoveAdmin(adminId));
                                            count2++;
                                            break;
                                        case "n":
                                            stringBuilder2.AppendLine("Deletion has been cancelled");
                                            count2++;
                                            break;
                                        default:
                                            Console.WriteLine("Wrong option, please try again");
                                            break;
                                    }
                                }
                                while (count2 == 0);

                            }
                            else
                            {
                                stringBuilder2.AppendLine("Admin ID does not exist");
                            }
                        }
                        else
                        {
                            stringBuilder2.AppendLine("Admin ID not in the correct format, please try again");
                        }
                        Console.WriteLine();
                        break;
                    case "99":
                        break;
                    default:
                        Console.WriteLine("Please review the menu and Enter your selected menu option");
                        break;
                }
                Console.WriteLine("\nPress any key to continue ...");
                Console.ReadKey();
            }
        }

        public static void AdminOptionTwoSubMenu()
        {
            CustomersRepository customersRepository = new CustomersRepository();
            RepositoryBase<Customer> customersRepositoryBase = new CustomersRepository();
            IRepository<Customer> customersIRepository = new CustomersRepository();
            CustomerOptions customerOptions = new CustomerOptions(customersRepository, customersRepositoryBase, customersIRepository);
            string exitMenu = "99";
            string menuOption = "";

            while (menuOption != exitMenu)
            {
                Console.Clear();
                Console.WriteLine("\nInstant Order eCommerce System Sub Menu\n==================================================================");
                Person.GetDetails();
                Console.WriteLine("==================================================================");
                Console.WriteLine("1. Display All Customers");
                Console.WriteLine("2. Find Customer by ID");
                Console.WriteLine("3. Find Customer by First Name");
                Console.WriteLine("4. Find Customer by First Name and Surname");
                Console.WriteLine("5. Add New Customer");
                Console.WriteLine("6. Update Customer");
                Console.WriteLine("7. Remove Customer");
                Console.WriteLine("99. Back\nEnter your selected menu option: ");
                menuOption = Console.ReadLine();


                switch (menuOption)
                {
                    case "1":
                        ShowAllCustomers();
                        break;
                    case "2":
                        Console.WriteLine("Please enter the customer ID");
                        int cusID = 0;
                        bool result = int.TryParse(Console.ReadLine(), out cusID);
                        if (result)
                        {
                            Console.WriteLine(customerOptions.FindCustomerByID(cusID));
                        }
                        else
                        {
                            Console.WriteLine("Please make sure that you insert a number for the id");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Please enter customer first name: ");
                        string cusName = Console.ReadLine();
                        Console.WriteLine(customerOptions.FindCustomerByFirstName(cusName));
                        break;
                    case "4":
                        Console.WriteLine("Please enter customer first name: ");
                        string cusName1 = Console.ReadLine();
                        Console.WriteLine("Please enter customer surname: ");
                        string lName = Console.ReadLine();
                        Console.WriteLine(customerOptions.FindCustomerByFullName(cusName1, lName));
                        break;
                    case "5":
                        StringBuilder stringBuilder = new StringBuilder();
                        Customer customer = new Customer();
                        List<Customer> customers = customersRepository.ReadGetAllRows();
                        int newID = customers.Max(c => c.CustomerID) + 1;


                        string firstName = "";
                        while (firstName.Trim() == "")
                        {
                            Console.WriteLine("Please enter first name: ");
                            firstName = Console.ReadLine();
                        }
                        string surname = "";
                        while (surname.Trim() == "")
                        {
                            Console.WriteLine("Please enter surname: ");
                            surname = Console.ReadLine();
                        }

                        string email = "";
                        while (email.Trim() == "")
                        {
                            Console.WriteLine("Please enter email (include @ in email): ");
                            email = Console.ReadLine();
                            while (!email.Contains("@"))
                            {
                                Console.WriteLine("Please enter email (include @ in email): ");
                                email = Console.ReadLine();
                            }
                        }

                        string username = "";
                        username = email.Substring(0, email.IndexOf("@"));


                        string address = "";
                        while (address.Trim() == "")
                        {
                            Console.WriteLine("Please enter address (format to follow: {street number}, {street name}, {postal code}): ");
                            address = Console.ReadLine();
                        }

                        string phoneNumber = "";
                        while (phoneNumber.Trim() == "")
                        {
                            Console.WriteLine("Please enter phone number");
                            phoneNumber = Console.ReadLine();
                        }

                        customer.CustomerID = newID;
                        customer.FirstName = firstName;
                        customer.Surname = surname;
                        customer.Email = email;
                        customer.Username = username;
                        customer.Address = address;
                        customer.PhoneNumber = phoneNumber;
                        int count = 0;
                        do
                        {
                            Console.WriteLine("Are you sure you want to add this new customer (Y/N): ");
                            string response = Console.ReadLine();
                            if (response == "y" || response == "Y")
                            {
                                response = response.ToLower();
                            }
                            else if (response == "n" || response == "N")
                            {
                                response = response.ToLower();
                            }

                            switch (response)
                            {
                                case "y":
                                    stringBuilder.AppendLine(customerOptions.AddNewCustomer(customer));
                                    count++;
                                    break;
                                case "n":
                                    stringBuilder.AppendLine("Insertion has been cancelled");
                                    count++;
                                    break;
                                default:
                                    Console.WriteLine("Wrong option, please try again");
                                    break;
                            }
                        }
                        while (count == 0);
                        Console.WriteLine(stringBuilder.ToString());
                        break;
                    case "6":
                        StringBuilder stringBuilder1 = new StringBuilder();
                        Console.WriteLine("Please enter a customer id:");
                        int customerID = 0;
                        bool valid = int.TryParse(Console.ReadLine(), out customerID);
                        if (valid)
                        {
                            Customer customer1 = new Customer();
                            List<Customer> customers1 = customersRepository.ReadGetAllRows();
                            customer1 = customers1.FirstOrDefault(c => c.CustomerID == customerID);
                            if (customer1 != null)
                            {


                                string firstName1 = "";
                                Console.WriteLine("Please enter first name (Leave blank if you do not want to update): ");
                                firstName1 = Console.ReadLine();
                                if (firstName1.Trim() == "")
                                {
                                    firstName1 = customer1.FirstName;
                                }
                                string surname1 = "";

                                Console.WriteLine("Please enter surname (Leave blank if you do not want to update): ");
                                surname1 = Console.ReadLine();
                                if (surname1.Trim() == "")
                                {
                                    surname1 = customer1.Surname;
                                }

                                string address1 = "";

                                Console.WriteLine("Please enter address (format to follow: {street number}, {street name}, {postal code}) (Leave blank if you do not want to update): ");
                                address1 = Console.ReadLine();
                                if (address1.Trim() == "")
                                {
                                    address1 = customer1.Address;
                                }

                                string phoneNumber1 = "";
                                Console.WriteLine("Please enter phone number");
                                phoneNumber1 = Console.ReadLine();
                                if (phoneNumber1.Trim() == "")
                                {
                                    phoneNumber1 = customer1.PhoneNumber;
                                }

                                customer1.FirstName = firstName1;
                                customer1.Surname = surname1;
                                customer1.Address = address1;
                                customer1.PhoneNumber = phoneNumber1;
                                int count1 = 0;
                                do
                                {
                                    Console.WriteLine("Are you sure you want to update this customer (Y/N): ");
                                    string response = Console.ReadLine();
                                    if (response == "y" || response == "Y")
                                    {
                                        response = response.ToLower();
                                    }
                                    else if (response == "n" || response == "N")
                                    {
                                        response = response.ToLower();
                                    }

                                    switch (response)
                                    {
                                        case "y":
                                            stringBuilder1.AppendLine(customerOptions.UpdateCustomer(customer1));
                                            count1++;
                                            break;
                                        case "n":
                                            stringBuilder1.AppendLine("Update has been cancelled");
                                            count1++;
                                            break;
                                        default:
                                            Console.WriteLine("Wrong option, please try again");
                                            break;
                                    }
                                }
                                while (count1 == 0);
                            }
                            else
                            {
                                stringBuilder1.AppendLine("Customer does not exist");
                            }
                        }
                        else
                        {
                            stringBuilder1.AppendLine("Incorrect format of customer id, please try again");
                        }
                        Console.WriteLine(stringBuilder1.ToString());
                        break;
                    case "7":
                        StringBuilder sb = new StringBuilder();
                        List<Customer> allOfTheCustomers = customersRepository.ReadGetAllRows();
                        Console.WriteLine("Please enter the customer ID: ");
                        int cusId = 0;
                        bool validNum = int.TryParse(Console.ReadLine(), out cusId);
                        if (validNum)
                        {
                            bool exists = customersRepository.CheckIfIdExists(cusId);
                            if (exists)
                            {
                                int count2 = 0;
                                do
                                {
                                    Console.WriteLine("Are you sure you want to remove this customer (Y/N): ");
                                    string response = Console.ReadLine();
                                    if (response == "y" || response == "Y")
                                    {
                                        response = response.ToLower();
                                    }
                                    else if (response == "n" || response == "N")
                                    {
                                        response = response.ToLower();
                                    }

                                    switch (response)
                                    {
                                        case "y":
                                            sb.AppendLine(customerOptions.RemoveCustomer(cusId));
                                            count2++;
                                            break;
                                        case "n":
                                            sb.AppendLine("Deletion has been cancelled");
                                            count2++;
                                            break;
                                        default:
                                            Console.WriteLine("Wrong option, please try again");
                                            break;
                                    }
                                }
                                while (count2 == 0);

                            }
                            else
                            {
                                sb.AppendLine("Customer ID does not exist");
                            }
                        }
                        else
                        {
                            sb.AppendLine("Admin ID not in the correct format, please try again");
                        }
                        Console.WriteLine(sb.ToString());
                        break;
                    case "99":
                        break;
                    default:
                        Console.WriteLine("Please review the menu and Enter your selected menu option");
                        break;
                }
                Console.WriteLine("\nPress any key to continue ...");
                Console.ReadKey();
            }
        }

        public static void AdminOptionThreeSubMenu()
        {
            ProductOptions productOptions = new ProductOptions();
            ProductsRepository productsRepository = new ProductsRepository();
            string exitMenu = "99";
            string menuOption = "";

            while (menuOption != exitMenu)
            {
                Console.Clear();
                Console.WriteLine("\nInstant Order eCommerce System Sub Menu\n==================================================================");
                Person.GetDetails();
                Console.WriteLine("==================================================================");
                Console.WriteLine("1. Display All Products");
                Console.WriteLine("2. Find Product by ID");
                Console.WriteLine("3. Find Product by Name");
                Console.WriteLine("4. Find Product by Type");
                Console.WriteLine("5. Add Product");
                Console.WriteLine("6. Update Product");
                Console.WriteLine("7. Remove Product");
                Console.WriteLine("99. Back\nEnter your selected menu option: ");
                menuOption = Console.ReadLine();


                switch (menuOption)
                {
                    case "1":
                        ShowAllProducts();
                        break;
                    case "2":
                        Console.WriteLine(productOptions.FindProductByID(productsRepository));
                        break;
                    case "3":
                        Console.WriteLine(productOptions.FindProductByName(productsRepository));
                        break;
                    case "4":
                        Console.WriteLine(productOptions.BrowseProductByType(productsRepository));
                        break;
                    case "5":
                        Console.WriteLine(productOptions.AddNewProduct(productsRepository));
                        break;
                    case "6":
                        Console.WriteLine(productOptions.UpdateProduct(productsRepository));
                        break;
                    case "7":
                        Console.WriteLine(productOptions.RemoveProduct(productsRepository));
                        break;
                    case "99":
                        break;
                    default:
                        Console.WriteLine("Please review the menu and Enter your selected menu option");
                        break;
                }
                Console.WriteLine("\nPress any key to continue ...");
                Console.ReadKey();
            }
        }

        public static void AdminOptionFourSubMenu()
        {
            CategoriesRepository categoriesRepository = new CategoriesRepository();
            RepositoryBase<Category> repositoryBase = new CategoriesRepository();
            IRepository<Category> repositoryInterface = new CategoriesRepository();
            CategoryOptions categoryOptions = new CategoryOptions(categoriesRepository, repositoryBase, repositoryInterface);
            
            string exitMenu = "99";
            string menuOption = "";

            while (menuOption != exitMenu)
            {
                Console.Clear();
                Console.WriteLine("\nInstant Order eCommerce System Sub Menu\n==================================================================");
                Person.GetDetails();
                Console.WriteLine("==================================================================");
                Console.WriteLine("1. Display All Categories");
                Console.WriteLine("2. Find Category by ID");
                Console.WriteLine("3. Find Category by Name");
                Console.WriteLine("4. Add Category");
                Console.WriteLine("5. Update Category");
                Console.WriteLine("6. Remove Category");
                Console.WriteLine("99. Back\nEnter your selected menu option: ");
                menuOption = Console.ReadLine();


                switch (menuOption)
                {
                    case "1":
                        ShowAllCategories();
                        break;
                    case "2":
                        Console.WriteLine("Please enter the category ID");
                        int catID = 0;
                        bool result = int.TryParse(Console.ReadLine(), out catID);
                        if (result)
                        {
                            Console.WriteLine(categoryOptions.FindCategoryByID(catID));
                        }
                        else
                        {
                            Console.WriteLine("Please make sure that you insert a number for the id");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Please enter Category name: ");
                        string catName = Console.ReadLine();
                        Console.WriteLine(categoryOptions.FindCategoryByName(catName));
                        break;
                    case "4":
                        Category category = new Category();
                        List<Category> categories = categoriesRepository.ReadGetAllRows();
                        StringBuilder stringBuilder = new StringBuilder();
                        int newID = categories.Max(c => c.CategoryID) + 1;


                        string name = "";
                        while (name.Trim() == "")
                        {
                            Console.WriteLine("Please enter category name: ");
                            name = Console.ReadLine();
                        }


                        category.CategoryID = newID;
                        category.Name = name;

                        int count = 0;
                        do
                        {
                            Console.WriteLine("Are you sure you want to add this new category (Y/N): ");
                            string response = Console.ReadLine();
                            if (response == "y" || response == "Y")
                            {
                                response = response.ToLower();
                            }
                            else if (response == "n" || response == "N")
                            {
                                response = response.ToLower();
                            }

                            switch (response)
                            {
                                case "y":
                                    stringBuilder.AppendLine(categoryOptions.AddNewCategory(category));
                                    count++;
                                    break;
                                case "n":
                                    stringBuilder.AppendLine("Insertion has been cancelled");
                                    count++;
                                    break;
                                default:
                                    Console.WriteLine("Wrong option, please try again");
                                    break;
                            }
                        }
                        while (count == 0);
                        Console.WriteLine(stringBuilder.ToString());
                        break;
                    case "5":
                        StringBuilder stringBuilder2 = new StringBuilder();
                        Console.WriteLine("Please enter a category id:");
                        int categoryID = 0;
                        bool valid = int.TryParse(Console.ReadLine(), out categoryID);
                        if (valid)
                        {
                            Category category2 = new Category();
                            List<Category> categories2 = categoriesRepository.ReadGetAllRows();
                            category2 = categories2.FirstOrDefault(c => c.CategoryID == categoryID);
                            if (category2 != null)
                            {


                                string name2 = "";
                                Console.WriteLine("Please enter category name (Leave blank if you do not want to update): ");
                                name2 = Console.ReadLine();
                                if (name2.Trim() == "")
                                {
                                    name2 = category2.Name;
                                }

                                category2.Name = name2;

                                int count2 = 0;
                                do
                                {
                                    Console.WriteLine("Are you sure you want to update this category (Y/N): ");
                                    string response = Console.ReadLine();
                                    if (response == "y" || response == "Y")
                                    {
                                        response = response.ToLower();
                                    }
                                    else if (response == "n" || response == "N")
                                    {
                                        response = response.ToLower();
                                    }

                                    switch (response)
                                    {
                                        case "y":
                                            stringBuilder2.AppendLine(categoryOptions.UpdateCategory(category2));
                                            count2++;
                                            break;
                                        case "n":
                                            stringBuilder2.AppendLine("Update has been cancelled");
                                            count2++;
                                            break;
                                        default:
                                            Console.WriteLine("Wrong option, please try again");
                                            break;
                                    }
                                }
                                while (count2 == 0);
                            }
                            else
                            {
                                stringBuilder2.AppendLine("Category does not exist");
                            }
                        }
                        else
                        {
                            stringBuilder2.AppendLine("Incorrect format of category ID, please try again");
                        }
                        Console.WriteLine(stringBuilder2.ToString());
                        
                        break;
                    case "6":
                        StringBuilder stringBuilder3 = new StringBuilder();
                        List<Category> categories3 = categoriesRepository.ReadGetAllRows();
                        Console.WriteLine("Please enter the category ID: ");
                        int catId = 0;
                        bool validNum = int.TryParse(Console.ReadLine(), out catId);
                        if (validNum)
                        {
                            bool exists = categoriesRepository.CheckIfIdExists(catId);
                            if (exists)
                            {
                                int count3 = 0;
                                do
                                {
                                    Console.WriteLine("Are you sure you want to remove this category (Y/N): ");
                                    string response = Console.ReadLine();
                                    if (response == "y" || response == "Y")
                                    {
                                        response = response.ToLower();
                                    }
                                    else if (response == "n" || response == "N")
                                    {
                                        response = response.ToLower();
                                    }

                                    switch (response)
                                    {
                                        case "y":
                                            stringBuilder3.AppendLine(categoryOptions.RemoveCategory(catId));
                                            count3++;
                                            break;
                                        case "n":
                                            stringBuilder3.AppendLine("Deletion has been cancelled");
                                            count3++;
                                            break;
                                        default:
                                            Console.WriteLine("Wrong option, please try again");
                                            break;
                                    }
                                }
                                while (count3 == 0);
                                

                            }
                            else
                            {
                                stringBuilder3.AppendLine("Category ID does not exist");
                            }
                        }
                        else
                        {
                            stringBuilder3.AppendLine("Category ID not in the correct format, please try again");
                        }
                        Console.WriteLine(stringBuilder3.ToString());
                        break;
                    case "99":
                        break;
                    default:
                        Console.WriteLine("Please review the menu and Enter your selected menu option");
                        break;
                }
                Console.WriteLine("\nPress any key to continue ...");
                Console.ReadKey();
            }
        }

        public static void AdminOptionFiveSubMenu()
        {
           
            OrdersRepository ordersRepository = new OrdersRepository();
            OrderOptions orderOptions = new OrderOptions(ordersRepository);
            string exitMenu = "99";
            string menuOption = "";

            while (menuOption != exitMenu)
            {
                Console.Clear();
                Console.WriteLine("\nInstant Order eCommerce System Sub Menu\n==================================================================");
                Person.GetDetails();
                Console.WriteLine("==================================================================");
                Console.WriteLine("1. Display All Orders");
                Console.WriteLine("2. Find Order by ID");
                Console.WriteLine("3. Find Order by Date");
                Console.WriteLine("4. Find Order between Dates");
                Console.WriteLine("99. Back\nEnter your selected menu option: ");
                menuOption = Console.ReadLine();


                switch (menuOption)
                {
                    case "1":
                        ShowAllOrders();
                        break;
                    case "2":
                        Console.WriteLine("Please enter the order ID: ");
                        int orderID = 0;
                        bool result = int.TryParse(Console.ReadLine(), out orderID);
                        CultureInfo ci = new CultureInfo("en-za");
                        if (result)
                        {
                            Console.WriteLine(orderOptions.FindOrderByID(orderID));
                        }
                        else
                        {
                            Console.WriteLine("Please make sure that you insert a number for the id");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Please enter order date (dd/MM/yyyy): ");
                        string inputDate = Console.ReadLine();
                        if (inputDate != "")
                        {
                            Console.WriteLine(orderOptions.FindOrdersbyDate(inputDate));
                        }
                        else
                        {
                            Console.WriteLine("No Date entered");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Please enter beginning order date (dd/MM/yyyy): ");
                        string inputDate1 = Console.ReadLine();
                        Console.WriteLine("Please enter ending order date (dd/MM/yyyy): ");
                        string inputDateTwo = Console.ReadLine();
                        if (inputDate1 != "" && inputDateTwo != "")
                        {
                            Console.WriteLine(orderOptions.FindOrdersbyBetweenDates(inputDate1, inputDateTwo));
                        }
                        else
                        {
                            Console.WriteLine("Please enter both beginning and end dates");
                        }
                        break;
                    case "99":
                        break;
                    default:
                        Console.WriteLine("Please review the menu and Enter your selected menu option");
                        break;
                }
                Console.WriteLine("\nPress any key to continue ...");
                Console.ReadKey();
            }
        }

        public static void AdminOptionSevenSubMenu()
        {
            
            OrderDetailsRepository orderDetailsRepository = new OrderDetailsRepository();
            OrdersRepository ordersRepository = new OrdersRepository();
            OrderDetailOptions orderDetailOptions = new OrderDetailOptions(orderDetailsRepository, ordersRepository);
            string exitMenu = "99";
            string menuOption = "";

            while (menuOption != exitMenu)
            {
                Console.Clear();
                Console.WriteLine("\nInstant Order eCommerce System Sub Menu\n==================================================================");
                Person.GetDetails();
                Console.WriteLine("==================================================================");
                Console.WriteLine("1. Display All Order Details");
                Console.WriteLine("2. Find Order by ID");
                Console.WriteLine("99. Back\nEnter your selected menu option: ");
                menuOption = Console.ReadLine();


                switch (menuOption)
                {
                    case "1":
                        ShowAllOrderDetails();
                        break;
                    case "2":
                        Console.WriteLine("Please enter the order ID: ");
                        int orderID = 0;
                        bool result = int.TryParse(Console.ReadLine(), out orderID);
                        if (result)
                        {
                            Console.WriteLine(orderDetailOptions.FindOrderDetailByOrderID(orderID));
                        }
                        else
                        {
                            Console.WriteLine("Please make sure that you insert a number for the id");
                        }
                        break;
                    case "99":
                        break;
                    default:
                        Console.WriteLine("Please review the menu and Enter your selected menu option");
                        break;
                }
                Console.WriteLine("\nPress any key to continue ...");
                Console.ReadKey();
            }
        }

        public static void AdminOptionEightSubMenu()
        {
            PaymentOptions paymentOptions = new PaymentOptions();
            PaymentsRepository paymentsRepository = new PaymentsRepository();
            string exitMenu = "99";
            string menuOption = "";

            while (menuOption != exitMenu)
            {
                Console.Clear();
                Console.WriteLine("\nInstant Order eCommerce System Sub Menu\n==================================================================");
                Person.GetDetails();
                Console.WriteLine("==================================================================");
                Console.WriteLine("1. Display All Payments");
                Console.WriteLine("2. Find Payment by Date");
                Console.WriteLine("3. Find Payment between Dates");
                Console.WriteLine("4. Find Payment by Method");
                Console.WriteLine("5. Find Payment by Status");
                Console.WriteLine("99. Back\nEnter your selected menu option: ");
                menuOption = Console.ReadLine();


                switch (menuOption)
                {
                    case "1":
                        ShowAllPayments();
                        break;
                    case "2":
                        Console.WriteLine(paymentOptions.FindPaymentsbyDate(paymentsRepository));
                        break;
                    case "3":
                        Console.WriteLine(paymentOptions.FindPaymentsbyBetweenDates(paymentsRepository));
                        break;
                    case "4":
                        Console.WriteLine(paymentOptions.FindPaymentsByMethod(paymentsRepository));
                        break;
                    case "5":
                        Console.WriteLine(paymentOptions.FindPaymentsByStatus(paymentsRepository));
                        break;
                    case "99":
                        break;
                    default:
                        Console.WriteLine("Please review the menu and Enter your selected menu option");
                        break;
                }
                Console.WriteLine("\nPress any key to continue ...");
                Console.ReadKey();
            }
        }

        public static void AdminOptionNineSubMenu()
        {
            ShippingOptions shippingOptions = new ShippingOptions();
            ShippingsRepository shippingsRepository = new ShippingsRepository();
            string exitMenu = "99";
            string menuOption = "";

            while (menuOption != exitMenu)
            {
                Console.Clear();
                Console.WriteLine("\nInstant Order eCommerce System Sub Menu\n==================================================================");
                Person.GetDetails();
                Console.WriteLine("==================================================================");
                Console.WriteLine("1. Display All Shippings");
                Console.WriteLine("2. Find Shipping by Tracking Number");
                Console.WriteLine("3. Find Shipping by Method");
                Console.WriteLine("4. Find Shipping by Date");
                Console.WriteLine("5. Find Shipping between Dates");
                Console.WriteLine("99. Back\nEnter your selected menu option: ");
                menuOption = Console.ReadLine();


                switch (menuOption)
                {
                    case "1":
                        ShowAllShippings();
                        break;
                    case "2":
                        Console.WriteLine(shippingOptions.FindShippingbyTrackingNumber(shippingsRepository));
                        break;
                    case "3":
                        Console.WriteLine(shippingOptions.FindShippingbyMethod(shippingsRepository));
                        break;
                    case "4":
                        Console.WriteLine(shippingOptions.FindShippingbyDate(shippingsRepository));
                        break;
                    case "5":
                        Console.WriteLine(shippingOptions.FindShippingbetweenDates(shippingsRepository));
                        break;
                    case "99":
                        break;
                    default:
                        Console.WriteLine("Please review the menu and Enter your selected menu option");
                        break;
                }
                Console.WriteLine("\nPress any key to continue ...");
                Console.ReadKey();
            }
        }

        public static void AdminOptionTenSubMenu()
        {
            ReviewOptions reviewOptions = new ReviewOptions();
            ReviewsRepository reviewsRepository = new ReviewsRepository();
            string exitMenu = "99";
            string menuOption = "";

            while (menuOption != exitMenu)
            {
                Console.Clear();
                Console.WriteLine("\nInstant Order eCommerce System Sub Menu\n==================================================================");
                Person.GetDetails();
                Console.WriteLine("==================================================================");
                Console.WriteLine("1. Display All Reviews");
                Console.WriteLine("2. Find Reviews by Rating");
                Console.WriteLine("3. Find Review by Date");
                Console.WriteLine("4. Find Review between dates");
                Console.WriteLine("99. Back\nEnter your selected menu option: ");
                menuOption = Console.ReadLine();


                switch (menuOption)
                {
                    case "1":
                        ShowAllReviews();
                        break;
                    case "2":
                        Console.WriteLine(reviewOptions.FindReviewbyRating(reviewsRepository));
                        break;
                    case "3":
                        Console.WriteLine(reviewOptions.FindReviewbyDate(reviewsRepository));
                        break;
                    case "4":
                        Console.WriteLine(reviewOptions.FindReviewbetweenDates(reviewsRepository));
                        break;
                    case "99":
                        break;
                    default:
                        Console.WriteLine("Please review the menu and Enter your selected menu option");
                        break;
                }
                Console.WriteLine("\nPress any key to continue ...");
                Console.ReadKey();
            }
        }

        public static void AdminOptionElevenSubMenu()
        {
            WishlistOptions wishlistOptions = new WishlistOptions();
            WishlistsRepository wishlistsRepository = new WishlistsRepository();
            ProductsRepository productsRepository = new ProductsRepository();
            string exitMenu = "99";
            string menuOption = "";

            while (menuOption != exitMenu)
            {
                Console.Clear();
                Console.WriteLine("\nInstant Order eCommerce System Sub Menu\n==================================================================");
                Person.GetDetails();
                Console.WriteLine("==================================================================");
                Console.WriteLine("1. Display All Wishlists");
                Console.WriteLine("2. Find Wishlist by Customer ID");
                Console.WriteLine("3. Find Wishlist by Date");
                Console.WriteLine("4. Find Wishlist between Dates");
                Console.WriteLine("99. Back\nEnter your selected menu option: ");
                menuOption = Console.ReadLine();


                switch (menuOption)
                {
                    case "1":
                        ShowAllWishlists();
                        break;
                    case "2":
                        Console.WriteLine(wishlistOptions.FindWishlistbyCusID(wishlistsRepository, productsRepository));
                        break;
                    case "3":
                        Console.WriteLine(wishlistOptions.FindWishlistbyDate(wishlistsRepository, productsRepository));
                        break;
                    case "4":
                        Console.WriteLine(wishlistOptions.FindWishlistbetweenDates(wishlistsRepository, productsRepository));
                        break;
                    case "99":
                        break;
                    default:
                        Console.WriteLine("Please review the menu and Enter your selected menu option");
                        break;
                }
                Console.WriteLine("\nPress any key to continue ...");
                Console.ReadKey();
            }
        }

        static void ShowAllAdmins()
        {
            AdminsRepository repository = new AdminsRepository();
            List<Admin> allOfTheAdmins = repository.ReadGetAllRows();

            Console.WriteLine("\n Option 1 - All Admins");
            allOfTheAdmins.ForEach(b => Console.WriteLine($"ID: {b.AdminID}, First Name: {b.FirstName}, Surname: {b.Surname}, Email: {b.Email}, Username: {b.Username}, Address: {b.Address}, Phone Number: {b.PhoneNumber}"));
        }

        static void ShowAllCustomers()
        {
            Console.WriteLine("\nOption 2 - All Customers");
            CustomersRepository repository = new CustomersRepository();
            List<Customer> allOfTheCustomers = repository.ReadGetAllRows();
            allOfTheCustomers.ForEach(b => Console.WriteLine($"ID: {b.CustomerID}, First Name: {b.FirstName}, Surname: {b.Surname}, Email: {b.Email}, Username: {b.Username}, Address: {b.Address}, Phone Number: {b.PhoneNumber}"));
        }

        static void ShowAllProducts()
        {
            CultureInfo ci = new CultureInfo("en-za");
            ProductsRepository repository = new ProductsRepository();
            List<Product> allOfTheProducts = repository.ReadGetAllRows();
            Console.WriteLine("\nOption 3 - All Products");
            allOfTheProducts.ForEach(b => Console.WriteLine($"ID: {b.ProductID}, Name: {b.Name}, Brand: {b.Brand}, Description: {b.Description}, Type: {b.Type}, Price: {b.Price.ToString("C", ci)}, Quantity: {b.StockQuantity}, Added Date: {b.ModifiedDate.ToString("dd MMMM yyyy HH:mm")}"));
        }

        static void ShowAllCategories()
        {
            CategoriesRepository repository = new CategoriesRepository();
            List<Category> allOfTheCategories = repository.ReadGetAllRows();
            Console.WriteLine("\nOption 4 - All Categories");
            allOfTheCategories.ForEach(b => Console.WriteLine($"ID: {b.CategoryID}, Name: {b.Name}"));
        }

        static void ShowAllOrders()
        {
            CustomersRepository cusRepository = new CustomersRepository();
            List<Customer> allOfTheCustomers = cusRepository.ReadGetAllRows();

            CultureInfo ci = new CultureInfo("en-za");
            OrdersRepository repository = new OrdersRepository();
            List<Order> allOfTheOrders = repository.ReadGetAllRows();
            Console.WriteLine("\n Option 5 - All Orders");
            allOfTheOrders.ForEach(b => Console.WriteLine($"ID: {b.OrderID}, Customer Name: {allOfTheCustomers.FirstOrDefault(z => z.CustomerID == b.CustomerID).FirstName + " " + allOfTheCustomers.FirstOrDefault(z => z.CustomerID == b.CustomerID).Surname}, Order Date: {b.OrderDate.ToString("dd MMMM yyyy HH:mm")}, Total Amount: {b.TotalAmount.ToString("C", ci)}"));
        }

        static void ShowAllOrderDetails()
        {
            ProductsRepository prodRepository = new ProductsRepository();
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();

            CultureInfo ci = new CultureInfo("en-za");
            OrderDetailsRepository repository = new OrderDetailsRepository();
            List<OrderDetail> allOfTheOrderDetails = repository.ReadGetAllRows();
            Console.WriteLine("\n Option 7 - All Order Details");
            allOfTheOrderDetails.ForEach(b => Console.WriteLine($"ID: {b.OrderDetailID}, Product Name: {allOfTheProducts.FirstOrDefault(z => z.ProductID == b.ProductID).Name}, Quantity: {b.Quantity}, Unit Price: {b.UnitPrice.ToString("C", ci)}"));
        }

        static void ShowAllPayments()
        {
            CultureInfo ci = new CultureInfo("en-za");
            PaymentsRepository repository = new PaymentsRepository();
            List<Payment> allOfThePayments = repository.ReadGetAllRows();
            Console.WriteLine("\n Option 8 - All Payments");
            allOfThePayments.ForEach(b => Console.WriteLine($"ID: {b.PaymentID}, Payment Date: {b.PaymentDate.ToString("dd MMMM yyyy HH:mm")}, Payment Method: {b.PaymentMethod}, Amount: {b.Amount}, Status: {b.Status}"));
        }

        static void ShowAllShippings()
        {
            ShippingsRepository repository = new ShippingsRepository();
            List<Shipping> allOfTheShippings = repository.ReadGetAllRows();
            Console.WriteLine("\n Option 9 - All Shippings");
            allOfTheShippings.ForEach(b => Console.WriteLine($"ID: {b.ShippingID}, Shipping Date: {b.ShipDate.ToString("dd MMMM yyyy HH:mm")}, Shipping Method: {b.ShipMethod}, Tracking Number: {b.TrackingNumber}, Delivery Status: {b.DeliveryStatus}"));
        }

        static void ShowAllReviews()
        {
            ReviewsRepository repository = new ReviewsRepository();
            List<Review> allOfTheReviews = repository.ReadGetAllRows();
            Console.WriteLine("\n Option 10 - All Reviews");
            allOfTheReviews.ForEach(b => Console.WriteLine($"ID: {b.ReviewID}, Rating: {b.Rating}, Title: {b.Title}, Comment: {b.Comment}, Review Date: {b.ReviewDate.ToString("dd MMMM yyyy HH:mm")}"));
        }

        static void ShowAllWishlists()
        {
            WishlistsRepository repository = new WishlistsRepository();
            List<Wishlist> allOfTheWishlists = repository.ReadGetAllRows();

            ProductsRepository prodRepository = new ProductsRepository();
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();
            Console.WriteLine("\n Option 11 - All Wishlists");
            allOfTheWishlists.ForEach(b => Console.WriteLine($"ID: {b.WishlistID}, Product Name: {allOfTheProducts.FirstOrDefault(z => z.ProductID == b.ProductID).Name}, Added Date: {b.AddedDate.ToString("dd MMMM yyyy HH:mm")}"));
        }
    }
}
