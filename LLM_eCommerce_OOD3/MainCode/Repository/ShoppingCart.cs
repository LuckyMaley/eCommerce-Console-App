using MainCode.Models;
using static MainCode.MainCodeStaticObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository
{
    public class ShoppingCart
    {
        protected delegate float CalcDelegate(float num1, int num2, float z);

        protected const float vat = 0.15f;

        private protected static Dictionary<long, int> items = new Dictionary<long, int>();

        public void AddItem(Product product, int quantity)
        {
            if (quantity <= 0)
            {
                Console.WriteLine("Quantity Cannot be less than 1");
                return;
            }
            if (items.ContainsKey(product.ProductID))
            {
                items[product.ProductID] = quantity;
            }
            else
            {
                items.Add(product.ProductID, quantity);
            }
        }

        public void RemoveItem(Product product)
        {
            if (product != null)
            {
                if (items.ContainsKey(product.ProductID))
                {
                    items.Remove(product.ProductID);
                    Console.WriteLine("Item removed from cart successfully");
                }
                else
                {
                    Console.WriteLine("Item not found in cart.");
                }
            }
            else
            {
                Console.WriteLine("That product does not exist, please try again");
            }
        }

        public string ViewCart(Dictionary<long, Product> products)
        {
            StringBuilder sb = new StringBuilder();
            if (items.Count == 0)
            {
                sb.AppendLine("Your cart is empty.");
                return sb.ToString();
            }

            CalcDelegate calcFormula = (x, y, z) =>
            {
                float temp = x * y;
                return x * y + temp * z;
            };
            if (products.Count == 0)
            {
                sb.AppendLine("There is no product catalog");
                return sb.ToString();
            }


            Console.WriteLine("Your Shopping Cart:");
            try
            {


                foreach (var item in items)
                {

                    Product product = products[item.Key];

                    sb.AppendLine($"Product ID: {item.Key}, Name: {product.Name}, Quantity: {item.Value}, Total Price: {calcFormula.Invoke(product.Price, item.Value, vat)}");
                }
            }
            catch (KeyNotFoundException ex)
            {
                sb.AppendLine("Error, Exception occured: " + ex.ToString());
            }

            return sb.ToString();
        }

        public Dictionary<long, int> GetItems() { return items; }
        public List<Product> GetCart(Dictionary<long, Product> products)
        {
            List<Product> prodList = new List<Product>();
            foreach (var item in items)
            {
                if (products.ContainsKey(item.Key))
                {
                    Product product = products[item.Key];
                    prodList.Add(product);
                }
            }
            return prodList;
        }
        public static ShoppingCart operator +(ShoppingCart cart, (Product product, int quantity) item)
        {
            cart.AddItem(item.product, item.quantity);


            return cart;
        }

        public static float GetTotalPrice(Dictionary<long, Product> _products)
        {
            CalcDelegate calcFormula = (x, y, z) =>
            {
                float temp = x * y;
                return x * y + temp * z;
            };
            float totalPrice = 0;
            foreach (var item in items)
            {
                if (_products.Count != 0)
                {
                    Product product = _products[item.Key];
                    totalPrice += calcFormula.Invoke(product.Price, item.Value, vat);
                }

            }
            return totalPrice;
        }

        public void ClearCart()
        {
            if (items.Count > 0)
            {
                items.Clear();
            }
            else
            {
                Console.WriteLine("Nothing cleared, cart is already empty");
            }
        }

        public static void GetCartNumItems() => Console.WriteLine("Cart Items: {0} Qty: {1} Total Price (incl. Vat): {2}", items.Distinct().Count(), items.Values.Sum(), GetTotalPrice(products).ToString("C", new CultureInfo("en-za")));
    }
}
