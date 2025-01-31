using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        
        public int OrderID { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }

        public static List<Payment> PaymentsDataSet = new List<Payment>()
        {
            new Payment{PaymentID=1, OrderID=1, PaymentDate=new DateTime(2023,11,12,12,55,00), PaymentMethod="Card", Amount=2100.99, Status="Paid"},
            new Payment{PaymentID=2, OrderID=2, PaymentDate=new DateTime(2017,4,22,15,45,10), PaymentMethod="Card", Amount=2700.99, Status="Paid"},
            new Payment{PaymentID=3, OrderID = 3, PaymentDate=new DateTime(2019,11,12,12,55,00), PaymentMethod="Card", Amount=900.99, Status = "Paid"},
            new Payment{PaymentID=4, OrderID = 4, PaymentDate=new DateTime(2018,5,12,12,55,00), PaymentMethod="Card", Amount=1000.99, Status = "Paid"},
            new Payment{PaymentID=5, OrderID = 5, PaymentDate=new DateTime(2018,3,12,21,57,50), PaymentMethod="Card", Amount=799.99, Status = "Paid"},
            new Payment{PaymentID=6, OrderID = 6, PaymentDate=new DateTime(2019,10,13,8,6,40), PaymentMethod="Card", Amount=800.99, Status = "Paid"},
            new Payment{PaymentID=7, OrderID = 7, PaymentDate=new DateTime(2020,2,2,13,45,00), PaymentMethod="Card", Amount=200.99, Status = "Paid"},
            new Payment{PaymentID=8, OrderID = 8, PaymentDate=new DateTime(2021,12,12,12,55,00), PaymentMethod="Card", Amount=2200.99, Status = "Paid"},
            new Payment{PaymentID=9, OrderID = 9, PaymentDate=new DateTime(2022,12,10,10,50,00), PaymentMethod="Card", Amount=1100.99, Status = "Paid"},
            new Payment{PaymentID=10, OrderID = 10, PaymentDate=new DateTime(2020,3,22,16,10,20), PaymentMethod="Card", Amount=2700.99, Status = "Paid"}
        };
    }
}
