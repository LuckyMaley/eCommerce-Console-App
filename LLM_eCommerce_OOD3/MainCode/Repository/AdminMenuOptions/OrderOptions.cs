using MainCode.Models;
using MainCode.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository.AdminMenuOptions
{
    /// <summary>
    /// This OrderOptions class consists of methods called when a user clicks a menu option in the front-end.
    /// These methods interact with the Repository to extract or remove data from the Repository.
    /// </summary>
    /// <remarks>
    /// 
    /// public OrderOptions(OrdersRepository _ordersRepository)
    /// A counstructor that initializes the OrderOptions Class.
    /// These parameters are used for injection into the class for mock testing.
    /// <param name="_ordersRepository">The orders repository.</param>
    /// 
    /// public string FindOrderByID(int orderID)
    /// Finds an order by ID and returns the order details as a string.
    /// <param name="orderID">The ID of the order to find.</param>
    /// <returns>A string containing the order details if found, or a message indicating that the order does not exist.</returns>
    /// 
    /// public string FindOrdersbyDate(string inputDate)
    /// Finds an order by date and returns the order details as a string.
    /// <param name="inputDate">The date of the order to find.</param>
    /// <returns>A string containing the order details if found, or a message indicating that the order does not exist.</returns>
    /// 
    /// public string FindOrdersbyBetweenDates(string inputDate, string inputDateTwo)
    /// Finds an order by between dates and returns the order details as a string.
    /// <param name="inputDate">The beginning date of the order to find.</param>
    /// <param name="inputDateTwo">The end date of the order to find.</param>
    /// <returns>A string containing the order details if found, or a message indicating that the order does not exist.</returns>
    /// </remarks>

    public class OrderOptions
    {
        OrdersRepository ordersRepository;

        public OrderOptions(OrdersRepository _ordersRepository)
        {
            ordersRepository = _ordersRepository;
        }

        public string FindOrderByID(int orderID)
        {
            StringBuilder stringBuilder = new StringBuilder();
            CustomersRepository cusRepository = new CustomersRepository();
            List<Customer> allOfTheCustomers = cusRepository.ReadGetAllRows();
            CultureInfo ci = new CultureInfo("en-za");
            
            bool valid = ordersRepository.CheckIfIdExists(orderID);
            if (valid)
            {
                var order = ordersRepository.ReadRowByID(orderID);
                stringBuilder.AppendLine($"ID: {order.OrderID}, Customer Name: {allOfTheCustomers.FirstOrDefault(z => z.CustomerID == order.CustomerID).FirstName + " " + allOfTheCustomers.FirstOrDefault(z => z.CustomerID == order.CustomerID).Surname}, Order Date: {order.OrderDate.ToString("dd MMMM yyyy HH:mm")}, Total Amount: {order.TotalAmount.ToString("C", ci)}");

            }
            else
            {
                stringBuilder.AppendLine("order does not exist, Please try again");
            }
            
            return stringBuilder.ToString();
        }

        public string FindOrdersbyDate(string inputDate)
        {
            
            StringBuilder stringBuilder = new StringBuilder();
            if (inputDate != "")
            {
                CustomersRepository cusRepository = new CustomersRepository();
                List<Customer> allOfTheCustomers = cusRepository.ReadGetAllRows();
                CultureInfo ci = new CultureInfo("en-za");
                List<Order> orders = ordersRepository.ReadRowByDate(inputDate);
                if (orders.Count > 0)
                {
                    orders.ForEach(b => stringBuilder.AppendLine($"ID: {b.OrderID}, Customer Name: {allOfTheCustomers.FirstOrDefault(z => z.CustomerID == b.CustomerID).FirstName + " " + allOfTheCustomers.FirstOrDefault(z => z.CustomerID == b.CustomerID).Surname}, Order Date: {b.OrderDate.ToString("dd MMMM yyyy HH:mm")}, Total Amount: {b.TotalAmount.ToString("C", ci)}"));
                }
                else
                {
                    stringBuilder.AppendLine("There is no order in that date");
                }
            }
            else
            {
                stringBuilder.AppendLine("No Date entered");
            }

            return stringBuilder.ToString();
        }

        public string FindOrdersbyBetweenDates(string inputDate, string inputDateTwo)
        {
            StringBuilder sb = new StringBuilder();
            if (inputDate != "" && inputDateTwo != "")
            {
                CustomersRepository cusRepository = new CustomersRepository();
                List<Customer> allOfTheCustomers = cusRepository.ReadGetAllRows();
                CultureInfo ci = new CultureInfo("en-za");
                List<Order> orders = ordersRepository.ReadRowByDate(inputDate, inputDateTwo);
                if (orders.Count > 0)
                {
                    orders.ForEach(b => sb.AppendLine($"ID: {b.OrderID}, Customer Name: {allOfTheCustomers.FirstOrDefault(z => z.CustomerID == b.CustomerID).FirstName + " " + allOfTheCustomers.FirstOrDefault(z => z.CustomerID == b.CustomerID).Surname}, Order Date: {b.OrderDate.ToString("dd MMMM yyyy HH:mm")}, Total Amount: {b.TotalAmount.ToString("C", ci)}"));
                }
                else
                {
                    sb.AppendLine("There is no order between those dates");
                }
            }
            else
            {
                sb.AppendLine("Please enter both beginning and end dates");
            }

            return sb.ToString();
        }
    }
}
