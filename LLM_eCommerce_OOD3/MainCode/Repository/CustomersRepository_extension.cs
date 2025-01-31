using MainCode.Models;
using static MainCode.MainCodeStaticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace MainCode.Repository
{
    public partial class CustomersRepository
    {
        public bool Login(string username)
        {
            CustomersRepository customersRepository = new CustomersRepository();
            var customer = customersRepository.ReadGetAllRows().FirstOrDefault(c => c.Username == username);
            bool valid = customer != null ? true : false;
            return valid;
        }

        public void Login()
        {
            char exitMenu = 'b';
            char menuOption = ' ';
            int count = 0;
            string userName = "";
            while (menuOption != exitMenu)
            {
                Console.Clear();
                Console.WriteLine("\nInstant Order eCommerce System Main Menu\n==================================================================");
                Console.WriteLine("Enter your username (b to go to Main Menu): ");
               userName = Console.ReadLine();

                if (userName == "X" || userName == "x")
                {
                    menuOption = 'x';
                }
                bool checkLogin = Login(userName);

                if (checkLogin)
                {
                    menuOption = '1';
                }
                else if( userName == "b" || userName == "B")
                {
                    menuOption = 'b';
                }
                
                switch (menuOption)
                {
                    case '1':
                        count++;
                        menuOption = 'b';
                        break;
                    case 'b':
                        break;
                    default:
                        Console.WriteLine("User does not exist, please try again");
                        Console.WriteLine("\nPress any key to continue ...");
                        Console.ReadKey();
                        break;
                }
            }
            if(count == 1)
            {
                var customer = allCustomers.FirstOrDefault(c => c.Username == userName);
                person = new Person(customer.CustomerID, customer.FirstName, customer.Surname, customer.Username);
                MenuOptions.CustomerMenu();
            }
        }


        public virtual partial List<Customer> ReadGetAllRows()
        {
            allCustomers = new List<Customer>();
            foreach (var cus in Customer.CustomersDataSet)
            {
                allCustomers.Add(new Customer()
                {
                    CustomerID = cus.CustomerID,
                    FirstName = cus.FirstName,
                    Surname = cus.Surname,
                    Email = cus.Email,
                    Username = cus.Username,
                    Address = cus.Address,
                    PhoneNumber = cus.PhoneNumber
                });
            }
            return allCustomers;
        }
        public override bool UpdateEntity(Customer entity)
        {
            bool returnVal = false;
            try
            {
                var customer = Customer.CustomersDataSet.FirstOrDefault(c => c.CustomerID == entity.CustomerID);
                customer.CustomerID = entity.CustomerID;
                customer.FirstName = entity.FirstName;
                customer.Surname = entity.Surname;
                customer.Email = entity.Email;
                customer.Username = entity.Username;
                customer.Address = entity.Address;
                customer.PhoneNumber = entity.PhoneNumber;
                returnVal = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error, cannot update customer" + ex.ToString());
            }

            return returnVal;
        }

        public virtual Queue<Customer> GetCustomerByName(string name)
        {
            CustomersRepository customersRepository = new CustomersRepository();
            var customerlist = customersRepository.ReadGetAllRows();
            var customers = new Queue<Customer>();
            foreach (var customer in customerlist.Where(c => c.FirstName == name)) 
            {
                customers.Enqueue(customer);
            }
            return customers;
        }

        public virtual Queue<Customer> GetCustomerByName(string name, string surName)
        {
            CustomersRepository customersRepository = new CustomersRepository();
            var customerlist = customersRepository.ReadGetAllRows();
            var customers = new Queue<Customer>();
            foreach (var customer in customerlist.Where(c => c.FirstName == name && c.Surname == surName))
            {
                customers.Enqueue(customer);
            }
            return customers;
        }
    }
}
