using MainCode.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository
{
    public class ShippingsRepository
    {
        private static List<Shipping> allShippings;

        public ShippingsRepository()
        {
            allShippings = ReadGetAllRows();
        }

        public virtual List<Shipping> ReadGetAllRows()
        {
            allShippings = new List<Shipping>();
            foreach (var ship in Shipping.ShippingsDataSet)
            {
                allShippings.Add(new Shipping()
                {
                    ShippingID = ship.ShippingID,
                    ShipDate = ship.ShipDate,
                    ShipMethod = ship.ShipMethod,
                    TrackingNumber = ship.TrackingNumber,
                    DeliveryStatus = ship.DeliveryStatus
                });
            }
            return allShippings;
        }

        public Shipping ReadRowByTrackNum(string trackNum)
        {
            ShippingsRepository repository = new ShippingsRepository();
            List<Shipping> allOfTheShippings = repository.ReadGetAllRows();
            Shipping shipping = allOfTheShippings.FirstOrDefault(c => c.TrackingNumber == trackNum);
            return shipping;
        }

        public List<Shipping> ReadRowsByMethod(string method)
        {
            ShippingsRepository repository = new ShippingsRepository();
            List<Shipping> allOfTheShippings = repository.ReadGetAllRows();
            IEnumerable<Shipping> shipping = allOfTheShippings.Where(c => c.ShipMethod == method);
            List<Shipping> shippings = new List<Shipping>();
            if (shipping != null)
            {
                shippings = shipping.ToList();
            }
            else
            {
                shippings = null;
            }
            return shippings;
        }

        public List<Shipping> ReadRowByDate(string date)
        {
            ShippingsRepository repository = new ShippingsRepository();
            List<Shipping> allOfTheShippings = repository.ReadGetAllRows();

            string defaultDateString = "10/08/2008";
            string format = "dd/MM/yyyy";
            CultureInfo ci = new CultureInfo("en-za");

            DateTime dateTime;
            if (!DateTime.TryParseExact(date, format, ci, System.Globalization.DateTimeStyles.None, out dateTime))
            {
                dateTime = DateTime.ParseExact(defaultDateString, format, ci);
            }
            IEnumerable<Shipping> shipping = allOfTheShippings.Where(c => c.ShipDate.Date == dateTime.Date);
            List<Shipping> shippings = new List<Shipping>();
            if (shipping != null)
            {
                shippings = shipping.ToList();
            }
            else
            {
                shippings = null;
            }
            return shippings;
        }

        public List<Shipping> ReadRowByDate(string beginDate, string endDate)
        {
            ShippingsRepository repository = new ShippingsRepository();
            List<Shipping> allOfTheShippings = repository.ReadGetAllRows();

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
            IEnumerable<Shipping> shipping = allOfTheShippings.Where(c => c.ShipDate.Date >= beginDateTime.Date && c.ShipDate.Date <= endDateTime.Date);
            List<Shipping> shippings = new List<Shipping>();
            if (shipping != null)
            {
                shippings = shipping.ToList();
            }
            else
            {
                shippings = null;
            }
            return shippings;
        }
    }
}
