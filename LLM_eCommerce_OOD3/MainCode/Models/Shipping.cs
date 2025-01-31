using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Models
{
    public class Shipping
    {
        public int ShippingID { get; set; }
        public DateTime ShipDate { get; set; }
        public string ShipAddress { get; set; }
        public string ShipMethod { get; set; }
        public string TrackingNumber { get; set; }
        public string DeliveryStatus { get; set; }

        public static List<Shipping> ShippingsDataSet = new List<Shipping>()
        {
            new Shipping(){ ShippingID = 1, ShipDate=new DateTime(2023,11,14,12,55,00), ShipAddress="15 Zuma avenue, 2001", ShipMethod="Courier", TrackingNumber="2D222236", DeliveryStatus="Delivered"},
            new Shipping(){ ShippingID = 2, ShipDate=new DateTime(2017,4,25,15,45,10), ShipAddress="13 Commercial street, 4001", ShipMethod="Courier", TrackingNumber=Guid.NewGuid().ToString().Substring(0,8), DeliveryStatus="Delivered"},
            new Shipping(){ ShippingID = 3, ShipDate=new DateTime(2019,11,16,12,55,00), ShipAddress="300 West street, 4001", ShipMethod="Courier", TrackingNumber=Guid.NewGuid().ToString().Substring(0,8), DeliveryStatus="Delivered"},
            new Shipping(){ ShippingID = 4, ShipDate=new DateTime(2018,5,16,12,55,00), ShipAddress="13 Zimbali avenue, 4501", ShipMethod="Courier", TrackingNumber=Guid.NewGuid().ToString().Substring(0, 8), DeliveryStatus="Delivered"},
            new Shipping(){ ShippingID = 5, ShipDate=new DateTime(2018,3,16,21,57,50), ShipAddress="51 Sandton avenue, 2001", ShipMethod="Courier", TrackingNumber=Guid.NewGuid().ToString().Substring(0, 8), DeliveryStatus="Delivered"},
            new Shipping(){ ShippingID = 6, ShipDate=new DateTime(2019,10,17,8,6,40), ShipAddress="10 King Edwards avenue, 3201", ShipMethod="Courier", TrackingNumber=Guid.NewGuid().ToString().Substring(0, 8), DeliveryStatus="Delivered"},
            new Shipping(){ ShippingID = 7, ShipDate=new DateTime(2020,2,5,13,45,00), ShipAddress="16 Allison avenue, 2001", ShipMethod="Courier", TrackingNumber=Guid.NewGuid().ToString().Substring(0, 8), DeliveryStatus="Delivered"},
            new Shipping(){ ShippingID = 8, ShipDate=new DateTime(2021,12,16,12,55,00), ShipAddress="22 Zakhele avenue, 3001", ShipMethod="Courier", TrackingNumber=Guid.NewGuid().ToString().Substring(0, 8), DeliveryStatus="Delivered"},
            new Shipping(){ ShippingID = 9, ShipDate=new DateTime(2022,12,14,10,50,00), ShipAddress="100 Marchester street, 6001", ShipMethod="Courier", TrackingNumber=Guid.NewGuid().ToString().Substring(0, 8), DeliveryStatus="Delivered"},
            new Shipping(){ ShippingID = 10, ShipDate=new DateTime(2020,3,25,16,10,20), ShipAddress="20 Zuma avenue, 2001", ShipMethod="Courier", TrackingNumber=Guid.NewGuid().ToString().Substring(0, 8), DeliveryStatus="Delivered"}
        };
    }
}
