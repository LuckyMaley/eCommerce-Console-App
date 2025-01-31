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

namespace UnitTests.RepositoryTests.categoryMenuOptionsTests
{
    [TestFixture]
    public class CategoryOptionsTests
    {
        Mock<CategoriesRepository> _mockCategoriesrepository;
        Mock<RepositoryBase<Category>> _mockCategoriesRepositoryBase;
        Mock<IRepository<Category>> _mockCategoriesIRepository;
        CategoryOptions _categoryOptions;

        [SetUp]
        public void SetUp()
        {
            _mockCategoriesrepository = new Mock<CategoriesRepository>();
            _mockCategoriesRepositoryBase = new Mock<RepositoryBase<Category>>();
            _mockCategoriesIRepository = new Mock<IRepository<Category>>();
            _categoryOptions = new CategoryOptions(_mockCategoriesrepository.Object, _mockCategoriesRepositoryBase.Object, _mockCategoriesIRepository.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockCategoriesrepository = null;
            _mockCategoriesRepositoryBase = null;
            _mockCategoriesIRepository = null;
            _categoryOptions = null;
        }

        [Test]
        public void _01Test_FindCategoryByID_ExpectedReadRowByIDNeverOccurs_WhenCatIdDoesNotExist()
        {
            // Arrange
            int catId = 11;
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfIdExists(catId)).Returns(false);
            _mockCategoriesrepository.Setup(repo => repo.ReadRowByID(catId)).Returns(category);

            // Act
            string categoryDetails = _categoryOptions.FindCategoryByID(catId);

            // Assert
            _mockCategoriesrepository.Verify(c => c.ReadRowByID(catId), Times.Never);
        }

        [Test]
        public void _02Test_FindCategoryByID_ExpectedReadRowByIDOccursOnce_WhenCatIdExists()
        {
            // Arrange
            int catId = 3;
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfIdExists(catId)).Returns(true);
            _mockCategoriesrepository.Setup(repo => repo.ReadRowByID(catId)).Returns(category);

            // Act
            string categoryDetails = _categoryOptions.FindCategoryByID(catId);

            // Assert
            _mockCategoriesrepository.Verify(c => c.ReadRowByID(catId), Times.Once);
        }

        [Test]
        public void _03Test_FindCategoryByID_ExpectedTrue_WhenCategoryDetailExist()
        {
            // Arrange
            int catId = 3;
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfIdExists(catId)).Returns(true);
            _mockCategoriesrepository.Setup(repo => repo.ReadRowByID(catId)).Returns(category);

            // Act
            string categoryDetails = _categoryOptions.FindCategoryByID(catId);

