using MainCode.Models;
using MainCode.Repository;
using NUnit;
using NUnit.Framework;

namespace UnitTests.RepositoryTests
{
    [TestFixture]
    public class ShoppingCartTests
    {
        private Dictionary<long, int> items;
        private Product product;
        private Product productTwo;
        private Product productThree;
        private ShoppingCart cart;

        [SetUp]
        public void Setup()
        {
            items = new Dictionary<long, int>();
            product = new Product() { ProductID = 2, Name = "Adidas Yeezy", Brand = "Adidas", Description = "Adidas Yeezy sneakers for summer wear", Type = "Women", Price = 2700.99f, CategoryID = 1, StockQuantity = 100, ModifiedDate = new DateTime(2017, 1, 22, 15, 35, 10) };
            productTwo = new Product { ProductID = 3, Name = "Puma T-shirt", Brand = "Puma", Description = "Puma T-shirt for the summer and track wear", Type = "Women", Price = 900.99f, CategoryID = 3, StockQuantity = 100, ModifiedDate = new DateTime(2019, 10, 12, 12, 45, 00) };
            productThree = new Product { ProductID = 5, Name = "Redbat Jean", Brand = "Redbat", Description = "Redbat Jean for casual wear", Type = "Men", Price = 799.99f, CategoryID = 8, StockQuantity = 100, ModifiedDate = new DateTime(2018, 2, 12, 21, 47, 50) };
            cart = new ShoppingCart();
        }

        [TearDown]
        public void Teardown()
        {
            items = null;
            product = null;
            productTwo = null;
            productThree = null;
            cart.GetItems().Clear();
        }


        [Test]
        public void _01Test_AddItemsToCart_Expecting7_WhenAddedProductWithQty7()
        {
            //Arrange
            int qty = 7;


            //Act
            int expectedQty = 7;
            cart.AddItem(product, qty);
            items = cart.GetItems();
            int actualQty = items[product.ProductID];

            //Assert
            Assert.AreEqual(expectedQty, actualQty);

        }

        [Test]
        public void _02Test_AddItemsToCart_Expecting1_WhenAddedProducIsDoneTwiceWithDifferentQty7Then1()
        {
            //Arrange
            int qty = 7;


            //Act
            int expectedQty = 1;
            cart.AddItem(product, qty);
            qty = 1;
            cart.AddItem(product, qty);
            items = cart.GetItems();
            int actualQty = items[product.ProductID];

            //Assert
            Assert.AreEqual(expectedQty, actualQty);

        }

        [Test]
        public void _03Test_AddItemsToCart_ExpectingNotNull_WhenProductWithQtyIsAdded()
        {
            //Arrange
            int qty = 6;

            //Act
            cart.AddItem(product, qty);
            items = cart.GetItems();

            //Assert
            Assert.NotNull(items);

        }

        [Test]
        public void _04Test_AddItemsToCart_ExpectingTrue_WhenProductIsAdded()
        {
            //Arrange
            int qty = 6;

            //Act
            cart.AddItem(product, qty);
            items = cart.GetItems();
            bool actual = items.ContainsKey(product.ProductID);

            //Assert
            Assert.IsTrue(actual);

        }


