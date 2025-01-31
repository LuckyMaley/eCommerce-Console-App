using MainCode;
using MainCode.Models;
using MainCode.Repository;
using MainCode.Repository.AdminMenuOptions;
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
    public class AdminOptionsTests
    {
        Mock<AdminsRepository> _mockAdminsrepository;
        Mock<RepositoryBase<Admin>> _mockAdminsRepositoryBase;
        AdminOptions _adminOptions;

        [SetUp]
        public void SetUp()
        {
            _mockAdminsrepository = new Mock<AdminsRepository>();
            _mockAdminsRepositoryBase = new Mock<RepositoryBase<Admin>>();
            _adminOptions = new AdminOptions(_mockAdminsrepository.Object, _mockAdminsRepositoryBase.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockAdminsrepository = null;
            _mockAdminsRepositoryBase = null;
            _adminOptions = null;
        }

        [Test]
        public void _01Test_FindAdminByID_ExpectedReadRowByIDNeverOccurs_WhenAdminIDDoesNotExist()
        {
            // Arrange
            int adminId = 11;
            var admin = new Admin { AdminID = 11, FirstName = "Mack", Surname = "Harris", Email = "Harrisj11@gmail.com", Username = "Harris01", Address = "23 West street, 4001", PhoneNumber = "0675744355", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfIdExists(adminId)).Returns(false);
            _mockAdminsrepository.Setup(repo => repo.ReadRowByID(adminId)).Returns(admin);

            // Act
            string adminDetails = _adminOptions.FindAdminByID(adminId);

            // Assert
            _mockAdminsrepository.Verify(c => c.ReadRowByID(adminId), Times.Never);
        }

        [Test]
        public void _02Test_FindAdminByID_ExpectedReadRowByIDOccursOnce_WhenAdminIDExists()
        {
            // Arrange
            int adminId = 3;
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfIdExists(adminId)).Returns(true);
            _mockAdminsrepository.Setup(repo => repo.ReadRowByID(adminId)).Returns(admin);

            // Act
            string adminDetails = _adminOptions.FindAdminByID(adminId);

            // Assert
            _mockAdminsrepository.Verify(c => c.ReadRowByID(adminId), Times.Once);
        }

        [Test]
        public void _03Test_FindAdminByID_ExpectedTrue_WhenAdminDetailExist()
        {
            // Arrange
            int adminId = 3;
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfIdExists(adminId)).Returns(true);
            _mockAdminsrepository.Setup(repo => repo.ReadRowByID(adminId)).Returns(admin);

            // Act
            string adminDetails = _adminOptions.FindAdminByID(adminId);

            // Assert
            Assert.IsTrue(adminDetails.Contains(admin.AdminID.ToString()));
            Assert.IsTrue(adminDetails.Contains(admin.FirstName));
            Assert.IsTrue(adminDetails.Contains(admin.Surname));
            Assert.IsTrue(adminDetails.Contains(admin.Email));
            Assert.IsTrue(adminDetails.Contains(admin.Username));
            Assert.IsTrue(adminDetails.Contains(admin.Address));
            Assert.IsTrue(adminDetails.Contains(admin.PhoneNumber));
        }

        [Test]
        public void _04Test_FindAdminByID_ExpectedFalse_WhenAdminDetailDoesNotExist()
        {
            // Arrange
            int adminId = 3;
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfIdExists(adminId)).Returns(false);
            _mockAdminsrepository.Setup(repo => repo.ReadRowByID(adminId)).Returns(admin);

            // Act
            string adminDetails = _adminOptions.FindAdminByID(adminId);

            // Assert
            Assert.IsFalse(adminDetails.Contains(admin.AdminID.ToString()));
            Assert.IsFalse(adminDetails.Contains(admin.FirstName));
            Assert.IsFalse(adminDetails.Contains(admin.Surname));
            Assert.IsFalse(adminDetails.Contains(admin.Email));
            Assert.IsFalse(adminDetails.Contains(admin.Username));
            Assert.IsFalse(adminDetails.Contains(admin.Address));
            Assert.IsFalse(adminDetails.Contains(admin.PhoneNumber));
        }

        [Test]
        public void _05Test_FindAdminByID_ExpectedTrue_WhenAdminDetailDoesNotExist()
        {
            // Arrange
            int adminId = 3;
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfIdExists(adminId)).Returns(false);
            _mockAdminsrepository.Setup(repo => repo.ReadRowByID(adminId)).Returns(admin);

            // Act
            string adminDetails = _adminOptions.FindAdminByID(adminId);
            string expected = "Admin does not exist, Please try again";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
        }

        [Test]
        public void _06Test_FindAdminByName_ExpectedFalse_WhenAdminDetailDoesExist()
        {
            // Arrange
            string adminName = "Jack";
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            var adminTwo = new Admin { AdminID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386", Role = "Admin" };
            Stack<Admin> stackAdmins = new Stack<Admin>();
            List<Admin> list = new List<Admin>();
            list.Add(admin);
            list.Add(adminTwo);
            foreach (var adm in list.Where(c => c.FirstName == adminName).OrderByDescending(c => c.AdminID).ToList())
            {
                stackAdmins.Push(adm);
            }
            _mockAdminsrepository.Setup(repo => repo.GetAdminByName(adminName)).Returns(stackAdmins);

            // Act
            string adminDetails = _adminOptions.FindAdminByFirstName(adminName);
            string expected = "No admin with that first name exists";

            // Assert
            Assert.IsFalse(adminDetails.Contains(expected));
        }


        [Test]
        public void _07Test_FindAdminByName_ExpectedTrue_WhenAdminDetailDoesNotExist()
        {
            // Arrange
            string adminName = "Alfred";
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            var adminTwo = new Admin { AdminID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386", Role = "Admin" };
            Stack<Admin> stackAdmins = new Stack<Admin>();
            List<Admin> list = new List<Admin>();
            list.Add(admin);
            list.Add(adminTwo);
            foreach (var adm in list.Where(c => c.FirstName == adminName).OrderByDescending(c => c.AdminID).ToList())
            {
                stackAdmins.Push(adm);
            }
            _mockAdminsrepository.Setup(repo => repo.GetAdminByName(adminName)).Returns(stackAdmins);

            // Act
            string adminDetails = _adminOptions.FindAdminByFirstName(adminName);
            string expected = "No admin with that first name exists";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
        }

        [Test]
        public void _08Test_FindAdminByName_ExpectedTrue_WhenAdminNameIsEmpty()
        {
            // Arrange
            string adminName = "";
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            var adminTwo = new Admin { AdminID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386", Role = "Admin" };
            Stack<Admin> stackAdmins = new Stack<Admin>();
            List<Admin> list = new List<Admin>();
            list.Add(admin);
            list.Add(adminTwo);
            foreach (var adm in list.Where(c => c.FirstName == adminName).OrderByDescending(c => c.AdminID).ToList())
            {
                stackAdmins.Push(adm);
            }
            _mockAdminsrepository.Setup(repo => repo.GetAdminByName(adminName)).Returns(stackAdmins);

            // Act
            string adminDetails = _adminOptions.FindAdminByFirstName(adminName);
            string expected = "No admin with that first name exists";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
        }

        [Test]
        public void _09Test_FindAdminByName_ExpectedOccurOnce_WhenAdminDetailDoesExist()
        {
            // Arrange
            string adminName = "Alfred";
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            var adminTwo = new Admin { AdminID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386", Role = "Admin" };
            Stack<Admin> stackAdmins = new Stack<Admin>();
            List<Admin> list = new List<Admin>();
            list.Add(admin);
            list.Add(adminTwo);
            foreach (var adm in list.Where(c => c.FirstName == adminName).OrderByDescending(c => c.AdminID).ToList())
            {
                stackAdmins.Push(adm);
            }
            _mockAdminsrepository.Setup(repo => repo.GetAdminByName(adminName)).Returns(stackAdmins);

            // Act
            string adminDetails = _adminOptions.FindAdminByFirstName(adminName);

            // Assert
            _mockAdminsrepository.Verify(c => c.GetAdminByName(adminName), Times.Once);
        }

        [Test]
        public void _10Test_FindAdminByName_ExpectedMoreThanOneJack_WhenAdminDetailDoesExist()
        {
            // Arrange
            string adminName = "Jack";
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            var adminTwo = new Admin { AdminID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386", Role = "Admin" };
            var adminThree = new Admin { AdminID = 9, FirstName = "Jack", Surname = "Fermin", Email = "Ferminj47@gmail.com", Username = "Ferminj47", Address = "100 Everton street, 6001", PhoneNumber = "0746277885", Role = "Admin" };
            Stack<Admin> stackAdmins = new Stack<Admin>();
            List<Admin> list = new List<Admin>();
            list.Add(admin);
            list.Add(adminTwo);
            list.Add(adminThree);
            foreach (var adm in list.Where(c => c.FirstName == adminName).OrderByDescending(c => c.AdminID).ToList())
            {
                stackAdmins.Push(adm);
            }
            _mockAdminsrepository.Setup(repo => repo.GetAdminByName(adminName)).Returns(stackAdmins);

            // Act
            string adminDetails = _adminOptions.FindAdminByFirstName(adminName);
            string expected = "First Name: Jack, Surname: Harrison";
            string expectedTwo = "First Name: Jack, Surname: Fermin";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
            Assert.IsTrue(adminDetails.Contains(expectedTwo));
        }

        [Test]
        public void _11Test_FindAdminByFullName_ExpectedFalse_WhenAdminDetailDoesExist()
        {
            // Arrange
            string adminName = "Jack";
            string adminSurname = "Harrison";
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            var adminTwo = new Admin { AdminID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386", Role = "Admin" };
            Stack<Admin> stackAdmins = new Stack<Admin>();
            List<Admin> list = new List<Admin>();
            list.Add(admin);
            list.Add(adminTwo);
            foreach (var adm in list.Where(c => c.FirstName == adminName && c.Surname == adminSurname).OrderByDescending(c => c.AdminID).ToList())
            {
                stackAdmins.Push(adm);
            }
            _mockAdminsrepository.Setup(repo => repo.GetAdminByName(adminName, adminSurname)).Returns(stackAdmins);

            // Act
            string adminDetails = _adminOptions.FindAdminByFullName(adminName, adminSurname);
            string expected = "No admin with that fullname exists";

            // Assert
            Assert.IsFalse(adminDetails.Contains(expected));
        }


        [Test]
        public void _12Test_FindAdminByFullName_ExpectedTrue_WhenAdminDetailDoesNotExist()
        {
            // Arrange
            string adminName = "Alfred";
            string adminSurname = "Savage";
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            var adminTwo = new Admin { AdminID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386", Role = "Admin" };
            Stack<Admin> stackAdmins = new Stack<Admin>();
            List<Admin> list = new List<Admin>();
            list.Add(admin);
            list.Add(adminTwo);
            foreach (var adm in list.Where(c => c.FirstName == adminName && c.Surname == adminSurname).OrderByDescending(c => c.AdminID).ToList())
            {
                stackAdmins.Push(adm);
            }
            _mockAdminsrepository.Setup(repo => repo.GetAdminByName(adminName, adminSurname)).Returns(stackAdmins);

            // Act
            string adminDetails = _adminOptions.FindAdminByFullName(adminName, adminSurname);
            string expected = "No admin with that fullname exists";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
        }

        [Test]
        public void _13Test_FindAdminByFullName_ExpectedTrue_WhenAdminNameIsEmpty()
        {
            // Arrange
            string adminName = "";
            string adminSurname = "";
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            var adminTwo = new Admin { AdminID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386", Role = "Admin" };
            Stack<Admin> stackAdmins = new Stack<Admin>();
            List<Admin> list = new List<Admin>();
            list.Add(admin);
            list.Add(adminTwo);
            foreach (var adm in list.Where(c => c.FirstName == adminName && c.Surname == adminSurname).OrderByDescending(c => c.AdminID).ToList())
            {
                stackAdmins.Push(adm);
            }
            _mockAdminsrepository.Setup(repo => repo.GetAdminByName(adminName, adminSurname)).Returns(stackAdmins);

            // Act
            string adminDetails = _adminOptions.FindAdminByFullName(adminName, adminSurname);
            string expected = "No admin with that fullname exists";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
        }

        [Test]
        public void _14Test_FindAdminByFullName_ExpectedTrue_WhenAdminFirstNameIsEmptyButSurnameIsNot()
        {
            // Arrange
            string adminName = "";
            string adminSurname = "Harrision";
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            var adminTwo = new Admin { AdminID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386", Role = "Admin" };
            Stack<Admin> stackAdmins = new Stack<Admin>();
            List<Admin> list = new List<Admin>();
            list.Add(admin);
            list.Add(adminTwo);
            foreach (var adm in list.Where(c => c.FirstName == adminName && c.Surname == adminSurname).OrderByDescending(c => c.AdminID).ToList())
            {
                stackAdmins.Push(adm);
            }
            _mockAdminsrepository.Setup(repo => repo.GetAdminByName(adminName, adminSurname)).Returns(stackAdmins);

            // Act
            string adminDetails = _adminOptions.FindAdminByFullName(adminName, adminSurname);
            string expected = "No admin with that fullname exists";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
        }

        [Test]
        public void _15Test_FindAdminByFullName_ExpectedOccurOnce_WhenAdminDetailDoesExist()
        {
            // Arrange
            string adminName = "Alfred";
            string adminSurname = "Bateman";
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            var adminTwo = new Admin { AdminID = 5, FirstName = "Zack", Surname = "Wake", Email = "Wakez007@gmail.com", Username = "Wakez007", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386", Role = "Admin" };
            Stack<Admin> stackAdmins = new Stack<Admin>();
            List<Admin> list = new List<Admin>();
            list.Add(admin);
            list.Add(adminTwo);
            foreach (var adm in list.Where(c => c.FirstName == adminName && c.Surname == adminSurname).OrderByDescending(c => c.AdminID).ToList())
            {
                stackAdmins.Push(adm);
            }
            _mockAdminsrepository.Setup(repo => repo.GetAdminByName(adminName, adminSurname)).Returns(stackAdmins);

            // Act
            string adminDetails = _adminOptions.FindAdminByFullName(adminName, adminSurname);

            // Assert
            _mockAdminsrepository.Verify(c => c.GetAdminByName(adminName, adminSurname), Times.Once);
        }

        [Test]
        public void _16Test_AddNewAdmin_ExpectedAddOccurOnce_WhenAdminDetailCorrectAndEmailDoesNotAlreadyExist()
        {
            // Arrange
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfEmailExists(admin.Email)).Returns(false);
            _mockAdminsrepository.Setup(repo => repo.AddEntity(admin)).Returns(true);

            // Act
            string adminDetails = _adminOptions.AddNewAdmin(admin);

            // Assert
            _mockAdminsrepository.Verify(c => c.AddEntity(admin), Times.Once);
        }

        [Test]
        public void _17Test_AddNewAdmin_ExpectedAddneverOccur_WhenAdminDetailCorrectAndEmailAlreadyExist()
        {
            // Arrange
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfEmailExists(admin.Email)).Returns(true);
            _mockAdminsrepository.Setup(repo => repo.AddEntity(admin)).Returns(true);

            // Act
            string adminDetails = _adminOptions.AddNewAdmin(admin);

            // Assert
            _mockAdminsrepository.Verify(c => c.AddEntity(admin), Times.Never);
        }

        [Test]
        public void _18Test_AddNewAdmin_ExpectedSuccessMessage_WhenAdminDetailCorrectAndEmailDoesNotAlreadyExist()
        {
            // Arrange
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfEmailExists(admin.Email)).Returns(false);
            _mockAdminsrepository.Setup(repo => repo.AddEntity(admin)).Returns(true);

            // Act
            string adminDetails = _adminOptions.AddNewAdmin(admin);
            string expected = "Admin has been added";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
        }

        [Test]
        public void _19Test_AddNewAdmin_ExpectedFailMessage_WhenAdminDetailCorrectAndEmailAlreadyExist()
        {
            // Arrange
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfEmailExists(admin.Email)).Returns(true);
            _mockAdminsrepository.Setup(repo => repo.AddEntity(admin)).Returns(true);

            // Act
            string adminDetails = _adminOptions.AddNewAdmin(admin);
            string expected = "Admin with that email already exists";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
        }

        [Test]
        public void _20Test_AddNewAdmin_ExpectedFailMessage_WhenAddingErrorOccurs()
        {
            // Arrange
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfEmailExists(admin.Email)).Returns(false);
            _mockAdminsrepository.Setup(repo => repo.AddEntity(admin)).Returns(false);

            // Act
            string adminDetails = _adminOptions.AddNewAdmin(admin);
            string expected = "An error occured, admin was not added";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
        }

        [Test]
        public void _21Test_UpdateAdmin_ExpectedOccurOnce_WhenUpdatingAdminCorrectly()
        {
            // Arrange
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfIdExists(admin.AdminID)).Returns(true);
            _mockAdminsrepository.Setup(repo => repo.UpdateEntity(admin)).Returns(true);

            // Act
            string adminDetails = _adminOptions.UpdateAdmin(admin);

            // Assert
            _mockAdminsrepository.Verify(c => c.UpdateEntity(admin), Times.Once);
            
        }

        [Test]
        public void _22Test_UpdateAdmin_ExpectedSuccessMessage_WhenUpdatingAdminCorrectly()
        {
            // Arrange
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfIdExists(admin.AdminID)).Returns(true);
            _mockAdminsrepository.Setup(repo => repo.UpdateEntity(admin)).Returns(true);

            // Act
            string adminDetails = _adminOptions.UpdateAdmin(admin);
            string expected = "Admin has been updated";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
        }

        [Test]
        public void _23Test_UpdateAdmin_ExpectedNeverOccur_WhenUpdatingAdminIdNotFound()
        {
            // Arrange
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfIdExists(admin.AdminID)).Returns(false);
            _mockAdminsrepository.Setup(repo => repo.UpdateEntity(admin)).Returns(false);

            // Act
            string adminDetails = _adminOptions.UpdateAdmin(admin);

            // Assert
            _mockAdminsrepository.Verify(c => c.UpdateEntity(admin), Times.Never);
        }

        [Test]
        public void _24Test_UpdateAdmin_ExpectedDoNotExist_WhenUpdatingAdminIDNotFound()
        {
            // Arrange
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfIdExists(admin.AdminID)).Returns(false);
            _mockAdminsrepository.Setup(repo => repo.UpdateEntity(admin)).Returns(false);

            // Act
            string adminDetails = _adminOptions.UpdateAdmin(admin);
            string expected = "Admin does not exist";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
        }

        [Test]
        public void _25Test_UpdateAdmin_ExpectedErrorOccured_WhenUpdatingAdminDoesNotExist()
        {
            // Arrange
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfIdExists(admin.AdminID)).Returns(true);
            _mockAdminsrepository.Setup(repo => repo.UpdateEntity(admin)).Returns(false);

            // Act
            string adminDetails = _adminOptions.UpdateAdmin(admin);
            string expected = "Error Occured, Admin was not updated";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
        }

        [Test]
        public void _26Test_RemoveAdmin_ExpectedOccurOnce_WhenUpdatingAdminCorrectly()
        {
            // Arrange
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfIdExists(admin.AdminID)).Returns(true);
            _mockAdminsrepository.Setup(repo => repo.DeleteRow(admin.AdminID)).Returns(true);

            // Act
            string adminDetails = _adminOptions.RemoveAdmin(admin.AdminID);
            

            // Assert
            _mockAdminsrepository.Verify(c => c.DeleteRow(admin.AdminID), Times.Once);

        }

        [Test]
        public void _27Test_RemoveAdmin_ExpectedDeletedMessage_WhenUpdatingAdminCorrectly()
        {
            // Arrange
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfIdExists(admin.AdminID)).Returns(true);
            _mockAdminsrepository.Setup(repo => repo.DeleteRow(admin.AdminID)).Returns(true);

            // Act
            string adminDetails = _adminOptions.RemoveAdmin(admin.AdminID);
            string expected = "Admin has been deleted";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
        }

        [Test]
        public void _28Test_RemoveAdmin_ExpectedNeverOccur_WhenUpdatingAdminIdNotFound()
        {
            // Arrange
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfIdExists(admin.AdminID)).Returns(false);
            _mockAdminsrepository.Setup(repo => repo.DeleteRow(admin.AdminID)).Returns(false);

            // Act
            string adminDetails = _adminOptions.RemoveAdmin(admin.AdminID);

            // Assert
            _mockAdminsrepository.Verify(c => c.DeleteRow(admin.AdminID), Times.Never);
        }

        [Test]
        public void _29Test_RemoveAdmin_ExpectedDoNotExist_WhenUpdatingAdminIDNotFound()
        {
            // Arrange
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfIdExists(admin.AdminID)).Returns(false);
            _mockAdminsrepository.Setup(repo => repo.DeleteRow(admin.AdminID)).Returns(false);

            // Act
            string adminDetails = _adminOptions.RemoveAdmin(admin.AdminID);
            string expected = "Admin ID does not exist";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
        }

        [Test]
        public void _30Test_RemoveAdmin_ExpectedErrorOccured_WhenUpdatingAdminDoesNotExist()
        {
            // Arrange
            var admin = new Admin { AdminID = 3, FirstName = "Jack", Surname = "Harrison", Email = "Harrionj01@gmail.com", Username = "Harrison01", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Admin" };
            _mockAdminsrepository.Setup(repo => repo.CheckIfIdExists(admin.AdminID)).Returns(true);
            _mockAdminsrepository.Setup(repo => repo.DeleteRow(admin.AdminID)).Returns(false);

            // Act
            string adminDetails = _adminOptions.RemoveAdmin(admin.AdminID);
            string expected = "Error Occured, Admin not removed";

            // Assert
            Assert.IsTrue(adminDetails.Contains(expected));
        }

    }
}
