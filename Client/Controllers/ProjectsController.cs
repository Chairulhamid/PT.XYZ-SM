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
    
    public class ProjectsController : BaseController<Project, ProjectRepository , string>
    {
        private readonly ProjectRepository repository;
        public ProjectsController(ProjectRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public async Task<JsonResult> GetAllProject(string email)
        {
            var result = await repository.GetAllProject(email);
            return Json(result);
        }
        public JsonResult Postw(Project entity)
        {
            var result = repository.Postw(entity);
            return Json(result);
        }
    }
}
 