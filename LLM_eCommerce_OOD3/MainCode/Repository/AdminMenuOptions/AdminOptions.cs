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
    /// This AdminOptions class consists of methods called when a user clicks a menu option in the front-end.
    /// These methods interact with the Repository to extract or remove data from the Repository.
    /// </summary>
    /// <remarks>
    /// 
    /// public AdminOptions(AdminsRepository _adminsRepository, RepositoryBase&lt;Admin&gt; _adminsRepositoryBase)
    /// A counstructor that initializes the AdminOptions Class.
    /// These parameters are used for injection into the class for mock testing.
    /// <param name="_adminsRepository">The admins repository.</param>
    /// <param name="_adminsRepositoryBase">The admins IRepository.</param>
    /// 
    /// public string FindAdminByID(int adminID)
    /// Finds an admin by ID and returns their details as a string.
    /// <param name="adminID">The ID of the admin to find.</param>
    /// <returns>A string containing the admin details if found, or a message indicating that the admin does not exist.</returns>
    /// 
    /// public string FindAdminByFirstName(string adName)
    /// Finds admins by their First Name and returns their details as a string.
    /// <param name="adName">The Name of the admin or admins to find.</param>
    /// <returns>A string containing the admin details if found, or a message indicating that the admin does not exist.</returns>
    /// 
    /// public string FindAdminByFullName(string adName, string lName)
    /// Finds admins by their FullName and returns their details as a string.
    /// <param name="adName">The Name of the admin or admins to find.</param>
    /// <param name="lName">The Surname of the admin or admins to find.</param>
    /// <returns>A string containing the admin details if found, or a message indicating that the admin does not exist.</returns>
    /// 
    /// public string AddNewAdmin(Admin admin)
    /// Adds a new admin to the repository and returns a string that shows whether it was successful or not.
    /// <param name="admin">The admin object of the admin to add.</param>
    /// <returns>A string containing a message indicating that the admin has been added or not.</returns>
    /// 
    /// public string UpdateAdmin(Admin admin)
    /// Updates admin to the repository and returns a string that shows whether it was successful or not.
    /// <param name="admin">The admin object of the admin to update.</param>
    /// <returns>A string containing a message indicating that the admin has been updated or not.</returns>
    /// 
    /// public string RemoveAdmin(int adminId)
    /// Removes admin from the repository and returns a string that shows whether it was successful or not.
    /// <param name="adminId">The admin Id of the admin to remove as an integer type.</param>
    /// <returns>A string containing a message indicating that the admin has been removed or not.</returns>
    /// 
    /// </remarks>
    ///
    public class AdminOptions
    {
        private AdminsRepository adminsRepository;
        private RepositoryBase<Admin> adminsRepositoryBase;
        public AdminOptions(AdminsRepository _adminsRepository, RepositoryBase<Admin> _adminsRepositoryBase)
        {
            adminsRepository = _adminsRepository;
            adminsRepositoryBase = _adminsRepositoryBase;
        }

        public string FindAdminByID(int adminID)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool valid = adminsRepository.CheckIfIdExists(adminID);
            if (valid)
            {
                Admin admin = adminsRepository.ReadRowByID(adminID);
                stringBuilder.AppendLine($"ID: {admin.AdminID}, First Name: {admin.FirstName}, Surname: {admin.Surname}, Email: {admin.Email}, Username: {admin.Username}, Address: {admin.Address}, Phone Number: {admin.PhoneNumber}");
            }
            else
            {
                stringBuilder.AppendLine("Admin does not exist, Please try again");
            }

            return stringBuilder.ToString();
        }

        public string FindAdminByFirstName(string adName)
        {


            StringBuilder stringBuilder = new StringBuilder();
            Stack<Admin> admins = adminsRepository.GetAdminByName(adName);
            if (admins.Count > 0)
            {
                while (admins.Count != 0)
                {
                    var admin = admins.Pop();
                    stringBuilder.AppendLine($"ID: {admin.AdminID}, First Name: {admin.FirstName}, Surname: {admin.Surname}, Email: {admin.Email}, Username: {admin.Username}, Address: {admin.Address}, Phone Number: {admin.PhoneNumber}");
                }
            }
            else
            {
                stringBuilder.AppendLine("No admin with that first name exists");
            }
            return stringBuilder.ToString();

        }

        public string FindAdminByFullName(string adName, string lName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Stack<Admin> admins = adminsRepository.GetAdminByName(adName, lName);
            if (admins.Count > 0)
            {
                while (admins.Count != 0)
                {
                    var admin = admins.Pop();
                    stringBuilder.AppendLine($"ID: {admin.AdminID}, First Name: {admin.FirstName}, Surname: {admin.Surname}, Email: {admin.Email}, Username: {admin.Username}, Address: {admin.Address}, Phone Number: {admin.PhoneNumber}");
                }

            }
            else
            {
                stringBuilder.AppendLine("No admin with that fullname exists");
            }
            return stringBuilder.ToString();
        }

        public string AddNewAdmin(Admin admin)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!adminsRepository.CheckIfEmailExists(admin.Email))
            {
                bool result = adminsRepository.AddEntity(admin);
                if (result)
                {
                    stringBuilder.AppendLine("Admin has been added");
                    adminsRepository.ReadGetAllRows();
                }
                else
                {
                    stringBuilder.AppendLine("An error occured, admin was not added");
                }
            }
            else
            {
                stringBuilder.AppendLine("Admin with that email already exists");
            }

            return stringBuilder.ToString();
        }

        public string UpdateAdmin(Admin admin)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool check = adminsRepository.CheckIfIdExists(admin.AdminID);
            if (check)
            {
                bool result = adminsRepository.UpdateEntity(admin);
                if (result)
                {
                    adminsRepository.ReadGetAllRows();
                    stringBuilder.AppendLine("Admin has been updated");
                }
                else
                {
                    stringBuilder.AppendLine("Error Occured, Admin was not updated");
                }
            }
            else
            {
                stringBuilder.AppendLine("Admin does not exist");
            }

            return stringBuilder.ToString();
        }

        public string RemoveAdmin(int adminId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool exists = adminsRepository.CheckIfIdExists(adminId);
            if (exists)
            {

                bool result = adminsRepository.DeleteRow(adminId);
                if (result)
                {
                    stringBuilder.AppendLine("Admin has been deleted");
                    adminsRepository.ReadGetAllRows();
                }
                else
                {
                    stringBuilder.AppendLine("Error Occured, Admin not removed");
                }

            }
            else
            {
                stringBuilder.AppendLine("Admin ID does not exist");
            }
            return stringBuilder.ToString();
        }
    }
}
