using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class UserRepository : GeneralRepository<MyContext, User , string>
    {
        private readonly MyContext myContext;
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public UserRepository(MyContext myContext) : base(myContext)
         {
            this.myContext = myContext;
         }

        public int Login(LoginVM loginVM)
        {
            User employee = new User();
            UsersAccount account = new UsersAccount();
            var checkEmail = myContext.Users.Where(x => x.Email == loginVM.Email).FirstOrDefault();
            if (checkEmail == null)
            {
                return 2;
            }          
            var checkPass = myContext.UsersAccounts.Find(checkEmail.Email);

            bool validPass = BCrypt.Net.BCrypt.Verify(loginVM.Password, checkPass.Password);
            if (validPass)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
      
        public int RegisterNew(UserVM registerVM)
        {
            User user = new User();     
            var checkEmail = myContext.Employees.Where(x => x.Email == registerVM.Email).FirstOrDefault();
            if (checkEmail != null)
            {
                return 4;
            }
           
            user.CompanyName = registerVM.CompanyName;
            user.Email = registerVM.Email;
            user.Phone = registerVM.Phone;
            user.Photo= registerVM.Photo;
            user.Status = "Pending";
            
            myContext.Users.Add(user);
            myContext.SaveChanges();

            UsersAccount account = new UsersAccount();
            account.Email = registerVM.Email;
            account.Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password, GetRandomSalt());
            myContext.UsersAccounts.Add(account);
            myContext.SaveChanges();


            UserRole accountRole = new UserRole();
            accountRole.Email = account.Email;
            accountRole.Role_Id = registerVM.RoleId;
            myContext.UserRoles.Add(accountRole);
            var result = myContext.SaveChanges();
            return result;
        }
        public IEnumerable<User> GetAllVendor(int id)
        {
            if (id == 1)
            {
                var result = (from u in myContext.Users
                              join ur in myContext.UserRoles on u.Email equals ur.Email
                              join r in myContext.Roles on ur.Role_Id equals r.Role_Id
                              where r.Role_Id == 3 && u.Status != "Approve"
                              orderby u.Email
                              select new User
                              {
                                  CompanyName = u.CompanyName,
                                  Email = u.Email,
                                  Phone = u.Phone,
                                  Photo = u.Photo,
                                  Corporatebusiness = u.Corporatebusiness,
                                  TypeCompany = u.TypeCompany,
                                  Status = u.Status,
                              }).ToList();
                return result;
            }
            else {
                var result = (from u in myContext.Users
                              join ur in myContext.UserRoles on u.Email equals ur.Email
                              join r in myContext.Roles on ur.Role_Id equals r.Role_Id
                              where r.Role_Id == 3 
                              orderby u.Email
                              select new User
                              {
                                  CompanyName = u.CompanyName,
                                  Email = u.Email,
                                  Phone = u.Phone,
                                  Photo = u.Photo,
                                  Corporatebusiness = u.Corporatebusiness,
                                  TypeCompany = u.TypeCompany,
                                  Status = u.Status,
                              }).ToList();
                return result;
            }
           
        }
        public IEnumerable<User> GetDataVendor(string email)
        {
            return myContext.Users.Where(x => x.Email == email);
        }
    }
}
