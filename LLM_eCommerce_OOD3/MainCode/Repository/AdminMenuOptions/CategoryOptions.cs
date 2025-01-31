using MainCode.Models;
using MainCode.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository.AdminMenuOptions
{
    /// <summary>
    /// This CategoryOptions class consists of methods called when a user clicks a menu option in the front-end.
    /// These methods interact with the Repository to extract or remove data from the Repository.
    /// </summary>
    /// <remarks>
    /// 
    /// public CategoryOptions(CategoriesRepository _categoriesRepository, RepositoryBase&lt;Category&gt; _categoriesRepositoryBase, IRepository&lt;Category&gt; _categoriesIRepository)
    /// A counstructor that initializes the CategoryOptions Class.
    /// These parameters are used for injection into the class for mock testing.
    /// <param name="_categoriesRepository">The categories repository.</param>
    /// <param name="_categoriesRepositoryBase">The categories RepositoryBase which is an abstract class.</param>
    /// <param name="_categoriesRepositoryBase">The categories IRepository which is an interface.</param>
    /// 
    /// public string FindCategoryByID(int catID)
    /// Finds an categories by ID and returns their details as a string.
    /// <param name="cusID">The ID of the category to find.</param>
    /// <returns>A string containing the category details if found, or a message indicating that the category does not exist.</returns>
    /// 
    /// public string FindCategoryByName(string catName)
    /// Finds categories by their Name and returns their details as a string.
    /// <param name="catName">The Name of the category or categories to find.</param>
    /// <returns>A string containing the category details if found, or a message indicating that the category does not exist.</returns>
    ///  
    /// public string AddNewCategory(Category category)
    /// Adds a new category to the repository and returns a string that shows whether it was successful or not.
    /// <param name="category">The category object of the category to add.</param>
    /// <returns>A string containing a message indicating that the category has been added or not.</returns>
    /// 
    /// public string UpdateCategory(Category category)
    /// Updates category to the repository and returns a string that shows whether it was successful or not.
    /// <param name="category">The category object of the category to update.</param>
    /// <returns>A string containing a message indicating that the category has been updated or not.</returns>
    /// 
    /// public string RemoveCategory(int cusId)
    /// Removes category from the repository and returns a string that shows whether it was successful or not.
    /// <param name="cusId">The Id of the category to remove as an integer type.</param>
    /// <returns>A string containing a message indicating that the category has been removed or not.</returns>
    /// 
    /// </remarks>
    ///
    public class CategoryOptions
    {
        private CategoriesRepository categoriesRepository;
        private RepositoryBase<Category> categoriesRepositoryBase;
        private IRepository<Category> categoriesIRepository;
        public CategoryOptions(CategoriesRepository _categoriesRepository, RepositoryBase<Category> _categoriesRepositoryBase, IRepository<Category> _categoriesIRepository)
        {
            categoriesRepository = _categoriesRepository;
            categoriesRepositoryBase = _categoriesRepositoryBase;
            categoriesIRepository = _categoriesIRepository;
        }
        public string FindCategoryByID(int catID)
        {
            
            StringBuilder sb = new StringBuilder();
            
                bool valid = categoriesRepository.CheckIfIdExists(catID);
                if (valid)
                {
                    var category = categoriesRepository.ReadRowByID(catID);
                    sb.AppendLine($"ID: {category.CategoryID}, Name: {category.Name}");
                }
                else
                {
                    sb.AppendLine("category does not exist, Please try again");
                }
            
            return sb.ToString();
        }

        public string FindCategoryByName(string catName)
        {
            

            StringBuilder stringBuilder = new StringBuilder();
            Stack<Category> categories = categoriesRepository.GetCategoryByName(catName);
            if (categories.Count > 0)
            {
                while (categories.Count != 0)
                {
                    var category = categories.Pop();
                    stringBuilder.AppendLine($"ID: {category.CategoryID}, Name: {category.Name}");
                }
            }
            else
            {
                stringBuilder.AppendLine("No category with that name exists");
            }
            return stringBuilder.ToString();
        }



        public string AddNewCategory(Category category)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!categoriesRepository.CheckIfNameExists(category.Name))
            {

                bool result = categoriesRepository.AddEntity(category);
                if (result)
                {
                    categoriesRepository.ReadGetAllRows();
                    stringBuilder.AppendLine("Category has been added");
                }
                else
                {
                    stringBuilder.AppendLine("Error, cannot add category");
                }
            }
            else
            {
                stringBuilder.AppendLine("Cannot add category that already exists");
            }
            return stringBuilder.ToString();
        }

        public string UpdateCategory(Category category)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool check = categoriesRepository.CheckIfIdExists(category.CategoryID);
            if (check)
            {
                bool result = categoriesRepository.UpdateEntity(category);
                if (result)
                {
                    categoriesRepository.ReadGetAllRows();
                    stringBuilder.AppendLine("Category has been updated");
                }
                else
                {
                    stringBuilder.AppendLine("Error Occured, Category was not updated");
                }
            }
            else
            {
                stringBuilder.AppendLine("Category does not exist");
            }
            return stringBuilder.ToString();
        }

        public string RemoveCategory(int catId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            bool exists = categoriesRepository.CheckIfIdExists(catId);
            if (exists)
            {
                bool result = categoriesRepository.DeleteRow(catId);
                if (result)
                {
                    categoriesRepository.ReadGetAllRows();
                    stringBuilder.AppendLine("Category has been deleted");
                }
                else
                {
                    stringBuilder.AppendLine("Error Occured, Category not removed");
                }

            }
            else
            {
                stringBuilder.AppendLine("Category ID does not exist");
            }
            return stringBuilder.ToString();
        }
    }
}
