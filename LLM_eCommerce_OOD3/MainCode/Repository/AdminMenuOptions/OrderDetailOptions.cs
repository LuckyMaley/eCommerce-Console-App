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
    /// This OrderDetailOptions class consists of methods called when a user clicks a menu option in the front-end.
    /// These methods interact with the Repository to extract or remove data from the Repository.
    /// </summary>
    /// <remarks>
    /// 
    /// public OrderDetailOptions(OrderDetailsRepository _orderDetailsRepository, OrdersRepository _ordersRepository)
    /// A counstructor that initializes the OrderDetailOptions Class.
    /// These parameters are used for injection into the class for mock testing.
    /// <param name="_orderDetailsRepository">The orderDetails repository.</param>
    /// <param name="_ordersRepositoryBase">The orders repository.</param>
    /// 
    /// public string FindOrderDetailByOrderID(int orderID)
    /// Finds an order by ID and returns the order details as a string.
    /// <param name="orderID">The ID of the order to find.</param>
    /// <returns>A string containing the order details if found, or a message indicating that the order does not exist.</returns>
    ///</remarks>
    public class OrderDetailOptions
    {
        OrderDetailsRepository orderDetailsRepository;
        OrdersRepository ordersRepository;

        public OrderDetailOptions(OrderDetailsRepository _orderDetailsRepository, OrdersRepository _ordersRepository)
        {
            orderDetailsRepository = _orderDetailsRepository;
            ordersRepository = _ordersRepository;
        }

        public string FindOrderDetailByOrderID(int orderID)
        {
            List<OrderDetail> orderDetails = orderDetailsRepository.ReadGetAllRows();
            ProductsRepository prodRepository = new ProductsRepository();
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();
            StringBuilder stringBuilder = new StringBuilder();
            CultureInfo ci = new CultureInfo("en-za");
            
            bool valid = ordersRepository.CheckIfIdExists(orderID);
            if (valid)
            {
                var orderDetailsList = orderDetailsRepository.ReadRowByID(orderID);
                orderDetailsList.ForEach(b => stringBuilder.AppendLine($"ID: {b.OrderDetailID}, Product Name: {allOfTheProducts.FirstOrDefault(z => z.ProductID == b.ProductID).Name}, Quantity: {b.Quantity}, Unit Price: {b.UnitPrice.ToString("C", ci)}"));

            }
            else
            {
                stringBuilder.AppendLine("order does not exist, Please try again");
            }
            
            
            return stringBuilder.ToString();
        }
    }
}
