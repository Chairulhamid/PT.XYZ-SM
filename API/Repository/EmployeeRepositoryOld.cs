using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
   /* public class EmployeeRepositoryOld : IEmployeeRepository
    {
        public IEnumerable<Employee> Get() // Menampung semua data 
        {
            return context.Employees.ToList();
        }

        public Employee Get(string NIK)
        {
            return context.Employees.Find(NIK); //menggunakan Find untuk mencari Primary Key dari NIK
        }

        public int Insert(Employee employee)
        {
            var checkNik = context.Employees.Find(employee.NIK);
            var checkEmail = context.Employees.Where(x => x.Email == employee.Email).FirstOrDefault();
            var checkPhone = context.Employees.Where(x => x.Phone == employee.Phone).FirstOrDefault();
            if (checkNik != null)
              {
                  return 2;
              }
              else if (checkPhone != null)
              {
                  return 3;
              }
              else if (checkEmail != null)
              {
                  return 4;
              }
              context.Employees.Add(employee);
              var result = context.SaveChanges();
              return result;
        }

        public int Update(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }

        public int Delete(string NIK)
        {
            var entity = context.Employees.Find(NIK);
            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }

        private readonly MyContext context;

    *//*    public EmployeeRepository(MyContext context)
        {
            this.context = context;
        }*//*
    }*/
}