        [Test]
        public void _05Test_AddItemsToCart_ExpectingProdWithID2_When2ProductsAdded()
        {
            //Arrange

            int qty = 2;
            int qtyTwo = 4;

            //Act
            cart.AddItem(product, qty);
            cart.AddItem(productTwo, qtyTwo);
            items = cart.GetItems();
            bool expected = true;
            bool actual = items.ContainsKey(productTwo.ProductID);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void _06Test_AddItemsToCart_ExpectingCount3_When3ProductsAdded()
        {
            //Arrange
            int qty = 2;
            int qtyTwo = 4;
            int qtyThree = 1;

            //Act
            int expectedQty = 3;
            cart.AddItem(product, qty);
            cart.AddItem(productTwo, qtyTwo);
            cart.AddItem(productThree, qtyThree);
            items = cart.GetItems();

            //Assert
            Assert.AreEqual(expectedQty, items.Count);

        }

        [Test]
        public void _07Test_RemoveItemsFromCart_ExpectingFalse_WhenCheckingIfProdId3ExistsInCart()
        {
            //Arrange
            int qty = 2;
            int qtyTwo = 4;
            int qtyThree = 1;

            //Act
            cart.AddItem(product, qty);
            cart.AddItem(productTwo, qtyTwo);
            cart.AddItem(productThree, qtyThree);

            cart.RemoveItem(productTwo);
            items = cart.GetItems();
            bool actual = items.ContainsKey(productTwo.ProductID);

            //Assert
            Assert.IsFalse(actual);

        }

        [Test]
        public void _08Test_RemoveItemsFromCart_Expecting2_WhenProductID2Removed()
        {
            //Arrange
            int qty = 2;
            int qtyTwo = 4;
            int qtyThree = 1;

            //Act
            cart.AddItem(product, qty);
            cart.AddItem(productTwo, qtyTwo);
            cart.AddItem(productThree, qtyThree);
            cart.RemoveItem(product);
            items = cart.GetItems();
            int expected = 2;
            int actual = items.Count;

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void _09Test_RemoveItemsFromCart_Expecting3Products_WhenRemovingProductThatIsNotInCart()
        {
            //Arrange
            int qty = 2;
            int qtyTwo = 4;
            int qtyThree = 1;
            Product productFour = new Product { ProductID = 99875425, Name = "Gant shirt", Brand = "Gant", Description = "Gant shirt for formal wear", Type = "Men", Price = 1100.99f, CategoryID = 4, StockQuantity = 100, ModifiedDate = new DateTime(2022, 11, 10, 10, 40, 00) };

            //Act
            cart.AddItem(product, qty);
            cart.AddItem(productTwo, qtyTwo);
            cart.AddItem(productThree, qtyThree);
            cart.RemoveItem(productFour);
            items = cart.GetItems();
            int expected = 3;
            int actual = items.Count;

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void _10Test_RemoveItemsFromCart_Expecting3Products_WhenRemovingProductThatIsNullNotInCart()
        {
            //Arrange
            int qty = 2;
            int qtyTwo = 4;
            int qtyThree = 1;
            Product productFour = null;

            //Act
            cart.AddItem(product, qty);
            cart.AddItem(productTwo, qtyTwo);
            cart.AddItem(productThree, qtyThree);
            cart.RemoveItem(productFour);
            items = cart.GetItems();
            int expected = 3;
            int actual = items.Count;

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void _11Test_GetItems_ExpectingDictItems_WhenGettingFromCart()
        {
            //Arrange
            int qty = 2;
            int qtyTwo = 4;
            int qtyThree = 1;

            //Act
            cart.AddItem(product, qty);
            cart.AddItem(productTwo, qtyTwo);
            cart.AddItem(productThree, qtyThree);
            items = cart.GetItems();

            //Assert
            Assert.IsNotEmpty(items);

        }

        [Test]
        public void _12Test_ViewItemsFromCart_ExpectingTrue_WhenProductsDictIsEmptyAndExceptionCatched()
        {
            //Arrange
            Dictionary<long, Product> products = new Dictionary<long, Product>();
            int qty = 2;
            int qtyTwo = 4;
            int qtyThree = 1;

            //Act

            cart.AddItem(product, qty);
            cart.AddItem(productTwo, qtyTwo);
            cart.AddItem(productThree, qtyThree);
            string output = cart.ViewCart(products);
            bool actual = output.Contains("There is no product catalog");
            bool expected = true;



            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void _13Test_GetItemsFromCart_ExpectingEmptyDict_WhenNoItemsInCart()
        {
            //Arrange


            //Act
            items = cart.GetItems();

            //Assert
            Assert.IsEmpty(items);

        }

        [Test]
        public void _14Test_GetItemsFromCart_ExpectingEmptyListProducts_WhenProductsDictIsEmpty()
        {
            //Arrange
            Dictionary<long, Product> products = new Dictionary<long, Product>();

            //Act
            List<Product> prodCartlist = cart.GetCart(products);

            //Assert
            Assert.IsEmpty(prodCartlist);

        }

        [Test]
        public void _15Test_GetItemsFromCart_ExpectingListOfAllTheProducts_WhenProductsDictHasProducts()
        {
            //Arrange
            Dictionary<long, Product> products = new Dictionary<long, Product>();
            ProductsRepository prodRepository = new ProductsRepository();
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();
            foreach (Product product in allOfTheProducts)
            {
                products.Add(product.ProductID, product);
            }

            int qty = 2;
            int qtyTwo = 4;
            int qtyThree = 1;

            //Act
            cart.AddItem(product, qty);
            cart.AddItem(productTwo, qtyTwo);
            cart.AddItem(productThree, qtyThree);
            cart.GetItems();

            List<Product> prodCartlist = cart.GetCart(products);

            //Assert
            Assert.IsNotEmpty(prodCartlist);

        }


        [Test]
        public void _16Test_GetOverloadingAddingItemsFromCart_Expecting4_WhenAddingProductQuantity()
        {
            //Arrange
            int qty = 3;

            //Act
            cart += (product, qty);
            int expected = 3;
            items = cart.GetItems();
            int actual = items[product.ProductID];

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void _17Test_GetOverloadingAddingItemsFromCart_Expecting2Product_WhenAddingThirdProductWithQuantityLessThan1()
        {

            //Arrange
            int qty = 3;
            int qtyTwo = -1;
            Product productFour = new Product { ProductID = 99875425, Name = "Gant shirt", Brand = "Gant", Description = "Gant shirt for formal wear", Type = "Men", Price = 1100.99f, CategoryID = 4, StockQuantity = 100, ModifiedDate = new DateTime(2022, 11, 10, 10, 40, 00) };


            //Act
            cart.AddItem(product, qty);
            cart.AddItem(productTwo, qtyTwo);
            cart.AddItem(productThree, qty);
            int expected = 2;
            items = cart.GetItems();
            int actual = items.Count;

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void _18Test_GetOverloadingAddingItemsFromCart_Expecting4_WhenAddingTheSameProductWithDifferentQuantity()
        {

            //Arrange
            int qty = 5;
            int qtyTwo = 4;

            //Act
            cart.AddItem(product, qty);
            cart.AddItem(product, qtyTwo);
            int expected = 4;
            items = cart.GetItems();
            int actual = items.Values.Sum();

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [TestCase(2, 4, 4)]
        [TestCase(7, 3, 3)]
        [TestCase(12, 20, 20)]
        [TestCase(5, 2, 2)]
        [TestCase(8, 9, 9)]
        public void _19Test_GetOverloadingAddingItemsFromCart_ExpectingTestCaseOutput_WhenAddingTheSameProductWithDifferentQuantity(int qty, int qtyTwo, int expected)
        {

            //Arrange
            int quantity = qty;
            int quantityTwo = qtyTwo;

            //Act
            cart.AddItem(product, quantity);
            cart.AddItem(product, quantityTwo);
            items = cart.GetItems();
            int actual = items.Values.Sum();

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void _20Test_GetTotalFromCart_ExpectingGreaterThan0_WhenProductDictHasProducts()
        {
            //Arrange
            Dictionary<long, Product> products = new Dictionary<long, Product>();
            ProductsRepository prodRepository = new ProductsRepository();
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();
            foreach (Product product in allOfTheProducts)
            {
                products.Add(product.ProductID, product);
            }

            int qty = 2;
            int qtyTwo = 4;
            int qtyThree = 1;

            //Act
            cart.AddItem(product, qty);
            cart.AddItem(productThree, qtyThree);
            items = cart.GetItems();

            float prodTotalPrice = ShoppingCart.GetTotalPrice(products);

            //Assert
            Assert.Greater(prodTotalPrice, 0);

        }

        [Test]
        public void _21Test_GetTotalFromCart_Expecting0_WhenProductDictHasNoProducts()
        {
            //Arrange
            Dictionary<long, Product> products = new Dictionary<long, Product>();

            int qty = 2;
            int qtyTwo = 4;
            int qtyThree = 1;

            //Act
            cart.AddItem(product, qty);
            cart.AddItem(productThree, qtyThree);
            items = cart.GetItems();
            int expected = 0;
            float prodTotalPrice = ShoppingCart.GetTotalPrice(products);

            //Assert
            Assert.AreEqual(expected, prodTotalPrice);

        }

        [Test]
        public void _22Test_GetTotalFromCart_Expecting0_WhenCartHasNoItems()
        {
            //Arrange
            Dictionary<long, Product> products = new Dictionary<long, Product>();
            ProductsRepository prodRepository = new ProductsRepository();
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();
            foreach (Product product in allOfTheProducts)
            {
                products.Add(product.ProductID, product);
            }

            int qty = 2;
            int qtyTwo = 4;
            int qtyThree = 1;

            //Act
            int expected = 0;
            float prodTotalPrice = ShoppingCart.GetTotalPrice(products);

            //Assert
            Assert.AreEqual(expected, prodTotalPrice);

        }
    }
}
