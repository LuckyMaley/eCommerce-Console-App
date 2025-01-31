using MainCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository
{
    public class OrderDetailsRepository
    {
        private static List<OrderDetail> allOrderDetails;

        public OrderDetailsRepository()
        {
            allOrderDetails = ReadGetAllRows();
        }

        public virtual List<OrderDetail> ReadGetAllRows()
        {
            allOrderDetails = new List<OrderDetail>();
            foreach (var ordd in OrderDetail.OrderDetailsDataSet)
            {
                allOrderDetails.Add(new OrderDetail()
                {
                    OrderDetailID = ordd.OrderDetailID,
                    OrderID = ordd.OrderID,
                    ProductID = ordd.ProductID,
                    Quantity = ordd.Quantity,
                    UnitPrice = ordd.UnitPrice
                });
            }
            return allOrderDetails;
        }

        public virtual List<OrderDetail> ReadRowByID(int id)
        {
            OrderDetailsRepository repository = new OrderDetailsRepository();
            List<OrderDetail> allOfTheOrderDetails = repository.ReadGetAllRows();
            IEnumerable<OrderDetail> orderDetail = allOfTheOrderDetails.Where(c => c.OrderID == id);
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            if (orderDetail != null)
            {
                orderDetails = orderDetail.ToList();
            }
            else
            {
                orderDetails = null;
            }
            return orderDetails;
        }
    }
}
