using MainCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MainCode.Repository
{
    public class CategoriesRepository : RepositoryBase<Category>, IRepository<Category>
    {
        private static List<Category> allCategories;

        public CategoriesRepository()
        {
            allCategories = ReadGetAllRows();
        }

        public virtual List<Category> ReadGetAllRows()
        {
            allCategories = new List<Category>();
            foreach (var cat in Category.CategoriesDataSet)
            {
                allCategories.Add(new Category()
                {
                    CategoryID = cat.CategoryID,
                    Name = cat.Name
                });
            }
            return allCategories;
        }

        public virtual bool CheckIfIdExists(int id)
        {
            CategoriesRepository catRepository = new CategoriesRepository();
            List<Category> allOfTheCategories = catRepository.ReadGetAllRows();
            int count = 0;
            foreach (var cat in allOfTheCategories)
            {
                if (cat.CategoryID == id)
                {
                    count++;
                }
            }
            bool result = count > 0 ? true : false;
            return result;
        }

        public virtual bool CheckIfNameExists(string name)
        {
            CategoriesRepository catRepository = new CategoriesRepository();
            List<Category> allOfTheCategories = catRepository.ReadGetAllRows();
            int count = 0;
            foreach (var cat in allOfTheCategories)
            {
                if (cat.Name == name)
                {
                    count++;
                }
            }
            bool result = count > 0 ? true : false;
            return result;
        }

        public virtual Category ReadRowByID(int id)
        {
            CategoriesRepository catRepository = new CategoriesRepository();
            List<Category> allOfTheCategories = catRepository.ReadGetAllRows();
            Category category = allOfTheCategories.FirstOrDefault(c => c.CategoryID == id);
            return category;
        }

        public override bool AddEntity(Category entity)
        {
            bool returnVal = false;
            try
            {


                CategoriesRepository catRepository = new CategoriesRepository();
                List<Category> allOfTheCategories = catRepository.ReadGetAllRows();
                allOfTheCategories.Add(entity);
                Category.CategoriesDataSet.Add(entity);
                returnVal = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return returnVal;
        }

        public override bool DeleteRow(int id)
        {
            bool returnVal = false;
            CategoriesRepository catRepository = new CategoriesRepository();
            List<Category> allOfTheCategories = catRepository.ReadGetAllRows();
            try
            {
                var ds = allOfTheCategories.FirstOrDefault(c => c.CategoryID == id);
                if (ds != null)
                {
                    allOfTheCategories.RemoveAll(c => c.CategoryID == id);
                    Category.CategoriesDataSet.RemoveAll(c => c.CategoryID == id);
                    returnVal = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error, cannot delete category: "+ ex.ToString());
            }
            return returnVal;
        }

        public override bool UpdateEntity(Category entity)
        {
            bool returnVal = false;
            try
            {
                var category = Category.CategoriesDataSet.FirstOrDefault(c => c.CategoryID == entity.CategoryID);
                category.CategoryID = entity.CategoryID;
                category.Name = entity.Name;
                returnVal = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error, cannot update category: "+ ex.ToString());
            }
            return returnVal;
        }

        public virtual Stack<Category> GetCategoryByName(string name)
        {
            CategoriesRepository catRepository = new CategoriesRepository();
            List<Category> allOfTheCategories = catRepository.ReadGetAllRows();
            var categories = new Stack<Category>();
            var categorieslistDesc = allOfTheCategories.Where(c => c.Name == name).OrderByDescending(c => c.CategoryID);
            foreach (var cat in categorieslistDesc)
            {
                categories.Push(cat);
            }
            return categories;
        }
    }
}
