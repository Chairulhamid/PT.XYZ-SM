using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.Repository.Interface;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController<User, UserRepository, string>
    {
        private readonly UserRepository user;
        public IConfiguration _configuration;
        private readonly MyContext myContext;

        public UsersController(UserRepository userRepository, IConfiguration configuration, MyContext myContext) : base(userRepository)
        {
            this.user = userRepository;
            this._configuration = configuration;
            this.myContext = myContext;
        }

        //[Authorize(Roles = "Director")]
        //[Route("SignManager")]
        //[HttpPost]
        //public ActionResult SignManager(SignManagerVM signManagerVM)
        //{
        //    var result = employee.SignManager(signManagerVM);
        //    if (result == 2)
        //    {
        //        return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan: Email yang Anda masukkan belum sudah terdaftar!" });
        //    }
        //    return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data Berhasil Dimasukan!!" });
        //}

        [Route("GetAllVendor")]
        [HttpGet]
        public ActionResult<User> GetAllVendor()
        {

            var getEmpRole = user.GetAllVendor();
            if (getEmpRole != null)
            {
                return Ok(getEmpRole);
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = getEmpRole, message = "Tidak ada data di sini" });
            }
        }

        [Route("Register")]
        [HttpPost]
        public ActionResult Register(UserVM registerVM)
        {
            var result = user.RegisterNew(registerVM);
            if (result == 2)
            {   
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan: ID yang Anda masukkan sudah terdaftar!" });
            }
            else if (result == 3)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan: No Hp yang Anda masukkan sudah terdaftar!" });
            }
            else if (result == 4)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan: Email yang Anda masukkan sudah terdaftar!" });
            }
            else if (result == 5)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan: ID Universiy Belum Terdaftar!" });
            }
            return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil dimasukkan" });
        }
        [Route("Login")]
        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            var result = user.Login(loginVM);
            if (result == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan: Email yang Anda masukkan belum sudah terdaftar!" });
            }
            else if (result == 3)
            {
                var getDataUser = (from e in myContext.Users
                                   join a in myContext.UsersAccounts on e.Email equals a.Email
                                   join ar in myContext.UserRoles on a.Email equals ar.Email
                                   join r in myContext.Roles on ar.Role_Id equals r.Role_Id
                                   orderby e.Email
                                   select new
                                   {
                                       CompanyName = e.CompanyName,
                                       Email = e.Email,
                                       Role = r.Role_Name
                                   }).Where(e => e.Email == loginVM.Email).ToList();
                List<string> listRole = new List<string>();
                foreach (var item in getDataUser)
                {
                    listRole.Add(item.Role);
                }
                var data = new LoginVM()
                {
                    Email = loginVM.Email,
                    Role = listRole.ToArray()
                };
                var claims = new List<Claim>
                 {
                new Claim("email", data.Email),
                };
                foreach (var item in data.Role)
                {
                    claims.Add(new Claim("roles", item.ToString()));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn
                    );
                var idToken = new JwtSecurityTokenHandler().WriteToken(token);
                claims.Add(new Claim("TokenSecurity", idToken.ToString()));
                return Ok(new JWTokenVM
                {
                    /* status = HttpStatusCode.OK, */
                    Token = idToken,
                    Messages = "Login Berhasil!!"
                });
            }
            return Ok(new { status = HttpStatusCode.OK, result = result, message = "Login Gagal, Password yang anda masukan Salah" });
        }


        ///*[Authorize(Roles = "Director, Manager")]*/
        //[Route("Profile")]
        //[HttpGet]
        //public ActionResult<RegisterVM> GetProfile()
        //{

        //    var getProfile = employee.GetProfile();
        //    if (getProfile.ToList().Count > 0)
        //    {
        //        return Ok(getProfile);
        //    }
        //    else
        //    {
        //        return NotFound(new { status = HttpStatusCode.NotFound, result = getProfile, message = "Tidak ada data di sini" });
        //    }
        //}
        //[Route("Profile/{NIK}")]
        //public   ActionResult<RegisterVM> GetProfileNik(string NIK)
        //{
        //    var ada = employee.GetProfileNik(NIK);
        //    if (ada != null)
        //    {
        //        return Ok(ada);
        //    }
        //    return NotFound(new { status = HttpStatusCode.NotFound, result = ada, message = $"Data dengan NIK {NIK} tidak ditemukan" });
        //}
        //[Route("Gender")]
        //[HttpGet]
        //public ActionResult<RegisterVM> GetGender()
        //{

        //    var getGender = employee.GetGender();
        //    if (getGender != null)
        //    {
        //        return Ok(new { status = HttpStatusCode.OK, result = getGender, message = "Data berhasil ditampilkan" });
        //    }
        //    else
        //    {
        //        return NotFound(new { status = HttpStatusCode.NotFound, result = getGender, message = "Tidak ada data di sini" });
        //    }
        //} 
        //[Route("CountRole")]
        //[HttpGet]
        //public ActionResult<RegisterVM> GetEmpRole()
        //{

        //    var getEmpRole = employee.GetEmpRole();
        //    if (getEmpRole != null)
        //    {
        //        return Ok(new { status = HttpStatusCode.OK, result = getEmpRole, message = "Data berhasil ditampilkan" });
        //    }
        //    else
        //    {
        //        return NotFound(new { status = HttpStatusCode.NotFound, result = getEmpRole, message = "Tidak ada data di sini" });
        //    }
        //} 
        //[Route("CountSalary2")]
        //[HttpGet]
        //public ActionResult<RegisterVM> GetSalary2()
        //{

        //    var getSalary = employee.GetSalary2();
        //    if (getSalary != null)
        //    {
        //        return Ok(new { status = HttpStatusCode.OK, result = getSalary, message = "Data berhasil ditampilkan" });
        //    }
        //    else
        //    {
        //        return NotFound(new { status = HttpStatusCode.NotFound, result = getSalary, message = "Tidak ada data di sini" });
        //    }
        //}  
        //[Route("CountDegree")]
        //[HttpGet]
        //public ActionResult<RegisterVM> GetDegree()
        //{

        //    var getDegree = employee.GetDegree();
        //    if (getDegree != null)
        //    {
        //        return Ok(new { status = HttpStatusCode.OK, result = getDegree, message = "Data berhasil ditampilkan" });
        //    }
        //    else
        //    {
        //        return NotFound(new { status = HttpStatusCode.NotFound, result = getDegree, message = "Tidak ada data di sini" });
        //    }
        //}

    }

}
