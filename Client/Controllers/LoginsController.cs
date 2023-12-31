using API.ViewModel;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LoginsController : Controller
    {
        private readonly LoginRepository repository;
        /*private readonly IJWTHandler jwtHandler;*/

        public LoginsController(LoginRepository repository)
        {
            this.repository = repository;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Auth(LoginVM loginVM)
        {
            var jwtToken = await repository.Auth(loginVM);
            var token = jwtToken.Token;

            if (token == null)
            {
                return RedirectToAction("LogiinPage", "Home");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            HttpContext.Session.SetString("JWToken", token);
            HttpContext.Session.SetString("SessionEmail", jsonToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value);
            HttpContext.Session.SetString("SessionRole", jsonToken.Claims.FirstOrDefault(c => c.Type == "roles")?.Value);
            /*HttpContext.Session.SetString("Name", jwtHandler.GetName(token));*/
            HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage", "Home");
        }
   

    }
}
