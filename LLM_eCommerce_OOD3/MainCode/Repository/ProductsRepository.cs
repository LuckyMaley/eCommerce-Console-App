using MainCode.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MainCode.Repository
{
    public class ProductsRepository: RepositoryBase<Product>, IRepository<Product>
    {
        private static List<Product> allProducts;

        public ProductsRepository()
        {
            allProducts = ReadGetAllRows();
        }
        public virtual List<Product> ReadGetAllRows()
        {
            allProducts = new List<Product>();
            foreach (var prod in Product.ProductsDataSet)
            {
                allProducts.Add(new Product()
                {
                    ProductID = prod.ProductID,
                    Name = prod.Name,
                    Brand = prod.Brand,
                    Description = prod.Description,
                    Type = prod.Type,
                    Price = prod.Price,
                    CategoryID = prod.CategoryID,
                    StockQuantity = prod.StockQuantity,
                    ModifiedDate = prod.ModifiedDate
                });
            }
            return allProducts;
        }

        public bool CheckIfIdExists(int id)
        {
            ProductsRepository prodRepository = new ProductsRepository();
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();
            int count = 0;
            foreach (var product in allOfTheProducts)
            {
                if (product.ProductID == id)
                {
                    count++;
                }
            }
            bool result = count > 0 ? true : false;
            return result;
        }

        public Product ReadRowByID(int id)
        {
            ProductsRepository prodRepository = new ProductsRepository();
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();
            Product product = allOfTheProducts.FirstOrDefault(c => c.ProductID == id);
            return product;
        }

        public override bool AddEntity(Product entity)
        {
            ProductsRepository prodRepository = new ProductsRepository();
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();
            bool returnVal = false;
            try
            {
                allOfTheProducts.Add(entity);
                Product.ProductsDataSet.Add(entity);
                returnVal = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error, cannot add product" + ex.ToString());
            }

            return returnVal;
        }

        public override bool DeleteRow(int id)
        {
            ProductsRepository prodRepository = new ProductsRepository();
            List<Product> allOfTheProducts = prodRepository.ReadGetAllRows();
            bool returnVal = false;
            try
            {
                var ds = allOfTheProducts.FirstOrDefault(c => c.ProductID == id);
                if (ds != null)
                {
                    allOfTheProducts.RemoveAll(c => c.ProductID == id);
                    Product.ProductsDataSet.RemoveAll(c => c.ProductID == id);
                    returnVal = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error, cannot delete product" + ex.ToString());
            }

            return returnVal;
        }

        public override bool UpdateEntity(Product entity)
        {
            bool returnVal = false;
            try
            {
                var product = Product.ProductsDataSet.FirstOrDefault(c => c.ProductID == entity.ProductID);
                product.ProductID = entity.ProductID;
                product.Name = entity.Name;
                product.Brand = entity.Brand;
                product.Description = entity.Description;
                product.Type = entity.Type;
                product.Price = entity.Price;
                product.CategoryID = entity.CategoryID;
                product.StockQuantity = entity.StockQuantity;
                product.ModifiedDate = entity.ModifiedDate;
                returnVal = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error, cannot update product" + ex.ToString());
            }

            return returnVal;
        }

        public Stack<Product> GetProductByName(string name)
        {
            ProductsRepository productsRepository = new ProductsRepository();
            var prodlist = productsRepository.ReadGetAllRows();
            var products = new Stack<Product>();
            var productslistDesc = prodlist.Where(c => c.Name == name).OrderByDescending(c => c.ProductID);
            foreach (var prod in productslistDesc)
            {
                products.Push(prod);
            }
            return products;
        }
    }
}
