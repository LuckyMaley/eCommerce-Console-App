using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public long ProductID { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }

        public static List<OrderDetail> OrderDetailsDataSet = new List<OrderDetail>()
        {
            new OrderDetail {OrderDetailID=1, OrderID=1, ProductID=1, Quantity=1, UnitPrice=2100.99},
            new OrderDetail {OrderDetailID=2, OrderID=2, ProductID=2, Quantity=1, UnitPrice=2700.99},
            new OrderDetail {OrderDetailID=3, OrderID=3, ProductID=3, Quantity=1, UnitPrice=900.99},
            new OrderDetail {OrderDetailID=4, OrderID=4, ProductID=4, Quantity=1, UnitPrice=1000.99},
            new OrderDetail {OrderDetailID=5, OrderID=5, ProductID=5, Quantity=1, UnitPrice=799.99},
            new OrderDetail {OrderDetailID=6, OrderID=6, ProductID=6, Quantity=1, UnitPrice=800.99},
            new OrderDetail {OrderDetailID=7, OrderID=7, ProductID=7, Quantity=1, UnitPrice=200.99},
            new OrderDetail {OrderDetailID=8, OrderID=8, ProductID=8, Quantity=1, UnitPrice=2200.99},
            new OrderDetail {OrderDetailID=9, OrderID=9, ProductID=99875425, Quantity=1, UnitPrice=1100.99},
            new OrderDetail {OrderDetailID=10, OrderID=10, ProductID=10000000, Quantity=1, UnitPrice=2700.99}
        };
    }
}
