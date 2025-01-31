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

namespace UnitTests.RepositoryTests.AdminMenuOptionsTests
{
    [TestFixture]
    public class CustomerOptionsTests
    {
        Mock<CustomersRepository> _mockCustomersrepository;
        Mock<RepositoryBase<Customer>> _mockCustomersRepositoryBase;
        Mock<IRepository<Customer>> _mockCustomersIRepository;
        CustomerOptions _customerOptions;

        [SetUp]
        public void SetUp()
        {
            _mockCustomersrepository = new Mock<CustomersRepository>();
            _mockCustomersRepositoryBase = new Mock<RepositoryBase<Customer>>();
            _mockCustomersIRepository = new Mock<IRepository<Customer>>();
            _customerOptions = new CustomerOptions(_mockCustomersrepository.Object, _mockCustomersRepositoryBase.Object, _mockCustomersIRepository.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockCustomersrepository = null;
            _mockCustomersRepositoryBase = null;
            _mockCustomersIRepository = null;
            _customerOptions = null;
        }

        [Test]
        public void _01Test_FindCustomerByID_ExpectedReadRowByIDNeverOccurs_WhenCustomerIDDoesNotExist()
        {
            // Arrange
            int customerId = 11;
            var customer = new Customer { CustomerID = 11, FirstName = "Mack", Surname = "Harris", Email = "Harrisj11@gmail.com", Username = "Harris01", Address = "23 West street, 4001", PhoneNumber = "0675744355" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfIdExists(customerId)).Returns(false);
            _mockCustomersrepository.Setup(repo => repo.ReadRowByID(customerId)).Returns(customer);

            // Act
            string customerDetails = _customerOptions.FindCustomerByID(customerId);

            // Assert
            _mockCustomersrepository.Verify(c => c.ReadRowByID(customerId), Times.Never);
        }

        [Test]
        public void _02Test_FindCustomerByID_ExpectedReadRowByIDOccursOnce_WhenCustomerIDExists()
        {
            // Arrange
            int customerId = 3;
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfIdExists(customerId)).Returns(true);
            _mockCustomersrepository.Setup(repo => repo.ReadRowByID(customerId)).Returns(customer);

            // Act
            string customerDetails = _customerOptions.FindCustomerByID(customerId);

            // Assert
            _mockCustomersrepository.Verify(c => c.ReadRowByID(customerId), Times.Once);
        }

        [Test]
        public void _03Test_FindCustomerByID_ExpectedTrue_WhenCustomerDetailExist()
        {
            // Arrange
            int customerId = 3;
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfIdExists(customerId)).Returns(true);
            _mockCustomersrepository.Setup(repo => repo.ReadRowByID(customerId)).Returns(customer);

            // Act
            string customerDetails = _customerOptions.FindCustomerByID(customerId);

            // Assert
            Assert.IsTrue(customerDetails.Contains(customer.CustomerID.ToString()));
            Assert.IsTrue(customerDetails.Contains(customer.FirstName));
            Assert.IsTrue(customerDetails.Contains(customer.Surname));
            Assert.IsTrue(customerDetails.Contains(customer.Email));
            Assert.IsTrue(customerDetails.Contains(customer.Username));
            Assert.IsTrue(customerDetails.Contains(customer.Address));
            Assert.IsTrue(customerDetails.Contains(customer.PhoneNumber));
        }

        [Test]
        public void _04Test_FindCustomerByID_ExpectedFalse_WhenCustomerDetailDoesNotExist()
        {
            // Arrange
            int customerId = 3;
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfIdExists(customerId)).Returns(false);
            _mockCustomersrepository.Setup(repo => repo.ReadRowByID(customerId)).Returns(customer);

            // Act
            string customerDetails = _customerOptions.FindCustomerByID(customerId);

