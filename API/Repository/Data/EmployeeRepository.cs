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
    public class EmployeeRepository : GeneralRepository<MyContext, Employee , string>
    {
        private readonly MyContext myContext;
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public EmployeeRepository(MyContext myContext) : base(myContext)
         {
            this.myContext = myContext;
         }

        public int Login(LoginVM loginVM)
        {
             Employee employee = new Employee();
            Account account = new Account();
            var checkEmail = myContext.Employees.Where(x => x.Email == loginVM.Email).FirstOrDefault();
            if (checkEmail == null)
            {
                return 2;
            }
            var checkNik = checkEmail.NIK;

            var checkPass = myContext.Accounts.Find(checkEmail.NIK);

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
        public int SignManager(SignManagerVM signManagerVM)
        {
            Employee employee = new Employee();
         
            var checkdata = myContext.Employees.Find(signManagerVM.NIK);
            employee.NIK = signManagerVM.NIK;
            if (checkdata == null)
            {
                return 2;
            }
            AccountRole accountRole = new AccountRole();
            accountRole.NIK = signManagerVM.NIK;
            accountRole.Role_Id = 5;
            myContext.AccountRoles.Add(accountRole);
            var result = myContext.SaveChanges();
            return result;
        }
  
      
    
        public IEnumerable GetGender()
        {
            var result = from emp in myContext.Employees
                         group emp by emp.Gender into x
                         select new 
                         {
                             gender = x.Key,
                             value = x.Count()
                         };
            return result;
        }
        public IEnumerable GetEmpRole()
        {
            var result = from role in myContext.AccountRoles
                         group role by role.Role_Id into x
                         select new
                         {
                             role_Id = x.Key,
                             value = x.Count()
                         };
            return result;
        }
        public object[] GetSalary2()
        {
            var label1 = (from emp in myContext.Employees
                          select new
                          {
                              label = "Rp.2.000.000 - Rp.5.000.000",
                              value = myContext.Employees.Where(a => a.Salary <= 5000000 && a.Salary >= 2000000).Count()
                          }).First();
            var label2 = (from emp in myContext.Employees
                          select new
                          {
                              label = "Rp.5.000.001 - Rp.10.000.000",
                              value = myContext.Employees.Where(a => a.Salary <= 10000000 && a.Salary >= 5000001).Count()
                          }).First();
            var label3 = (from emp in myContext.Employees
                          select new
                          {
                              label = "< Rp.2.000.000",
                              value = myContext.Employees.Where(a => a.Salary < 2000000).Count()
                          }).First();
            var label4 = (from emp in myContext.Employees
                          select new
                          {
                              label = "> Rp.10.000.000",
                              value = myContext.Employees.Where(a => a.Salary > 10000000).Count()
                          }).First();
            List<Object> result = new List<Object>();
            result.Add(label3);
            result.Add(label1);
            result.Add(label2);
            result.Add(label4);
            return result.ToArray();
        }
       
    }
}
