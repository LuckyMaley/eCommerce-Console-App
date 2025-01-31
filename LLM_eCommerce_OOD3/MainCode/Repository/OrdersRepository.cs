using MainCode.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository
{
    public class OrdersRepository
    {
        private static List<Order> allOrders;

        public OrdersRepository()
        {
            allOrders = ReadGetAllRows();
        }

        public virtual List<Order> ReadGetAllRows()
        {
            allOrders = new List<Order>();
            foreach (var ord in Order.OrdersDataSet)
            {
                allOrders.Add(new Order()
                {
                    OrderID = ord.OrderID,
                    CustomerID = ord.CustomerID,
                    ShippingID = ord.ShippingID,
                    OrderDate = ord.OrderDate,
                    TotalAmount = ord.TotalAmount
                });
            }
            return allOrders;
        }

        public virtual bool CheckIfIdExists(int id)
        {
            OrdersRepository repository = new OrdersRepository();
            List<Order> allOfTheOrders = repository.ReadGetAllRows();
            int count = 0;
            foreach (var cat in allOfTheOrders)
            {
                if (cat.OrderID == id)
                {
                    count++;
                }
            }
            bool result = count > 0 ? true : false;
            return result;
        }

        public virtual Order ReadRowByID(int id)
        {
            OrdersRepository repository = new OrdersRepository();
            List<Order> allOfTheOrders = repository.ReadGetAllRows();
            Order order = allOfTheOrders.FirstOrDefault(c => c.OrderID == id);
            return order;
        }

        public virtual List<Order> ReadRowByDate(string date)
        {
            OrdersRepository repository = new OrdersRepository();
            List<Order> allOfTheOrders = repository.ReadGetAllRows();
            
            string defaultDateString = "10/08/2008";
            string format = "dd/MM/yyyy";
            CultureInfo ci = new CultureInfo("en-za");

            DateTime dateTime;
            if (!DateTime.TryParseExact(date, format, ci, System.Globalization.DateTimeStyles.None, out dateTime))
            {
                dateTime = DateTime.ParseExact(defaultDateString, format, ci);
            }
            IEnumerable<Order> order = allOfTheOrders.Where(c => c.OrderDate.Date == dateTime.Date);
            List<Order> orders = new List<Order>();
            if(order != null)
            {
                orders = order.ToList();
            }
            else
            {
                orders = null;
            }
            return orders;
        }

        public virtual List<Order> ReadRowByDate(string beginDate, string endDate)
        {
            OrdersRepository repository = new OrdersRepository();
            List<Order> allOfTheOrders = repository.ReadGetAllRows();

            string defaultDateString = "10/08/2008";
            string format = "dd/MM/yyyy";
            CultureInfo ci = new CultureInfo("en-za");

            DateTime beginDateTime;
            if (!DateTime.TryParseExact(beginDate, format, ci, System.Globalization.DateTimeStyles.None, out beginDateTime))
            {
                beginDateTime = DateTime.ParseExact(defaultDateString, format, ci);
            }

            DateTime endDateTime;
            if (!DateTime.TryParseExact(endDate, format, ci, System.Globalization.DateTimeStyles.None, out endDateTime))
            {
                endDateTime = DateTime.ParseExact(defaultDateString, format, ci);
            }
            IEnumerable<Order> order = allOfTheOrders.Where(c => c.OrderDate.Date >= beginDateTime.Date && c.OrderDate.Date <= endDateTime.Date);
            List<Order> orders = new List<Order>();
            if (order != null)
            {
                orders = order.ToList();
            }
            else
            {
                orders = null;
            }
            return orders;
        }
    }
}
