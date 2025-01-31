using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int ShippingID { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }

        public static List<Order> OrdersDataSet = new List<Order>()
        {
            new Order() {OrderID=1, CustomerID=1,  ShippingID=1, OrderDate=new DateTime(2023,11,12,12,45,00), TotalAmount=2100.99},
            new Order() {OrderID=2, CustomerID=2, ShippingID=2, OrderDate=new DateTime(2017,4,22,15,35,10), TotalAmount=2700.99},
            new Order() {OrderID = 3, CustomerID = 3, ShippingID = 3, OrderDate = new DateTime(2019, 11, 12, 12, 45, 00), TotalAmount = 900.99},
            new Order() {OrderID = 4, CustomerID = 4, ShippingID = 4, OrderDate = new DateTime(2018, 5, 12, 12, 45, 00), TotalAmount = 1000.99},
            new Order() {OrderID = 5, CustomerID = 5, ShippingID = 5, OrderDate = new DateTime(2018, 3, 21  , 21, 47, 50), TotalAmount = 799.99},
            new Order() {OrderID = 6, CustomerID = 6, ShippingID = 6, OrderDate = new DateTime(2019, 10, 8, 8, 5, 40), TotalAmount = 800.99},
            new Order() {OrderID = 7, CustomerID = 7, ShippingID = 7, OrderDate = new DateTime(2020, 2, 13, 13, 35, 00), TotalAmount = 200.99},
            new Order() {OrderID = 8, CustomerID = 8, ShippingID = 8, OrderDate = new DateTime(2021, 12, 12, 12, 45, 00), TotalAmount = 2200.99},
            new Order() {OrderID = 9, CustomerID = 9, ShippingID = 9, OrderDate = new DateTime(2022, 12, 10, 10, 40, 00), TotalAmount = 1100.99},
            new Order() {OrderID = 10, CustomerID = 10, ShippingID = 10, OrderDate = new DateTime(2020, 3, 16, 16, 5, 20), TotalAmount = 2700.99}
        };
    }
}