            // Assert
            Assert.IsFalse(customerDetails.Contains(customer.CustomerID.ToString()));
            Assert.IsFalse(customerDetails.Contains(customer.FirstName));
            Assert.IsFalse(customerDetails.Contains(customer.Surname));
            Assert.IsFalse(customerDetails.Contains(customer.Email));
            Assert.IsFalse(customerDetails.Contains(customer.Username));
            Assert.IsFalse(customerDetails.Contains(customer.Address));
            Assert.IsFalse(customerDetails.Contains(customer.PhoneNumber));
        }

        [Test]
        public void _05Test_FindCustomerByID_ExpectedTrue_WhenCustomerDetailDoesNotExist()
        {
            // Arrange
            int customerId = 3;
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfIdExists(customerId)).Returns(false);
            _mockCustomersrepository.Setup(repo => repo.ReadRowByID(customerId)).Returns(customer);

            // Act
            string customerDetails = _customerOptions.FindCustomerByID(customerId);
            string expected = "Customer does not exist, Please try again";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
        }

        [Test]
        public void _06Test_FindCustomerByName_ExpectedFalse_WhenCustomerDetailDoesExist()
        {
            // Arrange
            string customerName = "Jack";
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            var customerTwo = new Customer { CustomerID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386" };
            Queue<Customer> stackCustomers = new Queue<Customer>();
            List<Customer> list = new List<Customer>();
            list.Add(customer);
            list.Add(customerTwo);
            foreach (var cus in list.Where(c => c.FirstName == customerName).OrderByDescending(c => c.CustomerID).ToList())
            {
                stackCustomers.Enqueue(cus);
            }
            _mockCustomersrepository.Setup(repo => repo.GetCustomerByName(customerName)).Returns(stackCustomers);

            // Act
            string customerDetails = _customerOptions.FindCustomerByFirstName(customerName);
            string expected = "No customer with that first name exists";

            // Assert
            Assert.IsFalse(customerDetails.Contains(expected));
        }


        [Test]
        public void _07Test_FindCustomerByName_ExpectedTrue_WhenCustomerDetailDoesNotExist()
        {
            // Arrange
            string customerName = "Alfred";
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            var customerTwo = new Customer { CustomerID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386" };
            Queue<Customer> stackCustomers = new Queue<Customer>();
            List<Customer> list = new List<Customer>();
            list.Add(customer);
            list.Add(customerTwo);
            foreach (var cus in list.Where(c => c.FirstName == customerName).OrderByDescending(c => c.CustomerID).ToList())
            {
                stackCustomers.Enqueue(cus);
            }
            _mockCustomersrepository.Setup(repo => repo.GetCustomerByName(customerName)).Returns(stackCustomers);

            // Act
            string customerDetails = _customerOptions.FindCustomerByFirstName(customerName);
            string expected = "No customer with that first name exists";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
        }

        [Test]
        public void _08Test_FindCustomerByName_ExpectedTrue_WhenCustomerNameIsEmpty()
        {
            // Arrange
            string customerName = "";
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            var customerTwo = new Customer { CustomerID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386" };
            Queue<Customer> stackCustomers = new Queue<Customer>();
            List<Customer> list = new List<Customer>();
            list.Add(customer);
            list.Add(customerTwo);
            foreach (var cus in list.Where(c => c.FirstName == customerName).OrderByDescending(c => c.CustomerID).ToList())
            {
                stackCustomers.Enqueue(cus);
            }
            _mockCustomersrepository.Setup(repo => repo.GetCustomerByName(customerName)).Returns(stackCustomers);

            // Act
            string customerDetails = _customerOptions.FindCustomerByFirstName(customerName);
            string expected = "No customer with that first name exists";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
        }

        [Test]
        public void _09Test_FindCustomerByName_ExpectedOccurOnce_WhenCustomerDetailDoesExist()
        {
            // Arrange
            string customerName = "Alfred";
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            var customerTwo = new Customer { CustomerID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386" };
            Queue<Customer> stackCustomers = new Queue<Customer>();
            List<Customer> list = new List<Customer>();
            list.Add(customer);
            list.Add(customerTwo);
            foreach (var cus in list.Where(c => c.FirstName == customerName).OrderByDescending(c => c.CustomerID).ToList())
            {
                stackCustomers.Enqueue(cus);
            }
            _mockCustomersrepository.Setup(repo => repo.GetCustomerByName(customerName)).Returns(stackCustomers);

            // Act
            string customerDetails = _customerOptions.FindCustomerByFirstName(customerName);

            // Assert
            _mockCustomersrepository.Verify(c => c.GetCustomerByName(customerName), Times.Once);
        }

        [Test]
        public void _10Test_FindCustomerByName_ExpectedMoreThanOneJack_WhenCustomerDetailDoesExist()
        {
            // Arrange
            string customerName = "Jack";
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            var customerTwo = new Customer { CustomerID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386" };
            var adminThree = new Customer { CustomerID = 9, FirstName = "Jack", Surname = "Fermin", Email = "Ferminj47@gmail.com", Username = "Ferminj47", Address = "100 Everton street, 6001", PhoneNumber = "0746277885" };
            Queue<Customer> stackCustomers = new Queue<Customer>();
            List<Customer> list = new List<Customer>();
            list.Add(customer);
            list.Add(customerTwo);
            list.Add(adminThree);
            foreach (var cus in list.Where(c => c.FirstName == customerName).OrderByDescending(c => c.CustomerID).ToList())
            {
                stackCustomers.Enqueue(cus);
            }
            _mockCustomersrepository.Setup(repo => repo.GetCustomerByName(customerName)).Returns(stackCustomers);

            // Act
            string customerDetails = _customerOptions.FindCustomerByFirstName(customerName);
            string expected = "First Name: Jack, Surname: Harrison";
            string expectedTwo = "First Name: Jack, Surname: Fermin";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
            Assert.IsTrue(customerDetails.Contains(expectedTwo));
        }

        [Test]
        public void _11Test_FindCustomerByFullName_ExpectedFalse_WhenCustomerDetailDoesExist()
        {
            // Arrange
            string customerName = "Jack";
            string customerSurname = "Harrison";
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            var customerTwo = new Customer { CustomerID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386" };
            Queue<Customer> stackCustomers = new Queue<Customer>();
            List<Customer> list = new List<Customer>();
            list.Add(customer);
            list.Add(customerTwo);
            foreach (var cus in list.Where(c => c.FirstName == customerName && c.Surname == customerSurname).OrderByDescending(c => c.CustomerID).ToList())
            {
                stackCustomers.Enqueue(cus);
            }
            _mockCustomersrepository.Setup(repo => repo.GetCustomerByName(customerName, customerSurname)).Returns(stackCustomers);

            // Act
            string customerDetails = _customerOptions.FindCustomerByFullName(customerName, customerSurname);
            string expected = "No customer with that fullname exists";

            // Assert
            Assert.IsFalse(customerDetails.Contains(expected));
        }


        [Test]
        public void _12Test_FindCustomerByFullName_ExpectedTrue_WhenCustomerDetailDoesNotExist()
        {
            // Arrange
            string customerName = "Alfred";
            string customerSurname = "Savage";
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            var customerTwo = new Customer { CustomerID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386" };
            Queue<Customer> stackCustomers = new Queue<Customer>();
            List<Customer> list = new List<Customer>();
            list.Add(customer);
            list.Add(customerTwo);
            foreach (var cus in list.Where(c => c.FirstName == customerName && c.Surname == customerSurname).OrderByDescending(c => c.CustomerID).ToList())
            {
                stackCustomers.Enqueue(cus);
            }
            _mockCustomersrepository.Setup(repo => repo.GetCustomerByName(customerName, customerSurname)).Returns(stackCustomers);

            // Act
            string customerDetails = _customerOptions.FindCustomerByFullName(customerName, customerSurname);
            string expected = "No customer with that fullname exists";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
        }

        [Test]
        public void _13Test_FindCustomerByFullName_ExpectedTrue_WhenCustomerNameIsEmpty()
        {
            // Arrange
            string customerName = "";
            string customerSurname = "";
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            var customerTwo = new Customer { CustomerID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386" };
            Queue<Customer> stackCustomers = new Queue<Customer>();
            List<Customer> list = new List<Customer>();
            list.Add(customer);
            list.Add(customerTwo);
            foreach (var cus in list.Where(c => c.FirstName == customerName && c.Surname == customerSurname).OrderByDescending(c => c.CustomerID).ToList())
            {
                stackCustomers.Enqueue(cus);
            }
            _mockCustomersrepository.Setup(repo => repo.GetCustomerByName(customerName, customerSurname)).Returns(stackCustomers);

            // Act
            string customerDetails = _customerOptions.FindCustomerByFullName(customerName, customerSurname);
            string expected = "No customer with that fullname exists";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
        }

        [Test]
        public void _14Test_FindCustomerByFullName_ExpectedTrue_WhenCustomerFirstNameIsEmptyButSurnameIsNot()
        {
            // Arrange
            string customerName = "";
            string customerSurname = "Harrision";
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            var customerTwo = new Customer { CustomerID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386" };
            Queue<Customer> stackCustomers = new Queue<Customer>();
            List<Customer> list = new List<Customer>();
            list.Add(customer);
            list.Add(customerTwo);
            foreach (var cus in list.Where(c => c.FirstName == customerName && c.Surname == customerSurname).OrderByDescending(c => c.CustomerID).ToList())
            {
                stackCustomers.Enqueue(cus);
            }
            _mockCustomersrepository.Setup(repo => repo.GetCustomerByName(customerName, customerSurname)).Returns(stackCustomers);

            // Act
            string customerDetails = _customerOptions.FindCustomerByFullName(customerName, customerSurname);
            string expected = "No customer with that fullname exists";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
        }

        [Test]
        public void _15Test_FindCustomerByFullName_ExpectedOccurOnce_WhenCustomerDetailDoesExist()
        {
            // Arrange
            string customerName = "Alfred";
            string customerSurname = "Bateman";
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            var customerTwo = new Customer { CustomerID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386" };
            Queue<Customer> stackCustomers = new Queue<Customer>();
            List<Customer> list = new List<Customer>();
            list.Add(customer);
            list.Add(customerTwo);
            foreach (var cus in list.Where(c => c.FirstName == customerName && c.Surname == customerSurname).OrderByDescending(c => c.CustomerID).ToList())
            {
                stackCustomers.Enqueue(cus);
            }
            _mockCustomersrepository.Setup(repo => repo.GetCustomerByName(customerName, customerSurname)).Returns(stackCustomers);

            // Act
            string customerDetails = _customerOptions.FindCustomerByFullName(customerName, customerSurname);

            // Assert
            _mockCustomersrepository.Verify(c => c.GetCustomerByName(customerName, customerSurname), Times.Once);
        }

        [Test]
        public void _16Test_AddNewCustomer_ExpectedAddOccurOnce_WhenCustomerDetailCorrectAndEmailDoesNotAlreadyExist()
        {
            // Arrange
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfEmailExists(customer.Email)).Returns(false);
            _mockCustomersrepository.Setup(repo => repo.AddEntity(customer)).Returns(true);

            // Act
            string customerDetails = _customerOptions.AddNewCustomer(customer);

            // Assert
            _mockCustomersrepository.Verify(c => c.AddEntity(customer), Times.Once);
        }

        [Test]
        public void _17Test_AddNewCustomer_ExpectedAddneverOccur_WhenCustomerDetailCorrectAndEmailAlreadyExist()
        {
            // Arrange
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfEmailExists(customer.Email)).Returns(true);
            _mockCustomersrepository.Setup(repo => repo.AddEntity(customer)).Returns(true);

            // Act
            string customerDetails = _customerOptions.AddNewCustomer(customer);

            // Assert
            _mockCustomersrepository.Verify(c => c.AddEntity(customer), Times.Never);
        }

        [Test]
        public void _18Test_AddNewCustomer_ExpectedSuccessMessage_WhenCustomerDetailCorrectAndEmailDoesNotAlreadyExist()
        {
            // Arrange
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfEmailExists(customer.Email)).Returns(false);
            _mockCustomersrepository.Setup(repo => repo.AddEntity(customer)).Returns(true);

            // Act
            string customerDetails = _customerOptions.AddNewCustomer(customer);
            string expected = "Customer has been added";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
        }

        [Test]
        public void _19Test_AddNewCustomer_ExpectedFailMessage_WhenCustomerDetailCorrectAndEmailAlreadyExist()
        {
            // Arrange
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfEmailExists(customer.Email)).Returns(true);
            _mockCustomersrepository.Setup(repo => repo.AddEntity(customer)).Returns(true);

            // Act
            string customerDetails = _customerOptions.AddNewCustomer(customer);
            string expected = "Customer with that email already exists";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
        }

        [Test]
        public void _20Test_AddNewCustomer_ExpectedFailMessage_WhenAddingErrorOccurs()
        {
            // Arrange
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfEmailExists(customer.Email)).Returns(false);
            _mockCustomersrepository.Setup(repo => repo.AddEntity(customer)).Returns(false);

            // Act
            string customerDetails = _customerOptions.AddNewCustomer(customer);
            string expected = "An error occured, customer was not added";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
        }

        [Test]
        public void _21Test_UpdateCustomer_ExpectedOccurOnce_WhenUpdatingCustomerCorrectly()
        {
            // Arrange
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfIdExists(customer.CustomerID)).Returns(true);
            _mockCustomersrepository.Setup(repo => repo.UpdateEntity(customer)).Returns(true);

            // Act
            string customerDetails = _customerOptions.UpdateCustomer(customer);

            // Assert
            _mockCustomersrepository.Verify(c => c.UpdateEntity(customer), Times.Once);

        }

        [Test]
        public void _22Test_UpdateCustomer_ExpectedSuccessMessage_WhenUpdatingCustomerCorrectly()
        {
            // Arrange
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfIdExists(customer.CustomerID)).Returns(true);
            _mockCustomersrepository.Setup(repo => repo.UpdateEntity(customer)).Returns(true);

            // Act
            string customerDetails = _customerOptions.UpdateCustomer(customer);
            string expected = "Customer has been updated";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
        }

        [Test]
        public void _23Test_UpdateCustomer_ExpectedNeverOccur_WhenUpdatingCustomerIdNotFound()
        {
            // Arrange
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfIdExists(customer.CustomerID)).Returns(false);
            _mockCustomersrepository.Setup(repo => repo.UpdateEntity(customer)).Returns(false);

            // Act
            string customerDetails = _customerOptions.UpdateCustomer(customer);

            // Assert
            _mockCustomersrepository.Verify(c => c.UpdateEntity(customer), Times.Never);
        }

        [Test]
        public void _24Test_UpdateCustomer_ExpectedDoNotExist_WhenUpdatingCustomerIDNotFound()
        {
            // Arrange
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfIdExists(customer.CustomerID)).Returns(false);
            _mockCustomersrepository.Setup(repo => repo.UpdateEntity(customer)).Returns(false);

            // Act
            string customerDetails = _customerOptions.UpdateCustomer(customer);
            string expected = "Customer does not exist";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
        }

        [Test]
        public void _25Test_UpdateCustomer_ExpectedErrorOccured_WhenUpdatingCustomerDoesNotExist()
        {
            // Arrange
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfIdExists(customer.CustomerID)).Returns(true);
            _mockCustomersrepository.Setup(repo => repo.UpdateEntity(customer)).Returns(false);

            // Act
            string customerDetails = _customerOptions.UpdateCustomer(customer);
            string expected = "Error Occured, Customer was not updated";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
        }

        [Test]
        public void _26Test_RemoveCustomer_ExpectedOccurOnce_WhenUpdatingCustomerCorrectly()
        {
            // Arrange
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfIdExists(customer.CustomerID)).Returns(true);
            _mockCustomersrepository.Setup(repo => repo.DeleteRow(customer.CustomerID)).Returns(true);

            // Act
            string customerDetails = _customerOptions.RemoveCustomer(customer.CustomerID);


            // Assert
            _mockCustomersrepository.Verify(c => c.DeleteRow(customer.CustomerID), Times.Once);

        }

        [Test]
        public void _27Test_RemoveCustomer_ExpectedDeletedMessage_WhenUpdatingCustomerCorrectly()
        {
            // Arrange
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfIdExists(customer.CustomerID)).Returns(true);
            _mockCustomersrepository.Setup(repo => repo.DeleteRow(customer.CustomerID)).Returns(true);

            // Act
            string customerDetails = _customerOptions.RemoveCustomer(customer.CustomerID);
            string expected = "Customer has been deleted";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
        }

        [Test]
        public void _28Test_RemoveCustomer_ExpectedNeverOccur_WhenUpdatingCustomerIdNotFound()
        {
            // Arrange
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfIdExists(customer.CustomerID)).Returns(false);
            _mockCustomersrepository.Setup(repo => repo.DeleteRow(customer.CustomerID)).Returns(false);

            // Act
            string customerDetails = _customerOptions.RemoveCustomer(customer.CustomerID);

            // Assert
            _mockCustomersrepository.Verify(c => c.DeleteRow(customer.CustomerID), Times.Never);
        }

        [Test]
        public void _29Test_RemoveCustomer_ExpectedDoNotExist_WhenUpdatingCustomerIDNotFound()
        {
            // Arrange
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfIdExists(customer.CustomerID)).Returns(false);
            _mockCustomersrepository.Setup(repo => repo.DeleteRow(customer.CustomerID)).Returns(false);

            // Act
            string customerDetails = _customerOptions.RemoveCustomer(customer.CustomerID);
            string expected = "Customer ID does not exist";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
        }

        [Test]
        public void _30Test_RemoveCustomer_ExpectedErrorOccured_WhenUpdatingCustomerDoesNotExist()
        {
            // Arrange
            var customer = new Customer { CustomerID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385" };
            _mockCustomersrepository.Setup(repo => repo.CheckIfIdExists(customer.CustomerID)).Returns(true);
            _mockCustomersrepository.Setup(repo => repo.DeleteRow(customer.CustomerID)).Returns(false);

            // Act
            string customerDetails = _customerOptions.RemoveCustomer(customer.CustomerID);
            string expected = "Error Occured, Customer not removed";

            // Assert
            Assert.IsTrue(customerDetails.Contains(expected));
        }
    }
}