            // Assert
            Assert.IsTrue(categoryDetails.Contains(category.CategoryID.ToString()));
            Assert.IsTrue(categoryDetails.Contains(category.Name));
        }

        [Test]
        public void _04Test_FindCategoryByID_ExpectedFalse_WhenCategoryDetailDoesNotExist()
        {
            // Arrange
            int catId = 3;
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfIdExists(catId)).Returns(false);
            _mockCategoriesrepository.Setup(repo => repo.ReadRowByID(catId)).Returns(category);

            // Act
            string categoryDetails = _categoryOptions.FindCategoryByID(catId);

            // Assert
            Assert.IsFalse(categoryDetails.Contains(category.CategoryID.ToString()));
            Assert.IsFalse(categoryDetails.Contains(category.Name));
        }

        [Test]
        public void _05Test_FindCategoryByID_ExpectedTrue_WhenCategoryDetailDoesNotExist()
        {
            // Arrange
            int catId = 3;
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfIdExists(catId)).Returns(false);
            _mockCategoriesrepository.Setup(repo => repo.ReadRowByID(catId)).Returns(category);

            // Act
            string categoryDetails = _categoryOptions.FindCategoryByID(catId);
            string expected = "category does not exist, Please try again";

            // Assert
            Assert.IsTrue(categoryDetails.Contains(expected));
        }

        [Test]
        public void _06Test_FindCategoryByName_ExpectedFalse_WhenCategoryDetailDoesExist()
        {
            // Arrange
            string CategoryName = "Jeans";
            var Category = new Category { CategoryID = 8, Name = "Jeans" };
            var CategoryTwo = new Category { CategoryID = 9, Name = "Shorts" };
            Stack<Category> stackCategorys = new Stack<Category>();
            List<Category> list = new List<Category>();
            list.Add(Category);
            list.Add(CategoryTwo);
            foreach (var cat in list.Where(c => c.Name == CategoryName).OrderByDescending(c => c.CategoryID).ToList())
            {
                stackCategorys.Push(cat);
            }
            _mockCategoriesrepository.Setup(repo => repo.GetCategoryByName(CategoryName)).Returns(stackCategorys);

            // Act
            string CategoryDetails = _categoryOptions.FindCategoryByName(CategoryName);
            string expected = "No Category with that first name exists";

            // Assert
            Assert.IsFalse(CategoryDetails.Contains(expected));
        }


        [Test]
        public void _07Test_FindCategoryByName_ExpectedTrue_WhenCategoryDetailDoesNotExist()
        {
            // Arrange
            string CategoryName = "Make-Up";
            var Category = new Category { CategoryID = 8, Name = "Jeans" };
            var CategoryTwo = new Category { CategoryID = 9, Name = "Shorts" };
            Stack<Category> stackCategorys = new Stack<Category>();
            List<Category> list = new List<Category>();
            list.Add(Category);
            list.Add(CategoryTwo);
            foreach (var cat in list.Where(c => c.Name == CategoryName).OrderByDescending(c => c.CategoryID).ToList())
            {
                stackCategorys.Push(cat);
            }
            _mockCategoriesrepository.Setup(repo => repo.GetCategoryByName(CategoryName)).Returns(stackCategorys);

            // Act
            string CategoryDetails = _categoryOptions.FindCategoryByName(CategoryName);
            string expected = "No category with that name exists";

            // Assert
            Assert.IsTrue(CategoryDetails.Contains(expected));
        }

        [Test]
        public void _08Test_FindCategoryByName_ExpectedTrue_WhenCategoryNameIsEmpty()
        {
            // Arrange
            string CategoryName = "";
            var Category = new Category { CategoryID = 8, Name = "Jeans" };
            var CategoryTwo = new Category { CategoryID = 9, Name = "Shorts" };
            Stack<Category> stackCategorys = new Stack<Category>();
            List<Category> list = new List<Category>();
            list.Add(Category);
            list.Add(CategoryTwo);
            foreach (var cat in list.Where(c => c.Name == CategoryName).OrderByDescending(c => c.CategoryID).ToList())
            {
                stackCategorys.Push(cat);
            }
            _mockCategoriesrepository.Setup(repo => repo.GetCategoryByName(CategoryName)).Returns(stackCategorys);

            // Act
            string CategoryDetails = _categoryOptions.FindCategoryByName(CategoryName);
            string expected = "No category with that name exists";

            // Assert
            Assert.IsTrue(CategoryDetails.Contains(expected));
        }

        [Test]
        public void _09Test_FindCategoryByName_ExpectedOccurOnce_WhenCategoryDetailDoesExist()
        {
            // Arrange
            string CategoryName = "Jeans";
            var Category = new Category { CategoryID = 8, Name = "Jeans" };
            var CategoryTwo = new Category { CategoryID = 9, Name = "Shorts" };
            Stack<Category> stackCategorys = new Stack<Category>();
            List<Category> list = new List<Category>();
            list.Add(Category);
            list.Add(CategoryTwo);
            foreach (var cat in list.Where(c => c.Name == CategoryName).OrderByDescending(c => c.CategoryID).ToList())
            {
                stackCategorys.Push(cat);
            }
            _mockCategoriesrepository.Setup(repo => repo.GetCategoryByName(CategoryName)).Returns(stackCategorys);

            // Act
            string CategoryDetails = _categoryOptions.FindCategoryByName(CategoryName);

            // Assert
            _mockCategoriesrepository.Setup(c => c.GetCategoryByName(CategoryName));
        }

        [Test]
        public void _10Test_FindCategoriesByName_ExpectedOccurOnce_WhenCategoriesDetailDoesNotExist()
        {
            // Arrange
            string catName = "Make-Up";
            var Category = new Category { CategoryID = 8, Name = "Jeans" };
            var CategoryTwo = new Category { CategoryID = 9, Name = "Shorts" };
            Stack<Category> stackCategorys = new Stack<Category>();
            List<Category> list = new List<Category>();
            list.Add(Category);
            list.Add(CategoryTwo);
            foreach (var cat in list.Where(c => c.Name == catName).OrderByDescending(c => c.CategoryID).ToList())
            {
                stackCategorys.Push(cat);
            }
            _mockCategoriesrepository.Setup(repo => repo.GetCategoryByName(catName)).Returns(stackCategorys);

            // Act
            string categoryDetails = _categoryOptions.FindCategoryByName(catName);

            // Assert
            _mockCategoriesrepository.Verify(c => c.GetCategoryByName(catName), Times.Once);
        }

        [Test]
        public void _11Test_AddNewCategory_ExpectedAddOccurOnce_WhenCategoryDetailCorrectAndNameDoesNotAlreadyExist()
        {
            // Arrange
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfNameExists(category.Name)).Returns(false);
            _mockCategoriesrepository.Setup(repo => repo.AddEntity(category)).Returns(true);

            // Act
            string categoryDetails = _categoryOptions.AddNewCategory(category);

            // Assert
            _mockCategoriesrepository.Verify(c => c.AddEntity(category), Times.Once);
        }

        [Test]
        public void _12Test_AddNewCategory_ExpectedAddneverOccur_WhenCategoryDetailCorrectAndNameAlreadyExist()
        {
            // Arrange
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfNameExists(category.Name)).Returns(true);
            _mockCategoriesrepository.Setup(repo => repo.AddEntity(category)).Returns(true);

            // Act
            string categoryDetails = _categoryOptions.AddNewCategory(category);

            // Assert
            _mockCategoriesrepository.Verify(c => c.AddEntity(category), Times.Never);
        }

        [Test]
        public void _13Test_AddNewCategory_ExpectedSuccessMessage_WhenCategoryDetailCorrectAndNameDoesNotAlreadyExist()
        {
            // Arrange
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfNameExists(category.Name)).Returns(false);
            _mockCategoriesrepository.Setup(repo => repo.AddEntity(category)).Returns(true);

            // Act
            string categoryDetails = _categoryOptions.AddNewCategory(category);
            string expected = "Category has been added";

            // Assert
            Assert.IsTrue(categoryDetails.Contains(expected));
        }

        [Test]
        public void _14Test_AddNewCategory_ExpectedFailMessage_WhenCategoryDetailCorrectAndNameAlreadyExist()
        {
            // Arrange
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfNameExists(category.Name)).Returns(true);
            _mockCategoriesrepository.Setup(repo => repo.AddEntity(category)).Returns(true);

            // Act
            string categoryDetails = _categoryOptions.AddNewCategory(category);
            string expected = "Cannot add category that already exists";

            // Assert
            Assert.IsTrue(categoryDetails.Contains(expected));
        }

        [Test]
        public void _15Test_AddNewCategory_ExpectedFailMessage_WhenAddingErrorOccurs()
        {
            // Arrange
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfNameExists(category.Name)).Returns(false);
            _mockCategoriesrepository.Setup(repo => repo.AddEntity(category)).Returns(false);

            // Act
            string categoryDetails = _categoryOptions.AddNewCategory(category);
            string expected = "Error, cannot add category";

            // Assert
            Assert.IsTrue(categoryDetails.Contains(expected));
        }

        [Test]
        public void _16Test_UpdateCategory_ExpectedOccurOnce_WhenUpdatingCategoryCorrectly()
        {
            // Arrange
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfIdExists(category.CategoryID)).Returns(true);
            _mockCategoriesrepository.Setup(repo => repo.UpdateEntity(category)).Returns(true);

            // Act
            string categoryDetails = _categoryOptions.UpdateCategory(category);

            // Assert
            _mockCategoriesrepository.Verify(c => c.UpdateEntity(category), Times.Once);

        }

        [Test]
        public void _17Test_UpdateCategory_ExpectedSuccessMessage_WhenUpdatingCategoryCorrectly()
        {
            // Arrange
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfIdExists(category.CategoryID)).Returns(true);
            _mockCategoriesrepository.Setup(repo => repo.UpdateEntity(category)).Returns(true);

            // Act
            string categoryDetails = _categoryOptions.UpdateCategory(category);
            string expected = "Category has been updated";

            // Assert
            Assert.IsTrue(categoryDetails.Contains(expected));
        }

        [Test]
        public void _18Test_UpdateCategory_ExpectedNeverOccur_WhenUpdatingCategoryIdNotFound()
        {
            // Arrange
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfIdExists(category.CategoryID)).Returns(false);
            _mockCategoriesrepository.Setup(repo => repo.UpdateEntity(category)).Returns(false);

            // Act
            string categoryDetails = _categoryOptions.UpdateCategory(category);

            // Assert
            _mockCategoriesrepository.Verify(c => c.UpdateEntity(category), Times.Never);
        }

        [Test]
        public void _19Test_UpdateCategory_ExpectedDoNotExist_WhenUpdatingCategoryIDNotFound()
        {
            // Arrange
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfIdExists(category.CategoryID)).Returns(false);
            _mockCategoriesrepository.Setup(repo => repo.UpdateEntity(category)).Returns(false);

            // Act
            string categoryDetails = _categoryOptions.UpdateCategory(category);
            string expected = "Category does not exist";

            // Assert
            Assert.IsTrue(categoryDetails.Contains(expected));
        }

        [Test]
        public void _20Test_UpdateCategory_ExpectedErrorOccured_WhenUpdatingCategoryDoesNotExist()
        {
            // Arrange
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfIdExists(category.CategoryID)).Returns(true);
            _mockCategoriesrepository.Setup(repo => repo.UpdateEntity(category)).Returns(false);

            // Act
            string categoryDetails = _categoryOptions.UpdateCategory(category);
            string expected = "Error Occured, Category was not updated";

            // Assert
            Assert.IsTrue(categoryDetails.Contains(expected));
        }

        [Test]
        public void _21Test_RemoveCategory_ExpectedOccurOnce_WhenUpdatingCategoryCorrectly()
        {
            // Arrange
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfIdExists(category.CategoryID)).Returns(true);
            _mockCategoriesrepository.Setup(repo => repo.DeleteRow(category.CategoryID)).Returns(true);

            // Act
            string categoryDetails = _categoryOptions.RemoveCategory(category.CategoryID);


            // Assert
            _mockCategoriesrepository.Verify(c => c.DeleteRow(category.CategoryID), Times.Once);

        }

        [Test]
        public void _22Test_RemoveCategory_ExpectedDeletedMessage_WhenUpdatingCategoryCorrectly()
        {
            // Arrange
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfIdExists(category.CategoryID)).Returns(true);
            _mockCategoriesrepository.Setup(repo => repo.DeleteRow(category.CategoryID)).Returns(true);

            // Act
            string categoryDetails = _categoryOptions.RemoveCategory(category.CategoryID);
            string expected = "Category has been deleted";

            // Assert
            Assert.IsTrue(categoryDetails.Contains(expected));
        }

        [Test]
        public void _23Test_RemoveCategory_ExpectedNeverOccur_WhenUpdatingCategoryIdNotFound()
        {
            // Arrange
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfIdExists(category.CategoryID)).Returns(false);
            _mockCategoriesrepository.Setup(repo => repo.DeleteRow(category.CategoryID)).Returns(false);

            // Act
            string categoryDetails = _categoryOptions.RemoveCategory(category.CategoryID);

            // Assert
            _mockCategoriesrepository.Verify(c => c.DeleteRow(category.CategoryID), Times.Never);
        }

        [Test]
        public void _24Test_RemoveCategory_ExpectedDoNotExist_WhenUpdatingCategoryIDNotFound()
        {
            // Arrange
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfIdExists(category.CategoryID)).Returns(false);
            _mockCategoriesrepository.Setup(repo => repo.DeleteRow(category.CategoryID)).Returns(false);

            // Act
            string categoryDetails = _categoryOptions.RemoveCategory(category.CategoryID);
            string expected = "Category ID does not exist";

            // Assert
            Assert.IsTrue(categoryDetails.Contains(expected));
        }

        [Test]
        public void _25Test_RemoveCategory_ExpectedErrorOccured_WhenUpdatingCategoryDoesNotExist()
        {
            // Arrange
            var category = new Category { CategoryID = 8, Name = "Jeans" };
            _mockCategoriesrepository.Setup(repo => repo.CheckIfIdExists(category.CategoryID)).Returns(true);
            _mockCategoriesrepository.Setup(repo => repo.DeleteRow(category.CategoryID)).Returns(false);

            // Act
            string categoryDetails = _categoryOptions.RemoveCategory(category.CategoryID);
            string expected = "Error Occured, Category not removed";

            // Assert
            Assert.IsTrue(categoryDetails.Contains(expected));
        }
    }
}
