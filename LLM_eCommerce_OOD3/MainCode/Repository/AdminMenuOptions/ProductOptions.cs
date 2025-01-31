using MainCode.Models;
using MainCode.Repository;
using MainCode.Repository.CustomerMenuOptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository.AdminMenuOptions
{
    public class ProductOptions
    {
        public string FindProductByID(ProductsRepository productsRepository)
        {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("Please enter the product ID");
            int prodID = 0;
            bool result = int.TryParse(Console.ReadLine(), out prodID);
            if (result)
            {
                bool valid = productsRepository.CheckIfIdExists(prodID);
                if (valid)
                {
                    var product = productsRepository.ReadRowByID(prodID);
                    CultureInfo ci = new CultureInfo("en-za");
                    sb.AppendLine($"ID: {product.ProductID}, Name: {product.Name}, Brand: {product.Brand}, Description: {product.Description}, Type: {product.Type}, Price: {product.Price.ToString("C", ci)}, Quantity: {product.StockQuantity}, Added Date: {product.ModifiedDate.ToString("dd MMMM yyyy HH:mm")}");
                }
                else
                {
                    sb.AppendLine("Product does not exist, Please try again");
                }
            }
            else
            {
                sb.AppendLine("Please make sure that you insert a number for the id");
            }
            return sb.ToString();
        }

        public string FindProductByName(ProductsRepository productsRepository)
        {
            CultureInfo ci = new CultureInfo("en-za");
            Console.WriteLine("Please enter product name: ");
            string prodName = Console.ReadLine();

            StringBuilder stringBuilder = new StringBuilder();
            Stack<Product> products = productsRepository.GetProductByName(prodName);
            if (products.Count > 0)
            {
                while (products.Count != 0)
                {
                    var product = products.Pop();
                    stringBuilder.AppendLine($"ID: {product.ProductID}, Name: {product.Name}, Brand: {product.Brand}, Description: {product.Description}, Type: {product.Type}, Price: {product.Price.ToString("C", ci)}, Quantity: {product.StockQuantity}, Added Date: {product.ModifiedDate.ToString("dd MMMM yyyy HH:mm")}");
                }
            }
            else
            {
                stringBuilder.AppendLine("No product with that name exists");
            }
            return stringBuilder.ToString();
        }

        public string BrowseProductByType(ProductsRepository repository)
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
            List<Product> allOfTheProducts = repository.ReadGetAllRows();
            foreach (var product in allOfTheProducts)
            {
                if (product.Type == type)
                {
                    sb.AppendLine($"ID: {product.ProductID}, Name: {product.Name}, Brand: {product.Brand}, Description: {product.Description}, Type: {product.Type}, Price: {product.Price.ToString("C", ci)}, Quantity: {product.StockQuantity}, Added Date: {product.ModifiedDate.ToString("dd MMMM yyyy HH:mm")}\n");
                }
            }
            return sb.ToString();
        }

        public string AddNewProduct(ProductsRepository productsRepository)
        {
            Product product = new Product();
            List<Product> products = productsRepository.ReadGetAllRows();
            long newID = products.Max(c => c.ProductID) + 1;


            string name = "";
            while (name.Trim() == "")
            {
                Console.WriteLine("Please enter product name: ");
                name = Console.ReadLine();
            }
            string brand = "";
            while (brand.Trim() == "")
            {
                Console.WriteLine("Please enter product brand: ");
                brand = Console.ReadLine();
            }

            string description = "";
            while (description.Trim() == "")
            {
                Console.WriteLine("Please enter product description: ");
                description = Console.ReadLine();
            }

            Console.WriteLine("Please specify the type of products (M for male or F for female): ");
            string genderResponse = "";
            string type = "";
            int genderCount = 0;
            do
            {
                genderResponse = Console.ReadLine();
                switch (genderResponse)
                {
                    case "M" or "m":
                        genderResponse = "M";
                        genderCount++;
                        break;
                    case "F" or "f":
                        genderResponse = "F";
                        genderCount++;
                        break;
                    default:
                        Console.WriteLine("Wrong option, please try again (M for male or F for female): ");
                        break;
                }
            }
            while (genderCount == 0);

            type = genderResponse == "M" || genderResponse == "m" ? Gender.Men.ToString() : Gender.Women.ToString();


            float price = 0f;
            int priceCount = 0;
            bool validPrice = false;
            CultureInfo ci = new CultureInfo("en-za");
            while (priceCount == 0)
            {
                Console.WriteLine("Please enter product price (The , sign represents the decimal point if you want to include it): ");
                validPrice = float.TryParse(Console.ReadLine(), NumberStyles.AllowDecimalPoint, ci, out price);
                if (validPrice)
                {
                    priceCount++;
                }
            }

            CategoriesRepository categoriesRepository = new CategoriesRepository();
            List<Category> categories = categoriesRepository.ReadGetAllRows();
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("=========================Categories========================");
            categories.ForEach(b => sb.AppendLine($"ID: {b.CategoryID}, Name: {b.Name}"));
            Console.WriteLine(sb.ToString());
            sb.Clear();
            int catCount = 0;
            int result = 0;
            int catID = 0;
            do
            {
                Console.WriteLine("Please select the category id for product: ");
                string catResponse = Console.ReadLine();
                Console.WriteLine();
                bool valid = int.TryParse(catResponse, out result);
                Category? category = null;
                if (valid)
                {
                    category = categoriesRepository.ReadGetAllRows().FirstOrDefault(c => c.CategoryID == result);
                    if (category != null)
                    {
                        catID = result;
                        catCount++;
                    }
                    else
                    {
                        Console.WriteLine("Please try again, Category ID does not exist in the list");
                    }
                }
                else
                {
                    Console.WriteLine("Please try again, Category ID not in the correct format");
                }
            }
            while (catCount == 0);

            int stockQuantity = 0;
            int stockCount = 0;
            bool validStock = false;
            while (stockCount == 0)
            {
                Console.WriteLine("Please enter product Stock Quantity: ");
                validStock = int.TryParse(Console.ReadLine(), out stockQuantity);
                if (validStock)
                {
                    stockCount++;
                }
            }

            product.ProductID = newID;
            product.Name = name;
            product.Brand = brand;
            product.Description = description;
            product.Type = type;
            product.Price = price;
            product.CategoryID = catID;
            product.StockQuantity = stockQuantity;
            product.ModifiedDate = DateTime.Now;

            int count = 0;
            do
            {
                Console.WriteLine("Are you sure you want to add this new product (Y/N): ");
                string response = Console.ReadLine();
                if (response == "y" || response == "Y")
                {
                    response = response.ToLower();
                }
                else if (response == "n" || response == "N")
                {
                    response = response.ToLower();
                }

                switch (response)
                {
                    case "y":
                        bool resultVal = productsRepository.AddEntity(product);
                        if (resultVal)
                        {
                            sb.AppendLine("Product has been added");
                            productsRepository.ReadGetAllRows();
                        }
                        count++;
                        break;
                    case "n":
                        sb.AppendLine("Insertion has been cancelled");
                        count++;
                        break;
                    default:
                        Console.WriteLine("Wrong option, please try again");
                        break;
                }
            }
            while (count == 0);
            return sb.ToString();
        }

        public string UpdateProduct(ProductsRepository productsRepository)
        {
            Console.WriteLine("Please enter a Product id:");
            int productID = 0;
            bool valid = int.TryParse(Console.ReadLine(), out productID);
            StringBuilder sb = new StringBuilder();
            if (valid)
            {
                Product product = new Product();
                List<Product> products = productsRepository.ReadGetAllRows();
                product = products.FirstOrDefault(c => c.ProductID == productID);
                if (product != null)
                {


                    string name = "";
                    Console.WriteLine("Please enter product name (Leave blank if you do not want to update): ");
                    name = Console.ReadLine();
                    if (name.Trim() == "")
                    {
                        name = product.Name;
                    }

                    string brand = "";
                    Console.WriteLine("Please enter product brand (Leave blank if you do not want to update): ");
                    brand = Console.ReadLine();
                    if (brand.Trim() == "")
                    {
                        brand = product.Brand;
                    }


                    string description = "";
                    Console.WriteLine("Please enter product description (Leave blank if you do not want to update): ");
                    description = Console.ReadLine();
                    if (description.Trim() == "")
                    {
                        description = product.Description;
                    }

                    string type = "";
                    Console.WriteLine("Please enter product type (M or F) (Leave blank if you do not want to update): ");
                    type = Console.ReadLine();
                    if (type.Trim() == "")
                    {
                        type = product.Type;
                    }
                    else
                    {
                        string genderResponse = "";
                        int genderCount = 0;
                        int whileCount = 0;
                        do
                        {
                            genderResponse = Console.ReadLine();
                            switch (genderResponse)
                            {
                                case "M" or "m":
                                    genderResponse = "M";
                                    genderCount++;
                                    break;
                                case "F" or "f":
                                    genderResponse = "F";
                                    genderCount++;
                                    break;
                                case "":
                                    whileCount++;
                                    genderCount++;
                                    break;
                                default:
                                    Console.WriteLine("Wrong option, please try again (M for male or F for female): ");
                                    break;
                            }
                        }
                        while (genderCount == 0);
                        if (whileCount == 0)
                        {
                            type = genderResponse == "M" || genderResponse == "m" ? Gender.Men.ToString() : Gender.Women.ToString();
                        }
                        else
                        {
                            type = product.Type;
                        }

                    }

                    float price = 0f;
                    int priceCount = 0;
                    bool validPrice = false;
                    CultureInfo ci = new CultureInfo("en-za");
                    while (priceCount == 0)
                    {
                        Console.WriteLine("Please enter product price (The , sign represents the decimal point if you want to include it): ");
                        validPrice = float.TryParse(Console.ReadLine(), NumberStyles.AllowDecimalPoint, ci, out price);
                        if (validPrice)
                        {
                            priceCount++;
                        }
                    }
                   

                    string inputCat = "";
                    int catID = 0;
                    Console.WriteLine("Please enter product category ID (Leave blank if you do not want to update): ");
                    inputCat = Console.ReadLine();
                    if (inputCat.Trim() == "")
                    {
                        catID = product.CategoryID;
                    }
                    else
                    {
                        CategoriesRepository categoriesRepository = new CategoriesRepository();
                        List<Category> categories = categoriesRepository.ReadGetAllRows();

                        Console.WriteLine("=========================Categories========================");
                        categories.ForEach(b => sb.AppendLine($"ID: {b.CategoryID}, Name: {b.Name}"));
                        Console.WriteLine(sb.ToString());
                        sb.Clear();
                        int catCount = 0;
                        int result = 0;
                        do
                        {
                            Console.WriteLine("Please select the category id for product (Leave blank if you do not want to update): ");
                            string catResponse = Console.ReadLine();

                            Console.WriteLine();
                            bool validCat = int.TryParse(catResponse, out result);
                            Category? category = null;
                            if (validCat)
                            {
                                category = categoriesRepository.ReadGetAllRows().FirstOrDefault(c => c.CategoryID == result);
                                if (category != null)
                                {
                                    catID = result;
                                    catCount++;
                                }
                                else
                                {
                                    Console.WriteLine("Please try again, Category ID does not exist in the list");
                                }
                            }
                            else
                            {
                                if (catResponse.Trim() == "")
                                {
                                    catID = product.CategoryID;
                                    catCount++;
                                }
                                else
                                {
                                    Console.WriteLine("Please try again, Category ID not in the correct format");
                                }

                            }
                        }
                        while (catCount == 0);
                    }

                    int stockQuantity = 0;
                    int stockCount = 0;
                    bool validStock = false;
                    while (stockCount == 0)
                    {
                        Console.WriteLine("Please enter product Stock Quantity: ");
                        validStock = int.TryParse(Console.ReadLine(), out stockQuantity);
                        if (validStock)
                        {
                            stockCount++;
                        }
                    }

                    product.Name = name;
                    product.Brand = brand;
                    product.Description = description;
                    product.Type = type;
                    product.Price = price;
                    product.CategoryID = catID;
                    product.StockQuantity = stockQuantity;
                    product.ModifiedDate = DateTime.Now;
                    int count = 0;
                    do
                    {
                        Console.WriteLine("Are you sure you want to update this product (Y/N): ");
                        string response = Console.ReadLine();
                        if (response == "y" || response == "Y")
                        {
                            response = response.ToLower();
                        }
                        else if (response == "n" || response == "N")
                        {
                            response = response.ToLower();
                        }

                        switch (response)
                        {
                            case "y":
                                bool result = productsRepository.UpdateEntity(product);
                                if (result)
                                {
                                    productsRepository.ReadGetAllRows();
                                    sb.AppendLine("Product has been updated");
                                }
                                count++;
                                break;
                            case "n":
                                sb.AppendLine("Update has been cancelled");
                                count++;
                                break;
                            default:
                                Console.WriteLine("Wrong option, please try again");
                                break;
                        }
                    }
                    while (count == 0);
                }
                else
                {
                    sb.AppendLine("Product does not exist");
                }
            }
            else
            {
                sb.AppendLine("Incorrect format of product id, please try again");
            }
            return sb.ToString();
        }

        public string RemoveProduct(ProductsRepository repository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Product> allOfTheProducts = repository.ReadGetAllRows();
            Console.WriteLine("Please enter the product ID: ");
            int prodId = 0;
            bool validNum = int.TryParse(Console.ReadLine(), out prodId);
            if (validNum)
            {
                bool exists = repository.CheckIfIdExists(prodId);
                if (exists)
                {
                    int count = 0;
                    do
                    {
                        Console.WriteLine("Are you sure you want to remove this product (Y/N): ");
                        string response = Console.ReadLine();
                        if (response == "y" || response == "Y")
                        {
                            response = response.ToLower();
                        }
                        else if (response == "n" || response == "N")
                        {
                            response = response.ToLower();
                        }

                        switch (response)
                        {
                            case "y":
                                bool result = repository.DeleteRow(prodId);
                                if (result)
                                {
                                    stringBuilder.AppendLine("Product has been deleted");
                                    repository.ReadGetAllRows();
                                }
                                count++;
                                break;
                            case "n":
                                stringBuilder.AppendLine("Deletion has been cancelled");
                                count++;
                                break;
                            default:
                                Console.WriteLine("Wrong option, please try again");
                                break;
                        }
                    }
                    while (count == 0);

                }
                else
                {
                    stringBuilder.AppendLine("Product ID does not exist");
                }
            }
            else
            {
                stringBuilder.AppendLine("Product ID not in the correct format, please try again");
            }
            return stringBuilder.ToString();
        }
    }
}
