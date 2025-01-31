using MainCode.Models;
using MainCode.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode
{
    public class MainCodeStaticObjects
    {

        public static Dictionary<long, Product> products = new Dictionary<long, Product>();

        public static ShoppingCart cart = new ShoppingCart();

        public struct Person
        {
            private static int id;
            private static string firstName;
            private static string surname;
            private static string username;

            public static int Id
            {
                get { return id; }
                set { id = value; }
            }

            public static string FirstName
            {
                get { return firstName; }
                set { firstName = value; }
            }

            public static string Surname
            {
                get { return surname; }
                set { surname = value; }
            }

            public static string UserNname
            {
                get { return username; }
                set { username = value; }
            }

            public Person(int id, string fName, string lName, string userName)
            {
                Id = id;
                FirstName = fName;
                Surname = lName;
                UserNname = userName;
            }

            public static void GetDetails() => Console.WriteLine($"User Logged in: {FirstName} {Surname}");
        }

        public static Person person;
    }
}
