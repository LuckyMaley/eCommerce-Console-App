using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Models
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

        public static List<Admin> AdminsDataSet = new List<Admin>()
        {
            new Admin{AdminID = 1, FirstName="Zandile", Surname ="Zimela", Email="Zzimela@gmail.com", Username="Zzimela", Address="18 Jack avenue, 2001", PhoneNumber = "0743244345", Role="Admin"},
            new Admin{AdminID = 2, FirstName="Andile", Surname ="Zuma", Email="Azuma22@gmail.com", Username="Azuma22", Address="2003 Field street, 4001", PhoneNumber = "0844544278", Role="Admin"},
            new Admin{AdminID = 3, FirstName="Jack", Surname ="Harrison", Email="Harrionj01@gmail.com", Username="Harrison01", Address="230 West street, 4001", PhoneNumber = "0675647385", Role="Admin"},
            new Admin{AdminID = 4, FirstName="Hannah", Surname ="Sancho", Email="Sanchoh@gmail.com", Username="Sanchoh", Address="15 Zimbali avenue, 4501", PhoneNumber = "0673004545", Role="Admin"},
            new Admin{AdminID = 5, FirstName="Zack", Surname ="Wake", Email="Wakez007@gmail.com", Username="Wakez007", Address="28 Sandton avenue, 2001", PhoneNumber = "0745667386", Role="Admin"},
            new Admin{AdminID = 6, FirstName="Bruno", Surname ="Sterling", Email="Bruno22@gmail.com", Username="Brunos22", Address="20 King Edwards avenue, 3201", PhoneNumber = "0843778347", Role="Admin"},
            new Admin{AdminID = 7, FirstName="Luis", Surname ="Diaz", Email="Luisd7@gmail.com", Username="Luis7", Address="19 Jack avenue, 2001", PhoneNumber = "0742687829", Role="Admin"},
            new Admin{AdminID = 8, FirstName="Cristiano", Surname ="Walker", Email="Walkerc07@gmail.com", Username="Walkerc07", Address="15 Harrison avenue, 3001", PhoneNumber = "0743264748", Role="Admin"},
            new Admin{AdminID = 9, FirstName="Phil", Surname ="Foden", Email="Fodenp47@gmail.com", Username="Fodenp47", Address="100 Everton street, 6001", PhoneNumber = "0746277885", Role="Admin"},
            new Admin{AdminID = 10, FirstName="Kylian", Surname ="Carrick", Email="Kcarrick@gmail.com", Username="Kcarrick", Address="20 Jack avenue, 2001", PhoneNumber = "0675249849", Role="Admin"}
        };

    }
}
