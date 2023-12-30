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
        //public int SignManager(SignManagerVM signManagerVM)
        //{
        //    Employee employee = new Employee();

        //    var checkdata = myContext.Employees.Find(signManagerVM.NIK);
        //    employee.NIK = signManagerVM.NIK;
        //    if (checkdata == null)
        //    {
        //        return 2;
        //    }
        //    AccountRole accountRole = new AccountRole();
        //    accountRole.NIK = signManagerVM.NIK;
        //    accountRole.Role_Id = 5;
        //    myContext.AccountRoles.Add(accountRole);
        //    var result = myContext.SaveChanges();
        //    return result;
        //}
        public int RegisterNew(UserVM registerVM)
        {
            User user = new User();
            //var checkUniversity = myContext.Universities.Find(registerVM.UniversityId);
            //var checkdata = myContext.Employees.Find(registerVM.NIK);

            //var checkPhone = myContext.Employees.Where(x => x.Phone == registerVM.PhoneNumber).FirstOrDefault();
            var checkEmail = myContext.Employees.Where(x => x.Email == registerVM.Email).FirstOrDefault();
            //employee.NIK = registerVM.NIK;
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
        public IEnumerable<User> GetAllVendor()
        {
            var result = (from u in myContext.Users
                          join ur in myContext.UserRoles on u.Email equals ur.Email
                          join r in myContext.Roles on ur.Role_Id equals r.Role_Id
                       where r.Role_Id == 3 orderby u.Email
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
        //       public IEnumerable<RegisterVM> GetProfile()
        //       {
        //           var query = (from e in myContext.Employees
        //                        join a in myContext.Accounts on e.NIK equals a.NIK
        //                        join p in myContext.Profillings on a.NIK equals p.NIK
        //                        join ed in myContext.Educations on p.Education_Id equals ed.Id
        //                        join u in myContext.Universities on ed.University_id equals u.Id
        //                        join ar in myContext.AccountRoles on a.NIK equals ar.NIK
        //                        join r in myContext.Roles on ar.Role_Id equals r.Role_Id
        //                        orderby e.NIK
        //                        select new RegisterVM
        //                        {
        //                            NIK = e.NIK,
        //                            FirstName = e.FirstName,
        //                            LastName = e.LastName,
        //                            PhoneNumber = e.Phone,
        //                            BirthDate = e.BirthDate,
        //                            Gender = (ViewModel.Gender)e.Gender,
        //                            Salary = e.Salary,
        //                            Email = e.Email,
        //                            Password = a.Password,
        //                            Degree = ed.Degree,
        //                            Gpa = ed.Gpa,
        //                            UniversityId = ed.University_id,
        //                            Role_Id = ar.Role_Id
        //                        }).ToList();
        //           return query;
        //       }
        //       public IEnumerable <RegisterVM> GetProfileNik(string NIK)
        //       {
        //           var query = (from e in myContext.Employees
        //                        join a in myContext.Accounts on e.NIK equals a.NIK
        //                        join p in myContext.Profillings on a.NIK equals p.NIK
        //                        join ed in myContext.Educations on p.Education_Id equals ed.Id
        //                        join u in myContext.Universities on ed.University_id equals u.Id
        //                        join ar in myContext.AccountRoles on a.NIK equals ar.NIK
        //                        join r in myContext.Roles on ar.Role_Id equals r.Role_Id
        //                        orderby e.NIK
        //                        select new RegisterVM
        //                        {
        //                            NIK = e.NIK,
        //                            FirstName = e.FirstName,
        //                            LastName = e.LastName,
        //                            PhoneNumber = e.Phone,
        //                            BirthDate = e.BirthDate,
        //                            Salary = e.Salary,
        //                            Email = e.Email,
        //                            Gender = (ViewModel.Gender)e.Gender,
        //                            Password = a.Password,
        //                            Degree = ed.Degree,
        //                            Gpa = ed.Gpa,
        //                            UniversityId = ed.University_id,
        //                            Role_Id = ar.Role_Id
        //                        }).Where(e => e.NIK == NIK).ToList(); 
        //           return query;
        //       }
        //       public IEnumerable GetGender()
        //       {
        //           var result = from emp in myContext.Employees
        //                        group emp by emp.Gender into x
        //                        select new 
        //                        {
        //                            gender = x.Key,
        //                            value = x.Count()
        //                        };
        //           return result;
        //       }


        ///*       public IEnumerable GetSalary()
        //       {
        //           var result = from sale in myContext.Employees
        //                        group sale by sale.Salary into x
        //                        select new
        //                        {
        //                            salary = x.Key,
        //                            value = x.Count()
        //                        };
        //           return result;
        //       }*/
        //       public object[] GetSalary2()
        //       {
        //           var label1 = (from emp in myContext.Employees
        //                         select new
        //                         {
        //                             label = "Rp.2.000.000 - Rp.5.000.000",
        //                             value = myContext.Employees.Where(a => a.Salary <= 5000000 && a.Salary >= 2000000).Count()
        //                         }).First();
        //           var label2 = (from emp in myContext.Employees
        //                         select new
        //                         {
        //                             label = "Rp.5.000.001 - Rp.10.000.000",
        //                             value = myContext.Employees.Where(a => a.Salary <= 10000000 && a.Salary >= 5000001).Count()
        //                         }).First();
        //           var label3 = (from emp in myContext.Employees
        //                         select new
        //                         {
        //                             label = "< Rp.2.000.000",
        //                             value = myContext.Employees.Where(a => a.Salary < 2000000).Count()
        //                         }).First();
        //           var label4 = (from emp in myContext.Employees
        //                         select new
        //                         {
        //                             label = "> Rp.10.000.000",
        //                             value = myContext.Employees.Where(a => a.Salary > 10000000).Count()
        //                         }).First();
        //           List<Object> result = new List<Object>();
        //           result.Add(label3);
        //           result.Add(label1);
        //           result.Add(label2);
        //           result.Add(label4);
        //           return result.ToArray();
        //       }
        //       public IEnumerable GetDegree()
        //       {
        //           var result = from degre in myContext.Educations
        //                        group degre by degre.Degree into x
        //                        select new
        //                        {
        //                            degree = x.Key,
        //                            value = x.Count()
        //                        };
        //           return result;
        //       }
    }
}
