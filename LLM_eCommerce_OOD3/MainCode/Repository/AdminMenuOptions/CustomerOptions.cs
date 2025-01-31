using MainCode.Models;
using MainCode.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository.AdminMenuOptions
{
    /// <summary>
    /// This CustomerOptions class consists of methods called when a user clicks a menu option in the front-end.
    /// These methods interact with the Repository to extract or remove data from the Repository.
    /// </summary>
    /// <remarks>
    /// 
    /// public CustomerOptions(CustomersRepository _customersRepository, RepositoryBase&lt;Customer&gt; _customersRepositoryBase, IRepository&lt;Customer&gt; _customersIRepository)
    /// A counstructor that initializes the CustomerOptions Class.
    /// These parameters are used for injection into the class for mock testing.
    /// <param name="_customersRepository">The customers repository.</param>
    /// <param name="_customersRepositoryBase">The customers RepositoryBase which is an abstract class.</param>
    /// <param name="_customersRepositoryBase">The customers IRepository which is an interface.</param>
    /// 
    /// public string FindCustomerByID(int cusID)
    /// Finds an customers by ID and returns their details as a string.
    /// <param name="cusID">The ID of the customer to find.</param>
    /// <returns>A string containing the customer details if found, or a message indicating that the customer does not exist.</returns>
    /// 
    /// public string FindCustomerByFirstName(string cusName)
    /// Finds customers by their First Name and returns their details as a string.
    /// <param name="cusName">The Name of the customers or customers to find.</param>
    /// <returns>A string containing the customers details if found, or a message indicating that the customers does not exist.</returns>
    /// 
    /// public string FindCustomerByFullName(string adName, string lName)
    /// Finds customers by their FullName and returns their details as a string.
    /// <param name="cusName">The Name of the customer or customers to find.</param>
    /// <param name="lName">The Surname of the customer or customers to find.</param>
    /// <returns>A string containing the customer details if found, or a message indicating that the customers does not exist.</returns>
    /// 
    /// public string AddNewCustomer(Customer customer)
    /// Adds a new customer to the repository and returns a string that shows whether it was successful or not.
    /// <param name="customer">The customer object of the customer to add.</param>
    /// <returns>A string containing a message indicating that the customer has been added or not.</returns>
    /// 
    /// public string UpdateCustomer(Customer customer)
    /// Updates customer to the repository and returns a string that shows whether it was successful or not.
    /// <param name="customer">The customer object of the customer to update.</param>
    /// <returns>A string containing a message indicating that the customer has been updated or not.</returns>
    /// 
    /// public string RemoveCustomer(int cusId)
    /// Removes customer from the repository and returns a string that shows whether it was successful or not.
    /// <param name="cusId">The Id of the customer to remove as an integer type.</param>
    /// <returns>A string containing a message indicating that the customer has been removed or not.</returns>
    /// 
    /// </remarks>
    ///

    public class CustomerOptions
    {
        private CustomersRepository customersRepository;
        private RepositoryBase<Customer> customersRepositoryBase;
        private IRepository<Customer> customersIRepository;

        public CustomerOptions(CustomersRepository _customersRepository, RepositoryBase<Customer> _customersRepositoryBase, IRepository<Customer> _customersIRepository)
        {
            customersRepository = _customersRepository;
            customersRepositoryBase = _customersRepositoryBase;
            customersIRepository = _customersIRepository;
        }

        public string FindCustomerByID(int cusID)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool valid = customersRepository.CheckIfIdExists(cusID);
            if (valid)
            {
                var customer = customersRepository.ReadRowByID(cusID);
                stringBuilder.AppendLine($"ID: {customer.CustomerID}, First Name: {customer.FirstName}, Surname: {customer.Surname}, Email: {customer.Email}, Username: {customer.Username}, Address: {customer.Address}, Phone Number: {customer.PhoneNumber}");
            }
            else
            {
                stringBuilder.AppendLine("Customer does not exist, Please try again");
            }
            return stringBuilder.ToString();
        }

        public string FindCustomerByFirstName(string cusName)
        {

            StringBuilder stringBuilder = new StringBuilder();
            Queue<Customer> customers = customersRepository.GetCustomerByName(cusName);
            if (customers.Count > 0)
            {
                while (customers.Count != 0)
                {
                    var customer = customers.Dequeue();
                    stringBuilder.AppendLine($"ID: {customer.CustomerID}, First Name: {customer.FirstName}, Surname: {customer.Surname}, Email: {customer.Email}, Username: {customer.Username}, Address: {customer.Address}, Phone Number: {customer.PhoneNumber}");
                }
            }
            else
            {
                stringBuilder.AppendLine("No customer with that first name exists");
            }

            return stringBuilder.ToString();
        }

        public string FindCustomerByFullName(string cusName, string lName)
        {

            StringBuilder stringBuilder = new StringBuilder();
            Queue<Customer> customers = customersRepository.GetCustomerByName(cusName, lName);
            if (customers.Count > 0)
            {
                while (customers.Count != 0)
                {
                    var customer = customers.Dequeue();
                    stringBuilder.AppendLine($"ID: {customer.CustomerID}, First Name: {customer.FirstName}, Surname: {customer.Surname}, Email: {customer.Email}, Username: {customer.Username}, Address: {customer.Address}, Phone Number: {customer.PhoneNumber}");
                }

            }
            else
            {
                stringBuilder.AppendLine("No customer with that fullname exists");
            }
            return stringBuilder.ToString();

        }

        public string AddNewCustomer(Customer customer)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!customersRepository.CheckIfEmailExists(customer.Email))
            {
                bool result = customersRepository.AddEntity(customer);
                if (result)
                {
                    stringBuilder.AppendLine("Customer has been added");
                    customersRepository.ReadGetAllRows();
                }
                else
                {
                    stringBuilder.AppendLine("An error occured, customer was not added");
                }
            }
            else
            {
                stringBuilder.AppendLine("Customer with that email already exists");
            }
            return stringBuilder.ToString();
        }

        public string UpdateCustomer(Customer customer)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool check = customersRepository.CheckIfIdExists(customer.CustomerID);
            if (check)
            {
                bool result = customersRepository.UpdateEntity(customer);
                if (result)
                {
                    customersRepository.ReadGetAllRows();
                    stringBuilder.AppendLine("Customer has been updated");
                }
                else
                {
                    stringBuilder.AppendLine("Error Occured, Customer was not updated");
                }
            }
            else
            {
                stringBuilder.AppendLine("Customer does not exist");
            }

            return stringBuilder.ToString();
        }

        public string RemoveCustomer(int cusId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool exists = customersRepository.CheckIfIdExists(cusId);
            if (exists)
            {

                bool result = customersRepository.DeleteRow(cusId);
                if (result)
                {
                    stringBuilder.AppendLine("Customer has been deleted");
                    customersRepository.ReadGetAllRows();
                }
                else
                {
                    stringBuilder.AppendLine("Error Occured, Customer not removed");
                }

            }
            else
            {
                stringBuilder.AppendLine("Customer ID does not exist");
            }
            return stringBuilder.ToString();
        }
    }
}
