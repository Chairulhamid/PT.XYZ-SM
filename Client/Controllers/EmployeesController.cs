using API.Models;
using API.ViewModel;
using Client.Base.Controllers;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    
    public class EmployeesController : BaseController<Employee, EmployeeRepository , string>
    {
        private readonly EmployeeRepository repository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public async Task<JsonResult> GetEmployees()
        {
            var result = await repository.GetEmployees();
            return Json(result);
        }
        public async Task<JsonResult> GetNikEmployees(string id)
        {
            var result = await repository.GetNikEmployees(id);
            return Json(result);

        }
        public JsonResult Register(RegisterVM entity)
        {
            var result = repository.Post(entity);
            return Json(result);
        }
        //Newed
        public JsonResult RegisterNew(User entity)
        {
            var result = repository.PostRegistration(entity);
            return Json(result);
        }
    }
}
 