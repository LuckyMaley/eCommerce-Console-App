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
    public class ShippingOptions
    {
        public string FindShippingbyTrackingNumber(ShippingsRepository repository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Shipping> allOfTheShippings = repository.ReadGetAllRows();
            Console.WriteLine("Please type in the shipping tracking Number: ");
            string response = Console.ReadLine();
            if (response != "")
            {
                Shipping shipping = repository.ReadRowByTrackNum(response);
                if (shipping != null)
                {

                    stringBuilder.AppendLine($"ID: {shipping.ShippingID}, Shipping Date: {shipping.ShipDate.ToString("dd MMMM yyyy HH:mm")}, Shipping Method: {shipping.ShipMethod}, Tracking Number: {shipping.TrackingNumber}, Delivery Status: {shipping.DeliveryStatus}");
                }
                else
                {
                    stringBuilder.AppendLine("tracking number not found");
                }
            }
            else
            {
                stringBuilder.AppendLine("Please enter a tracking number");
            }
            return stringBuilder.ToString();
        }

        public string FindShippingbyMethod(ShippingsRepository repository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Shipping> allOfTheShippings = repository.ReadGetAllRows();
            Console.WriteLine("Please type in the shipping Method: ");
            string response = Console.ReadLine();
            if (response != "")
            {
                List<Shipping> shippinglist = repository.ReadRowsByMethod(response);
                if (shippinglist != null)
                {

                    shippinglist.ForEach(shipping => stringBuilder.AppendLine($"ID: {shipping.ShippingID}, Shipping Date: {shipping.ShipDate.ToString("dd MMMM yyyy HH:mm")}, Shipping Method: {shipping.ShipMethod}, Tracking Number: {shipping.TrackingNumber}, Delivery Status: {shipping.DeliveryStatus}"));
                }
                else
                {
                    stringBuilder.AppendLine("Shipping method not found");
                }
            }
            else
            {
                stringBuilder.AppendLine("Please enter a shipping method");
            }
            return stringBuilder.ToString();
        }

        public string FindShippingbyDate(ShippingsRepository repository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Shipping> allOfTheShippings = repository.ReadGetAllRows();
            Console.WriteLine("Please type in the shipping date (dd/MM/yyy): ");
            string response = Console.ReadLine();
            if (response != "")
            {
                List<Shipping> shippinglist = repository.ReadRowByDate(response);
                if (shippinglist != null)
                {

                    shippinglist.ForEach(shipping => stringBuilder.AppendLine($"ID: {shipping.ShippingID}, Shipping Date: {shipping.ShipDate.ToString("dd MMMM yyyy HH:mm")}, Shipping Method: {shipping.ShipMethod}, Tracking Number: {shipping.TrackingNumber}, Delivery Status: {shipping.DeliveryStatus}"));
                }
                else
                {
                    stringBuilder.AppendLine("Shipping date not found");
                }
            }
            else
            {
                stringBuilder.AppendLine("Please enter a shipping date");
            }
            return stringBuilder.ToString();
        }

        public string FindShippingbetweenDates(ShippingsRepository repository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Shipping> allOfTheShippings = repository.ReadGetAllRows();
            Console.WriteLine("Please type in the starting shipping date (dd/MM/yyy): ");
            string response = Console.ReadLine();

            Console.WriteLine("Please type in the ending shipping date (dd/MM/yyy): ");
            string responseTwo = Console.ReadLine();
            if (response != "" || responseTwo != "")
            {
                List<Shipping> shippinglist = repository.ReadRowByDate(response, responseTwo);
                if (shippinglist != null)
                {

                    shippinglist.ForEach(shipping => stringBuilder.AppendLine($"ID: {shipping.ShippingID}, Shipping Date: {shipping.ShipDate.ToString("dd MMMM yyyy HH:mm")}, Shipping Method: {shipping.ShipMethod}, Tracking Number: {shipping.TrackingNumber}, Delivery Status: {shipping.DeliveryStatus}"));
                }
                else
                {
                    stringBuilder.AppendLine("Shipping dates not found");
                }
            }
            else
            {
                stringBuilder.AppendLine("Please enter both shipping dates");
            }
            return stringBuilder.ToString();
        }
    }
}
