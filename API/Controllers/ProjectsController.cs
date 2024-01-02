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
    public class ProjectsController : BaseController<Project, ProjectRepository, string>
    {
        private readonly ProjectRepository project;
        public IConfiguration _configuration;
        private readonly MyContext myContext;

        public ProjectsController(ProjectRepository projectRepository, IConfiguration configuration, MyContext myContext) : base(projectRepository)
        {
            this.project = projectRepository;
            this._configuration = configuration;
            this.myContext = myContext;
        }
        [Route("GetAllProject/{email}")]
        [HttpGet]
        public ActionResult<User> GetAllProject(string email)
        {

            var gete = project.GetAllProject(email);
            if (gete != null)
            {
                return Ok(gete);
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = gete, message = "Tidak ada data di sini" });
            }
        }
        [Route("Postw")]
        [HttpPost]
        public ActionResult Postw(Project VM)
        {
            var result = project.Postw(VM);
            if (result == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan!" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil dimasukkan" });
            }

        }
    }
}
