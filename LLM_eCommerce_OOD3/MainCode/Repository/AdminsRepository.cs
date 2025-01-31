using MainCode.Models;
using static MainCode.MainCodeStaticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository
{
    public class AdminsRepository : RepositoryBase<Admin>, ILogin<Admin>
    {
        private static List<Admin> allAdmins;

        public AdminsRepository()
        {
            allAdmins = ReadGetAllRows();
        }
        public virtual List<Admin> ReadGetAllRows()
        {
            allAdmins = new List<Admin>();
            foreach (var adm in Admin.AdminsDataSet)
            {
                allAdmins.Add(new Admin()
                {
                    AdminID = adm.AdminID,
                    FirstName = adm.FirstName,
                    Surname = adm.Surname,
                    Email = adm.Email,
                    Username = adm.Username,
                    Address = adm.Address,
                    PhoneNumber = adm.PhoneNumber,
                    Role = adm.Role
                });
            }
            return allAdmins;
        }

        public bool Login(string username)
        {
            AdminsRepository adminsRepository = new AdminsRepository();
            var adminlist = adminsRepository.ReadGetAllRows();
            var admin = adminlist.FirstOrDefault(c => c.Username == username);
            bool valid = admin != null ? true : false;
            return valid;
        }

        public void Login()
        {
            char exitMenu = 'b';
            char menuOption = ' ';
            string userName = string.Empty;
            int count = 0;
            while (menuOption != exitMenu)
            {
                Console.Clear();
                Console.WriteLine("\nInstant Order eCommerce System Main Menu\n==================================================================");
                Console.WriteLine("Enter your username (b to go to Main Menu): ");
                userName = Console.ReadLine();

                if (userName == "X" || userName == "x")
                {
                    menuOption = 'x';
                }
                bool checkLogin = Login(userName);

                if (checkLogin)
                {
                    menuOption = '1';
                }
                else if (userName == "b" || userName == "B")
                {
                    menuOption = 'b';
                }

                switch (menuOption)
                {
                    case '1':
                        ++count;
                        menuOption = 'b';
                        break;
                    case 'b':
                        break;
                    default:
                        Console.WriteLine("User does not exist, please try again");
                        Console.WriteLine("\nPress any key to continue ...");
                        Console.ReadKey();
                        break;
                }
            }
            if (count == 1)
            {
                AdminsRepository adminsRepository = new AdminsRepository();
                var adminlist = adminsRepository.ReadGetAllRows();
                var admin = adminlist.FirstOrDefault(c => c.Username == userName);
                person = new Person(admin.AdminID, admin.FirstName, admin.Surname, admin.Username);
                MenuOptions.AdminMenu();
            }
        }

        public virtual Admin ReadRowByID(int id)
        {
            AdminsRepository adminsRepository = new AdminsRepository();
            var adminlist = adminsRepository.ReadGetAllRows();
            var admin = adminlist.FirstOrDefault(c => c.AdminID == id);
            return admin;
        }

        public virtual bool CheckIfIdExists(int id)
        {
            AdminsRepository adminsRepository = new AdminsRepository();
            var adminlist = adminsRepository.ReadGetAllRows();
            var DScheck = adminlist.FirstOrDefault(c => c.AdminID == id);
            bool valid = false;
            if (DScheck != null)
            {
                valid = true;
            }
            return valid;
        }

        public virtual bool CheckIfEmailExists(string email)
        {
            AdminsRepository adminsRepository = new AdminsRepository();
            var adminlist = adminsRepository.ReadGetAllRows();
            var DScheck = adminlist.FirstOrDefault(c => c.Email == email);
            bool valid = false;
            if (DScheck != null)
            {
                valid = true;
            }
            return valid;
        }

        public override bool AddEntity(Admin entity)
        {
            bool returnVal = false;
            try
            {

                allAdmins.Add(entity);
                Admin.AdminsDataSet.Add(entity);
                returnVal = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error, Admin not added: " + ex.ToString());
            }
            return returnVal;
        }

        public override bool DeleteRow(int id)
        {
            bool returnVal = false;
            try
            {
                var ds = allAdmins.FirstOrDefault(c => c.AdminID == id);
                if (ds != null)
                {
                    allAdmins.RemoveAll(c => c.AdminID == id);
                    Admin.AdminsDataSet.RemoveAll(c => c.AdminID == id);
                    returnVal = true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error, Admin not deleted: "+ ex.ToString());
            }
            return returnVal;
        }
        public override bool UpdateEntity(Admin entity)
        {
            bool returnVal = false;
            try
            {
                var admin = Admin.AdminsDataSet.FirstOrDefault(c => c.AdminID == entity.AdminID);
                admin.AdminID = entity.AdminID;
                admin.FirstName = entity.FirstName;
                admin.Surname = entity.Surname;
                admin.Email = entity.Email;
                admin.Username = entity.Username;
                admin.Address = entity.Address;
                admin.PhoneNumber = entity.PhoneNumber;
                admin.Role = entity.Role;
                returnVal = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error, cannot update admin: "+ ex.ToString());
            }
            return returnVal;
        }


        public virtual Stack<Admin> GetAdminByName(string name)
        {
            AdminsRepository adminsRepository = new AdminsRepository();
            var adminlist = adminsRepository.ReadGetAllRows();
            var admins = new Stack<Admin>();
            var adminslistDesc = adminlist.Where(c => c.FirstName == name).OrderByDescending(c => c.AdminID);
            foreach (var adm in adminslistDesc)
            {
                admins.Push(adm);
            }
            return admins;
        }

        public virtual Stack<Admin> GetAdminByName(string name, string surName)
        {
            AdminsRepository adminsRepository = new AdminsRepository();
            var adminlist = adminsRepository.ReadGetAllRows();
            var admins = new Stack<Admin>();
            foreach (var admin in adminlist.Where(c => c.FirstName == name && c.Surname == surName).OrderByDescending(c => c.AdminID))
            {
                admins.Push(admin);
            }
            return admins;
        }
    }
}
