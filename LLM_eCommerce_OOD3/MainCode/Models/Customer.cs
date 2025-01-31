using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public static List<Customer> CustomersDataSet = new List<Customer>()
        {
            new Customer{CustomerID = 1, FirstName="Zack", Surname ="Efron", Email="Efronz@gmail.com", Username="Efronz", Address="15 Zuma avenue, 2001", PhoneNumber = "074396748"},
            new Customer{CustomerID = 2, FirstName="Jacob", Surname ="Zuma", Email="Jzuma11@gmail.com", Username="Jzuma11", Address="13 Commercial street, 4001", PhoneNumber = "0670004175"},
            new Customer{CustomerID = 3, FirstName="Nelson", Surname ="Mandela", Email="Nelsonm01@gmail.com", Username="Nelsonm01", Address="300 West street, 4001", PhoneNumber = "0675540000"},
            new Customer{CustomerID = 4, FirstName="Jadon", Surname ="Sancho", Email="Sanchoj@gmail.com", Username="Sanchoj", Address="13 Zimbali avenue, 4501", PhoneNumber = "0677008594"},
            new Customer{CustomerID = 5, FirstName="Julian", Surname ="Alvarez", Email="Jalvarez007@gmail.com", Username="Jalvarez007", Address="51 Sandton avenue, 2001", PhoneNumber = "0848767080"},
            new Customer{CustomerID = 6, FirstName="Eden", Surname ="Hazard", Email="Hazard10@gmail.com", Username="Hazard10", Address="10 King Edwards avenue, 3201", PhoneNumber = "0840004365"},
            new Customer{CustomerID = 7, FirstName="Tammy", Surname ="Abraham", Email="Abrahamt@gmail.com", Username="Abrahamt", Address="16 Allison avenue, 2001", PhoneNumber = "0742605859"},
            new Customer{CustomerID = 8, FirstName="Sanele", Surname ="Mbatha", Email="Mbathas07@gmail.com", Username="Mbathas07", Address="22 Zakhele avenue, 3001", PhoneNumber = "0744254040"},
            new Customer{CustomerID = 9, FirstName="Sinokuhle", Surname ="Gumede", Email="Gumedes01@gmail.com", Username="Gumedes01", Address="100 Marchester street, 6001", PhoneNumber = "0749457005"},
            new Customer{CustomerID = 10, FirstName="Amanda", Surname ="Nzama", Email="Amandan01@gmail.com", Username="Amandan", Address="20 Zuma avenue, 2001", PhoneNumber = "0676709940"}

        };
    }
}
