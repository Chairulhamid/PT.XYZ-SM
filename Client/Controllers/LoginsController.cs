using API.ViewModel;
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
    public class LoginsController : Controller
    {
        private readonly LoginRepository repository;
        /*private readonly IJWTHandler jwtHandler;*/

        public LoginsController(LoginRepository repository/*, IJWTHandler jwtHandler*/)
        {
            this.repository = repository;
           /* this.jwtHandler = jwtHandler;*/

        }
        public IActionResult Index()
        {
            return View();
        }
        /* [ValidateAntiForgeryToken]*/
        /*    [HttpPost("Auth/")]*/
        public async Task<IActionResult> Auth(LoginVM loginVM)
        {
            var jwtToken = await repository.Auth(loginVM);
            var token = jwtToken.Token;

            if (token == null)
            {
                return RedirectToAction("LogiinPage", "Home");
            }
            HttpContext.Session.SetString("JWToken", token);
            /*HttpContext.Session.SetString("Name", jwtHandler.GetName(token));*/
            HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");

            return RedirectToAction("Index", "Home");
        }
        /* [HttpGet("Logout/")]*/
        [Authorize]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage", "Home");
        }
   

    }
}
