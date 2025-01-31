using MainCode.Models;
using MainCode.Repository.AdminMenuOptions;
using MainCode.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace UnitTests.RepositoryTests.AdminMenuOptionsTests
{
    [TestFixture]
    public class OrderOptionsTests
    {
        Mock<OrdersRepository> _mockOrdersrepository;
        OrderOptions _orderOptions;
        List<Order> ordersList;

        [SetUp]
        public void SetUp()
        {
            _mockOrdersrepository = new Mock<OrdersRepository>();
            ordersList = new List<Order>()
            {
                new Order() {OrderID = 3, CustomerID = 3, ShippingID = 3, OrderDate = new DateTime(2019, 11, 12, 12, 45, 00), TotalAmount = 900.99},
                new Order() {OrderID = 4, CustomerID = 4, ShippingID = 4, OrderDate = new DateTime(2018, 5, 12, 12, 45, 00), TotalAmount = 1000.99},
                new Order() {OrderID = 5, CustomerID = 5, ShippingID = 5, OrderDate = new DateTime(2018, 3, 21  , 21, 47, 50), TotalAmount = 799.99}
            };
            _orderOptions = new OrderOptions(_mockOrdersrepository.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockOrdersrepository = null;
            _orderOptions = null;
            ordersList.Clear();
        }

        [Test]
        public void _01Test_FindOrderByID_ExpectedReadRowByIDNeverOccurs_WhenOrderIDDoesNotExist()
        {
            // Arrange
            int orderId = 11;
            var order = new Order() { OrderID = 11, CustomerID = 7, ShippingID = 7, OrderDate = new DateTime(2020, 2, 13, 13, 35, 00), TotalAmount = 200.99 };

            _mockOrdersrepository.Setup(repo => repo.CheckIfIdExists(orderId)).Returns(false);
            _mockOrdersrepository.Setup(repo => repo.ReadRowByID(orderId)).Returns(order);

            // Act
            string orders = _orderOptions.FindOrderByID(orderId);

            // Assert
            _mockOrdersrepository.Verify(c => c.ReadRowByID(orderId), Times.Never);
        }

        [Test]
        public void _02Test_FindOrderByID_ExpectedReadRowByIDOccursOnce_WhenOrderIDExists()
        {
            // Arrange
            int orderId = 11;
            var order = new Order() { OrderID = 11, CustomerID = 7, ShippingID = 7, OrderDate = new DateTime(2020, 2, 13, 13, 35, 00), TotalAmount = 200.99 };

            _mockOrdersrepository.Setup(repo => repo.CheckIfIdExists(orderId)).Returns(true);
            _mockOrdersrepository.Setup(repo => repo.ReadRowByID(orderId)).Returns(order);

            // Act
            string orders = _orderOptions.FindOrderByID(orderId);

            // Assert
            _mockOrdersrepository.Verify(c => c.ReadRowByID(orderId), Times.Once);
        }

        [Test]
        public void _03Test_FindOrderByID_ExpectedFalse_WhenOrderExist()
        {
            // Arrange
            int orderId = 11;
            var order = new Order() { OrderID = 11, CustomerID = 7, ShippingID = 7, OrderDate = new DateTime(2020, 2, 13, 13, 35, 00), TotalAmount = 200.99 };

            _mockOrdersrepository.Setup(repo => repo.CheckIfIdExists(orderId)).Returns(true);
            _mockOrdersrepository.Setup(repo => repo.ReadRowByID(orderId)).Returns(order);

            // Act
            string orders = _orderOptions.FindOrderByID(orderId);
            string expected = "order does not exist, Please try again";

            // Assert
            Assert.IsFalse(orders.Contains(expected));
        }

        [Test]
        public void _04Test_FindOrderByID_ExpectedFalse_WhenOrderDoesNotExist()
        {
            // Arrange
            int orderId = 11;
            var order = new Order() { OrderID = 11, CustomerID = 7, ShippingID = 7, OrderDate = new DateTime(2020, 2, 13, 13, 35, 00), TotalAmount = 200.99 };

            _mockOrdersrepository.Setup(repo => repo.CheckIfIdExists(orderId)).Returns(false);
            _mockOrdersrepository.Setup(repo => repo.ReadRowByID(orderId)).Returns(order);

            // Act
            string orders = _orderOptions.FindOrderByID(orderId);
            string expected = "ID: 11";

            // Assert
            Assert.IsFalse(orders.Contains(expected));
        }

        [Test]
        public void _05Test_FindOrderByID_ExpectedTrue_WhenOrderDoesNotExist()
        {
            // Arrange
            int orderId = 11;
            var order = new Order() { OrderID = 11, CustomerID = 7, ShippingID = 7, OrderDate = new DateTime(2020, 2, 13, 13, 35, 00), TotalAmount = 200.99 };

            _mockOrdersrepository.Setup(repo => repo.CheckIfIdExists(orderId)).Returns(false);
            _mockOrdersrepository.Setup(repo => repo.ReadRowByID(orderId)).Returns(order);

            // Act
            string orders = _orderOptions.FindOrderByID(orderId);
            string expected = "order does not exist, Please try again";

            // Assert
            Assert.IsTrue(orders.Contains(expected));
        }

        [Test]
        public void _06Test_FindOrderByDate_ExpectedFalse_WhenOrderDoesExist()
        {
            // Arrange
            string date = "12/11/2019";

            _mockOrdersrepository.Setup(repo => repo.ReadRowByDate(date)).Returns(ordersList.Where(c => c.OrderDate.Date.ToString("dd/MM/yyyy").Equals(date)).ToList());

            // Act
            string orderDetails = _orderOptions.FindOrdersbyDate(date);
            string expected = "There is no order in that date";

            // Assert
            Assert.IsFalse(orderDetails.Contains(expected));
        }


        [Test]
        public void _07Test_FindOrderByDate_ExpectedTrue_WhenOrderDoesNotExist()
        {
            // Arrange
            string date = "17/11/2019";

            _mockOrdersrepository.Setup(repo => repo.ReadRowByDate(date)).Returns(ordersList.Where(c => c.OrderDate.Date.ToString("dd/MM/yyyy").Equals(date)).ToList());

            // Act
            string orderDetails = _orderOptions.FindOrdersbyDate(date);
            string expected = "There is no order in that date";

            // Assert
            Assert.IsTrue(orderDetails.Contains(expected));
        }

        [Test]
        public void _08Test_FindOrderByDate_ExpectedTrue_WhenOrderDateIsEmpty()
        {
            // Arrange
            string date = "";

            _mockOrdersrepository.Setup(repo => repo.ReadRowByDate(date)).Returns(ordersList.Where(c => c.OrderDate.Date.ToString("dd/MM/yyyy").Equals(date)).ToList());

            // Act
            string orderDetails = _orderOptions.FindOrdersbyDate(date);
            string expected = "No Date entered";

            // Assert
            Assert.IsTrue(orderDetails.Contains(expected));
        }

        [Test]
        public void _09Test_FindOrderByDate_ExpectedOccurOnce_WhenOrderDoesExist()
        {
            // Arrange
            string date = "12/11/2019";

            _mockOrdersrepository.Setup(repo => repo.ReadRowByDate(date)).Returns(ordersList.Where(c => c.OrderDate.Date.ToString("dd/MM/yyyy").Equals(date)).ToList());

            // Act
            string orderDetails = _orderOptions.FindOrdersbyDate(date);

            // Assert
            _mockOrdersrepository.Verify(c => c.ReadRowByDate(date), Times.Once);
        }

        [Test]
        public void _10Test_FindOrderByDate_ExpectedMoreThanOne_WhenOrderDoesExist()
        {
            // Arrange
            ordersList = new List<Order>()
            {
                new Order() {OrderID = 3, CustomerID = 3, ShippingID = 3, OrderDate = new DateTime(2019, 11, 12, 12, 45, 00), TotalAmount = 900.99},
                new Order() {OrderID = 4, CustomerID = 4, ShippingID = 4, OrderDate = new DateTime(2018, 5, 12, 12, 45, 00), TotalAmount = 1000.99},
                new Order() {OrderID = 5, CustomerID = 5, ShippingID = 5, OrderDate = new DateTime(2018, 5, 12, 21, 47, 50), TotalAmount = 799.99}
            };
            string date = "12/05/2018";

            _mockOrdersrepository.Setup(repo => repo.ReadRowByDate(date)).Returns(ordersList.Where(c => c.OrderDate.Date.ToString("dd/MM/yyyy").Equals(date)).ToList());

            // Act
            string orderDetails = _orderOptions.FindOrdersbyDate(date);

            // Assert
            Assert.IsTrue(orderDetails.Contains("ID: 4"));
            Assert.IsTrue(orderDetails.Contains("ID: 5"));
        }

        [Test]
        public void _11Test_FindOrderByBetweenDate_ExpectedFalse_WhenOrderDoesExist()
        {
            // Arrange
            string date = "10/05/2018";
            string dateTwo = "13/11/2018";
            string defaultDateString = "10/08/2008";
            string format = "dd/MM/yyyy";
            CultureInfo ci = new CultureInfo("en-za");

            DateTime beginDateTime;
            DateTime.TryParseExact(date, format, ci, System.Globalization.DateTimeStyles.None, out beginDateTime);

            DateTime endDateTime;
            DateTime.TryParseExact(dateTwo, format, ci, System.Globalization.DateTimeStyles.None, out endDateTime);

            _mockOrdersrepository.Setup(repo => repo.ReadRowByDate(date, dateTwo)).Returns(ordersList.Where(c => c.OrderDate.Date >= beginDateTime.Date && c.OrderDate.Date <= endDateTime.Date).ToList());

            // Act
            string orderDetails = _orderOptions.FindOrdersbyBetweenDates(date, dateTwo);
            string expected = "There is no order between those dates";

            // Assert
            Assert.IsFalse(orderDetails.Contains(expected));
        }


        [Test]
        public void _12Test_FindOrderByBetweenDate_ExpectedTrue_WhenOrderDoesNotExist()
        {
            // Arrange
            string date = "17/11/2019";
            string dateTwo = "21/11/2019";
            string defaultDateString = "10/08/2008";
            string format = "dd/MM/yyyy";
            CultureInfo ci = new CultureInfo("en-za");

            DateTime beginDateTime;
            DateTime.TryParseExact(date, format, ci, System.Globalization.DateTimeStyles.None, out beginDateTime);

            DateTime endDateTime;
            DateTime.TryParseExact(dateTwo, format, ci, System.Globalization.DateTimeStyles.None, out endDateTime);

            _mockOrdersrepository.Setup(repo => repo.ReadRowByDate(date, dateTwo)).Returns(ordersList.Where(c => c.OrderDate.Date >= beginDateTime.Date && c.OrderDate.Date <= endDateTime.Date).ToList());

            // Act
            string orderDetails = _orderOptions.FindOrdersbyBetweenDates(date, dateTwo);
            string expected = "There is no order between those dates";

            // Assert
            Assert.IsTrue(orderDetails.Contains(expected));
        }

        [Test]
        public void _13Test_FindOrderByBetweenDate_ExpectedTrue_WhenOneOrderDateIsEmpty()
        {
            // Arrange
            string date = "";
            string dateTwo = "13/11/2018";
            string defaultDateString = "10/08/2008";
            string format = "dd/MM/yyyy";
            CultureInfo ci = new CultureInfo("en-za");

            DateTime beginDateTime;
            DateTime.TryParseExact(date, format, ci, System.Globalization.DateTimeStyles.None, out beginDateTime);

            DateTime endDateTime;
            DateTime.TryParseExact(dateTwo, format, ci, System.Globalization.DateTimeStyles.None, out endDateTime);

            _mockOrdersrepository.Setup(repo => repo.ReadRowByDate(date, dateTwo)).Returns(ordersList.Where(c => c.OrderDate.Date >= beginDateTime.Date && c.OrderDate.Date <= endDateTime.Date).ToList());

            // Act
            string orderDetails = _orderOptions.FindOrdersbyBetweenDates(date, dateTwo);
            string expected = "Please enter both beginning and end dates";

            // Assert
            Assert.IsTrue(orderDetails.Contains(expected));
        }

        [Test]
        public void _14Test_FindOrderByBetweenDate_ExpectedOccurOnce_WhenOrderDoesExist()
        {
            // Arrange
            string date = "12/11/2019";
            string dateTwo = "13/11/2018";
            string defaultDateString = "10/08/2008";
            string format = "dd/MM/yyyy";
            CultureInfo ci = new CultureInfo("en-za");

            DateTime beginDateTime;
            DateTime.TryParseExact(date, format, ci, System.Globalization.DateTimeStyles.None, out beginDateTime);

            DateTime endDateTime;
            DateTime.TryParseExact(dateTwo, format, ci, System.Globalization.DateTimeStyles.None, out endDateTime);

            _mockOrdersrepository.Setup(repo => repo.ReadRowByDate(date, dateTwo)).Returns(ordersList.Where(c => c.OrderDate.Date >= beginDateTime.Date && c.OrderDate.Date <= endDateTime.Date).ToList());

            // Act
            string orderDetails = _orderOptions.FindOrdersbyBetweenDates(date, dateTwo);

            // Assert
            _mockOrdersrepository.Verify(c => c.ReadRowByDate(date, dateTwo), Times.Once);
        }

        [Test]
        public void _15Test_FindOrderByBetweenDate_ExpectedMoreThanOne_WhenOrderDoesExist()
        {
            // Arrange
            ordersList = new List<Order>()
            {
                new Order() {OrderID = 3, CustomerID = 3, ShippingID = 3, OrderDate = new DateTime(2019, 11, 12, 12, 45, 00), TotalAmount = 900.99},
                new Order() {OrderID = 4, CustomerID = 4, ShippingID = 4, OrderDate = new DateTime(2018, 5, 12, 12, 45, 00), TotalAmount = 1000.99},
                new Order() {OrderID = 5, CustomerID = 5, ShippingID = 5, OrderDate = new DateTime(2018, 5, 12, 21, 47, 50), TotalAmount = 799.99}
            };
            string date = "12/05/2018";
            string dateTwo = "13/11/2018";
            string defaultDateString = "10/08/2008";
            string format = "dd/MM/yyyy";
            CultureInfo ci = new CultureInfo("en-za");

            DateTime beginDateTime;
            DateTime.TryParseExact(date, format, ci, System.Globalization.DateTimeStyles.None, out beginDateTime);

            DateTime endDateTime;
            DateTime.TryParseExact(dateTwo, format, ci, System.Globalization.DateTimeStyles.None, out endDateTime);

            _mockOrdersrepository.Setup(repo => repo.ReadRowByDate(date, dateTwo)).Returns(ordersList.Where(c => c.OrderDate.Date >= beginDateTime.Date && c.OrderDate.Date <= endDateTime.Date).ToList());

            // Act
            string orderDetails = _orderOptions.FindOrdersbyBetweenDates(date, dateTwo);

            // Assert
            Assert.IsTrue(orderDetails.Contains("ID: 4"));
            Assert.IsTrue(orderDetails.Contains("ID: 5"));
        }

    }
}
