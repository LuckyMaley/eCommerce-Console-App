using MainCode.Repository.AdminMenuOptions;
using MainCode.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainCode.Models;

namespace UnitTests.RepositoryTests.AdminMenuOptionsTests
{
    [TestFixture]
    public class OrderDetailOptionTests
    {
        Mock<OrderDetailsRepository> _mockOrderDetailsrepository;
        Mock<OrdersRepository> _mockOrdersrepository;
        OrderDetailOptions _orderDetailOptions;
        List<OrderDetail> orderDetailsList;

        [SetUp]
        public void SetUp()
        {
            _mockOrderDetailsrepository = new Mock<OrderDetailsRepository>();
            _mockOrdersrepository = new Mock<OrdersRepository>();
            orderDetailsList = new List<OrderDetail>()
            {
                new OrderDetail {OrderDetailID=1, OrderID=11, ProductID=1, Quantity=1, UnitPrice=2100.99},
                new OrderDetail {OrderDetailID=2, OrderID=11, ProductID=2, Quantity=1, UnitPrice=2700.99},
                new OrderDetail {OrderDetailID=3, OrderID=12, ProductID=3, Quantity=1, UnitPrice=900.99}
            };
            _orderDetailOptions = new OrderDetailOptions(_mockOrderDetailsrepository.Object, _mockOrdersrepository.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockOrderDetailsrepository = null;
            _mockOrdersrepository = null;
            _orderDetailOptions = null;
            orderDetailsList.Clear();
        }

        [Test]
        public void _01Test_FindOrderDetailByID_ExpectedReadRowByIDNeverOccurs_WhenOrderDetailIDDoesNotExist()
        {
            // Arrange
            int orderId = 11;
            
            _mockOrdersrepository.Setup(repo => repo.CheckIfIdExists(orderId)).Returns(false);
            _mockOrderDetailsrepository.Setup(repo => repo.ReadRowByID(orderId)).Returns(orderDetailsList);

            // Act
            string orderDetails = _orderDetailOptions.FindOrderDetailByOrderID(orderId);

            // Assert
            _mockOrderDetailsrepository.Verify(c => c.ReadRowByID(orderId), Times.Never);
        }

        [Test]
        public void _02Test_FindOrderDetailByID_ExpectedReadRowByIDOccursOnce_WhenOrderDetailIDExists()
        {
            // Arrange
            int orderId = 11;
            _mockOrdersrepository.Setup(repo => repo.CheckIfIdExists(orderId)).Returns(true);
            _mockOrderDetailsrepository.Setup(repo => repo.ReadRowByID(orderId)).Returns(orderDetailsList);

            // Act
            string orderDetails = _orderDetailOptions.FindOrderDetailByOrderID(orderId);

            // Assert
            _mockOrderDetailsrepository.Verify(c => c.ReadRowByID(orderId), Times.Once);
        }

        [Test]
        public void _03Test_FindOrderDetailByID_ExpectedFalse_WhenOrderDetailExist()
        {
            // Arrange
            int orderId = 11;
            _mockOrdersrepository.Setup(repo => repo.CheckIfIdExists(orderId)).Returns(true);
            _mockOrderDetailsrepository.Setup(repo => repo.ReadRowByID(orderId)).Returns(orderDetailsList);

            // Act
            string orderDetails = _orderDetailOptions.FindOrderDetailByOrderID(orderId);
            string expected = "order does not exist, Please try again";

            // Assert
            Assert.IsFalse(orderDetails.Contains(expected));
        }

        [Test]
        public void _04Test_FindOrderDetailByID_ExpectedFalse_WhenOrderDetailDoesNotExist()
        {
            // Arrange
            int orderId = 11;
            _mockOrdersrepository.Setup(repo => repo.CheckIfIdExists(orderId)).Returns(false);
            _mockOrderDetailsrepository.Setup(repo => repo.ReadRowByID(orderId)).Returns(orderDetailsList);

            // Act
            string orderDetails = _orderDetailOptions.FindOrderDetailByOrderID(orderId);
            string expected = "ID: 11";

            // Assert
            Assert.IsFalse(orderDetails.Contains(expected));
        }

        [Test]
        public void _05Test_FindOrderDetailByID_ExpectedTrue_WhenOrderDetailDoesNotExist()
        {
            // Arrange
            int orderId = 11;
            _mockOrdersrepository.Setup(repo => repo.CheckIfIdExists(orderId)).Returns(false);
            _mockOrderDetailsrepository.Setup(repo => repo.ReadRowByID(orderId)).Returns(orderDetailsList);

            // Act
            string orderDetails = _orderDetailOptions.FindOrderDetailByOrderID(orderId);
            string expected = "order does not exist, Please try again";

            // Assert
            Assert.IsTrue(orderDetails.Contains(expected));
        }
    }
}
