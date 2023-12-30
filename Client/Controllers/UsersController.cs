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
    
    public class UsersController : BaseController<User, UserRepository , string>
    {
        private readonly UserRepository repository;
        public UsersController(UserRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public async Task<JsonResult> GetAllVendor()
        {
            var result = await repository.GetAllVendor();
            return Json(result);
        }
        //public async Task<JsonResult> GetNikEmployees(string id)
        //{
        //    var result = await repository.GetNikEmployees(id);
        //    return Json(result);

        //}
        public JsonResult Register(UserVM entity)
        {
            var result = repository.Post(entity);
            return Json(result);
        }

    }
}
 