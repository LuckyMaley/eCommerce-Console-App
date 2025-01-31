using MainCode.Models;
using MainCode.Repository;
using static MainCode.MainCodeStaticObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository.CustomerMenuOptions
{
    enum Gender
    {
        Men,
        Women
    }
    public class CartOptions
    {
        public void BrowseItems()
        {
            CultureInfo ci = new CultureInfo("en-za");
            Console.WriteLine("=====================Products===================");
            foreach (var item in products)
            {

                Product product = products[item.Key];
                Console.WriteLine($"ID: {product.ProductID}, Name: {product.Name}, Brand: {product.Brand}, Description: {product.Description}, Type: {product.Type}, Price: {product.Price.ToString("C", ci)}, Quantity: {product.StockQuantity}, Added Date: {product.ModifiedDate.ToString("dd MMMM yyyy HH:mm")}\n");
            }
        }

        public void BrowseItemsByType()
        {
            CultureInfo ci = new CultureInfo("en-za");
            Console.WriteLine("Please specify the type of products (M for male or F for female): ");
            string response = "";
            string type = "";
            int count = 0;
            do
            {
                response = Console.ReadLine();
                switch (response)
                {
                    case "M" or "m":
                        response = "M";
                        count++;
                        break;
                    case "F" or "f":
                        response = "F";
                        count++;
                        break;
                    default:
                        Console.WriteLine("Wrong option, please try again (M for male or F for female): ");
                        break;
                }
            }
            while (count == 0);

            type = response == "M" || response == "m" ? Gender.Men.ToString() : Gender.Women.ToString();
            StringBuilder sb = new StringBuilder();
            foreach (var item in products)
            {

                Product product = products[item.Key];
                if (product.Type == type)
                {
                    sb.AppendLine($"ID: {product.ProductID}, Name: {product.Name}, Brand: {product.Brand}, Description: {product.Description}, Type: {product.Type}, Price: {product.Price.ToString("C", ci)}, Quantity: {product.StockQuantity}, Added Date: {product.ModifiedDate.ToString("dd MMMM yyyy HH:mm")}\n");
                }
            }
            Console.WriteLine(sb.ToString());
        }
        public void BrowseItemsByCategory()
        {
            CultureInfo ci = new CultureInfo("en-za");
            CategoriesRepository categoriesRepository = new CategoriesRepository();
            List<Category> categories = categoriesRepository.ReadGetAllRows();
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("=========================Categories========================");
            categories.ForEach(b => sb.AppendLine($"ID: {b.CategoryID}, Name: {b.Name}"));
            Console.WriteLine(sb.ToString());
            sb.Clear();
            int count = 0;
            int result = 0;
            do
            {
                Console.WriteLine("Please select the category id: ");
                string response = Console.ReadLine();
                Console.WriteLine();
                bool valid = int.TryParse(response, out result);
                Category? category = null;
                if (valid)
                {
                    category = categoriesRepository.ReadGetAllRows().FirstOrDefault(c => c.CategoryID == result);
                }

                if (category != null)
                {
                    foreach (var item in products)
                    {

                        Product product = products[item.Key];
                        if (product.CategoryID == category.CategoryID)
                        {
                            sb.AppendLine($"ID: {product.ProductID}, Name: {product.Name}, Brand: {product.Brand}, Description: {product.Description}, Type: {product.Type}, Price: {product.Price.ToString("C", ci)}, Quantity: {product.StockQuantity}, Added Date: {product.ModifiedDate.ToString("dd MMMM yyyy HH:mm")}");
                            sb.AppendLine("");
                            count++;
                        }

                    }

                    if (count == 0)
                    {
                        Console.WriteLine("Category has no Products yet");
                    }
                }
                else
                {
                    Console.WriteLine("Please select a category that exists in the list");
                }


            }
            while (count == 0);
            Console.WriteLine(sb.ToString());
        }
        public void AddItemToCart()
        {
            Console.WriteLine("Enter product ID:");
            long productId;
            bool valid = long.TryParse(Console.ReadLine(), out productId);
            if (!valid)
            {
                Console.WriteLine("Wrong format of ID, please try again");
                return;
            }

            if (!products.ContainsKey(productId))
            {
                Console.WriteLine("Invalid product ID.");
                return;
            }

            Console.WriteLine("Enter quantity:");
            int quantity = int.Parse(Console.ReadLine());

            if (quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than zero.");
                return;
            }

            Product product = products[productId];
            cart += (product, quantity);
            Console.WriteLine("Item added to cart successfully!");
        }

        public void ViewCartItems()
        {
            Console.WriteLine(cart.ViewCart(products));
        }

        public string RemoveItemFromCart()
        {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("Enter product ID:");
            long productId;
            bool valid = long.TryParse(Console.ReadLine(), out productId);
            if (!valid)
            {
                sb.AppendLine("Wrong format of ID, please try again");
                return sb.ToString();
            }

            if (!products.ContainsKey(productId))
            {
                sb.AppendLine("Invalid product ID.");
                return sb.ToString();
            }

            Product product = products[productId];
            cart.RemoveItem(product);
            return sb.ToString();
        }

        public string ClearAllCartItems()
        {

            StringBuilder stringBuilder = new StringBuilder();
            char charResponse = ' ';
            string response = "";
            int count = 0;
            Console.WriteLine("Are you sure you want to clear all items in the cart? (Y/N)");
            do
            {
                response = Console.ReadLine();
                if (response == "y" || response == "Y")
                {
                    charResponse = 'Y';
                }
                else if (response == "n" || response == "N")
                {
                    charResponse = 'N';
                }


                switch (charResponse)
                {
                    case 'Y':
                        cart.ClearCart();
                        stringBuilder.AppendLine("cart cleared");
                        count++;
                        break;
                    case 'N':
                        stringBuilder.AppendLine("Clearing cart was cancelled");
                        count++;
                        break;
                    default:
                        Console.WriteLine("Wrong response, please try again with either Y or N: ");
                        break;
                }
            }
            while (count == 0);
            return stringBuilder.ToString();
        }

        public string ProceedToCheckOut(OrdersRepository ordersRepository, OrderDetailsRepository orderDetailsRepository, CustomersRepository cusRepository)
        {
            StringBuilder sb = new StringBuilder();
            float total = ShoppingCart.GetTotalPrice(products);
            if (total != 0f)
            {
                Console.WriteLine($"Total Price (incl. Vat): {total.ToString("C", new CultureInfo("en-za"))}");
                Console.WriteLine("Enter your shipping address - street number, streetname, postal code (press n to use your saved address):");
                string shippingAddress = Console.ReadLine();
                List<Order> orders = ordersRepository.ReadGetAllRows();
                List<OrderDetail> orderDetails = orderDetailsRepository.ReadGetAllRows();
                if (shippingAddress == "n" || shippingAddress == "N" || shippingAddress == "")
                {
                    Customer customer = cusRepository.ReadRowByID(Person.Id);
                    shippingAddress = customer.Address;
                }

                Console.WriteLine("Processing payment...");
                Thread.Sleep(2000);


                Shipping ship = StoreShippingDetails(shippingAddress);

                int newOrderID = orders.Max(c => c.OrderID) + 1;
                PaymentsRepository paymentsRepository = new PaymentsRepository();
                List<Payment> payments = paymentsRepository.ReadGetAllRows();
                int newID = payments.Max(c => c.PaymentID) + 1;
                Payment payment = new Payment()
                {
                    PaymentID = newID,
                    PaymentDate = DateTime.Now,
                    OrderID = newOrderID,
                    PaymentMethod = "Card",
                    Amount = total,
                    Status = "Paid"

                };
                Payment.PaymentsDataSet.Add(payment);
                paymentsRepository.ReadGetAllRows();
                Order order = new Order()
                {
                    OrderID = newOrderID,
                    CustomerID = Person.Id,
                    ShippingID = ship.ShippingID,
                    OrderDate = DateTime.Now,
                    TotalAmount = total,
                };
                Order.OrdersDataSet.Add(order);
                ordersRepository.ReadGetAllRows();
                List<Product> productsList = cart.GetCart(products);
                Dictionary<long, int> cartItems = cart.GetItems();
                int newDetailsID = orderDetails.Max(c => c.OrderDetailID) + 1;
                foreach (Product product in productsList)
                {

                    OrderDetail detail = new OrderDetail()
                    {
                        OrderDetailID = newDetailsID,
                        OrderID = newOrderID,
                        ProductID = product.ProductID,
                        Quantity = cartItems[product.ProductID],
                        UnitPrice = product.Price
                    };
                    OrderDetail.OrderDetailsDataSet.Add(detail);
                    orderDetailsRepository.ReadGetAllRows();
                    newDetailsID++;
                }

                sb.AppendLine("Your order number: " + newOrderID.ToString());
                sb.AppendLine("Payment successful!");
                sb.AppendLine("Thank you for your purchase.");
                cart.ClearCart();
            }
            else
            {
                sb.AppendLine("Cannot checkout with an empty cart");
            }
            return sb.ToString();
        }

        public Shipping StoreShippingDetails(string shippingAddress)
        {
            Console.WriteLine("Storing shipping details...");
            ShippingsRepository shippingsRepository = new ShippingsRepository();
            List<Shipping> shippings = shippingsRepository.ReadGetAllRows();
            int newID = shippings.Max(c => c.ShippingID) + 1;
            string TrackNum = Guid.NewGuid().ToString().Substring(0, 8);
            Shipping shipping = new Shipping()
            {
                ShippingID = newID,
                ShipDate = DateTime.Now.AddDays(3),
                ShipAddress = shippingAddress,
                ShipMethod = "Courier",
                TrackingNumber = TrackNum,
                DeliveryStatus = "Not Shipped Yet"
            };
            Shipping.ShippingsDataSet.Add(shipping);
            shippingsRepository.ReadGetAllRows();
            Console.WriteLine("Shipping details stored successfully!");
            Console.WriteLine($"Shipping Address: {shippingAddress}");
            Console.WriteLine($"Tracking Number: {TrackNum}");
            return shipping;
        }

        public string ViewOrder(OrdersRepository ordersRepository, OrderDetailsRepository orderDetailsRepository, CustomersRepository cusRepository, ProductsRepository prodRepository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int count = 0;
            Console.WriteLine("Please type in the order ID: ");
            string response = Console.ReadLine();
            List<Order> orders = ordersRepository.ReadGetAllRows();
            List<OrderDetail> orderDetails = orderDetailsRepository.ReadGetAllRows();
            List<Customer> allOfTheCustomers = cusRepository.ReadGetAllRows();
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();

            CultureInfo ci = new CultureInfo("en-za");

            int result = 0;
            if (response != null)
            {
                bool valid = int.TryParse(response, out result);
                if (valid)
                {
                    Order order = orders.FirstOrDefault(c => c.OrderID == result && c.CustomerID == Person.Id);
                    if (order != null)
                    {
                        count++;
                        stringBuilder.AppendLine("Your order: ");
                        stringBuilder.AppendLine($"ID: {order.OrderID}, Customer Name: {allOfTheCustomers.FirstOrDefault(z => z.CustomerID == order.CustomerID).FirstName + " " + allOfTheCustomers.FirstOrDefault(z => z.CustomerID == order.CustomerID).Surname}, Order Date: {order.OrderDate.ToString("dd MMMM yyyy HH:mm")}, Total Amount: {order.TotalAmount.ToString("C", ci)}");

                        stringBuilder.AppendLine("Items in your order: ");
                        List<OrderDetail> allOfTheOrderDetails = orderDetailsRepository.ReadGetAllRows().Where(c => c.OrderID == result).ToList();
                        allOfTheOrderDetails.ForEach(b => stringBuilder.AppendLine($"ID: {b.OrderDetailID}, Product Name: {allOfTheProducts.FirstOrDefault(z => z.ProductID == b.ProductID).Name}, Quantity: {b.Quantity}, Unit Price: {b.UnitPrice.ToString("C", ci)}"));
                    }
                    else
                    {
                        stringBuilder.AppendLine("order number not found");
                    }
                }
                else
                {
                    stringBuilder.AppendLine("order number not in the correct format");
                }
            }
            else
            {
                stringBuilder.AppendLine("Please enter an order id");
            }
            return stringBuilder.ToString();
        }
        public string ViewShippingStatus(ShippingsRepository repository, OrdersRepository ordersRepository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Order> orders = ordersRepository.ReadGetAllRows();
            List<Shipping> allOfTheShippings = repository.ReadGetAllRows();
            Console.WriteLine("Please type in the order ID: ");
            string response = Console.ReadLine();
            int result = 0;
            if (response != "")
            {
                bool valid = int.TryParse(response, out result);
                if (valid)
                {
                    Order order = orders.FirstOrDefault(c => c.OrderID == result && c.CustomerID == Person.Id);
                    if (order != null)
                    {
                        Shipping shipping = allOfTheShippings.FirstOrDefault(c => c.ShippingID == order.ShippingID);
                        stringBuilder.AppendLine($"ID: {shipping.ShippingID}, Shipping Date: {shipping.ShipDate.ToString("dd MMMM yyyy HH:mm")}, Shipping Method: {shipping.ShipMethod}, Tracking Number: {shipping.TrackingNumber}, Delivery Status: {shipping.DeliveryStatus}");
                    }
                    else
                    {
                        stringBuilder.AppendLine("order number not found");
                    }
                }
                else
                {
                    stringBuilder.AppendLine("order number not in the correct format, please try again");
                }
            }
            else
            {
                stringBuilder.AppendLine("Please enter an order id");
            }
            return stringBuilder.ToString();
        }

        public string AddItemToWishlist(WishlistsRepository repository, ProductsRepository prodRepository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Wishlist> allOfTheWishlists = repository.ReadGetAllRows();
            HashSet<Wishlist> hashSetWishilist = repository.GetCustWishlistItems(Person.Id);
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();

            Wishlist newWishlistItem = new Wishlist();
            int newID = allOfTheWishlists.Max(c => c.WishlistID) + 1;
            Console.WriteLine("Please enter product you want to add to wishlist: ");
            long productId = 0;
            bool validNum = long.TryParse(Console.ReadLine(), out productId);
            if (validNum)
            {
                newWishlistItem.WishlistID = newID;
                newWishlistItem.CustomerID = Person.Id;
                newWishlistItem.ProductID = productId;
                newWishlistItem.AddedDate = DateTime.Now;
                int count = 0;
                int countProd = 0;
                foreach (var item in allOfTheProducts)
                {
                    if (item.ProductID == newWishlistItem.ProductID)
                    {
                        countProd++;
                    }
                }
                if (countProd == 0)
                {
                    Console.WriteLine("Product does not exist, please try again");
                    return "";
                }
                foreach (var item in hashSetWishilist)
                {
                    if (item.ProductID == newWishlistItem.ProductID)
                    {
                        count++;
                    }
                }
                if (count > 0)
                {
                    stringBuilder.AppendLine("Cannot add, product is aleardy in wishlist");
                }
                else if (count == 0)
                {
                    repository.AddEntity(newWishlistItem);
                    stringBuilder.AppendLine("Product added to wishlist successfully");
                    repository.ReadGetAllRows();
                }

            }
            else
            {
                stringBuilder.AppendLine("Incorrect product Id format, please try again");
            }
            return stringBuilder.ToString();
        }

        public string ViewWishlist(WishlistsRepository repository, ProductsRepository prodRepository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Wishlist> allOfTheWishlists = repository.ReadGetAllRows();
            HashSet<Wishlist> hashSetWishilist = repository.GetCustWishlistItems(Person.Id);
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();

            if (hashSetWishilist.Count > 0)
            {
                foreach (var item in hashSetWishilist)
                {
                    string productName = "";
                    productName = allOfTheProducts.FirstOrDefault(z => z.ProductID == item.ProductID).Name;
                    stringBuilder.AppendLine($"ID: {item.WishlistID}, Product Name: {productName}, Added Date: {item.AddedDate.ToString("dd MMMM yyyy HH:mm")}");
                }
            }
            else
            {
                stringBuilder.AppendLine("No Items in wishlist");
            }
            return stringBuilder.ToString();
        }

        public string RemoveWishlistItem(WishlistsRepository repository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Wishlist> allOfTheWishlists = repository.ReadGetAllRows();
            Console.WriteLine("Please enter the wishlist ID: ");
            int wishId = 0;
            bool validNum = int.TryParse(Console.ReadLine(), out wishId);
            if (validNum)
            {
                bool exists = repository.CheckEntitycusID(wishId, Person.Id);
                if (exists)
                {
                    repository.DeleteRow(wishId);
                    stringBuilder.AppendLine("Wishlist Item Deleted Successfully");
                    repository.ReadGetAllRows();
                }
                else
                {
                    stringBuilder.AppendLine("Wishlist ID does not exist");
                }
            }
            else
            {
                stringBuilder.AppendLine("Wishlist Id not in the correct format, please try again");
            }
            return stringBuilder.ToString();
        }
    }
}
