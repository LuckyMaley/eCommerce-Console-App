using MainCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository
{
    public partial class CustomersRepository: RepositoryBase<Customer>, IRepository<Customer>, ILogin<Customer>
    {
        private static List<Customer> allCustomers;

        public CustomersRepository()
        {
            allCustomers = ReadGetAllRows();
        }

        public virtual partial List<Customer> ReadGetAllRows();
        
        public virtual Customer ReadRowByID(int id)
        {
            Customer customer = allCustomers.FirstOrDefault(c => c.CustomerID == id);
            return customer;
        }

        public virtual bool CheckIfIdExists(int id)
        {
            var DScheck = allCustomers.FirstOrDefault(c =>c.CustomerID == id);
            bool valid = false;
            if (DScheck != null)
            {
                valid = true;
            }
            return valid;
        }

        public virtual bool CheckIfEmailExists(string email)
        {
            var DScheck = allCustomers.FirstOrDefault(c => c.Email == email);
            bool valid = false;
            if (DScheck != null)
            {
                valid = true;
            }
            return valid;
        }

        public override bool AddEntity(Customer entity)
        {
            bool returnVal = false;
            try
            {
                allCustomers.Add(entity);
                Customer.CustomersDataSet.Add(entity);
                returnVal = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error, cannot add customer" + ex.ToString());
            }
            return returnVal;
            
        }

        public override bool DeleteRow(int id)
        {
            bool returnVal = false;
            try
            {
                var ds = allCustomers.FirstOrDefault(c => c.CustomerID == id);
                if (ds != null)
                {
                    allCustomers.RemoveAll(c => c.CustomerID == id);
                    Customer.CustomersDataSet.RemoveAll(c => c.CustomerID == id);
                    returnVal = true;
                }
            }
            catch(Exception ex) 
            { 
                Console.WriteLine("Error, cannot delete customer" + ex.ToString()); 
            }

            return returnVal;
        }

    }
}
