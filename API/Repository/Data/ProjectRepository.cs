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
    public class ProjectRepository : GeneralRepository<MyContext, Project , string>
    {
        private readonly MyContext myContext;
        
        public ProjectRepository(MyContext myContext) : base(myContext)
         {
            this.myContext = myContext;
         }
        public IEnumerable<Project> GetAllProject(string email)
        {
            var result = (from u in myContext.Users
                            join ur in myContext.Projects on u.Email equals ur.Email
                            where ur.Email == email
                            select new Project
                            {
                                Name = ur.Name,
                                Description = ur.Description,
                            }).ToList();
            return result;
        }
        public int Postw(Project VM)
        {
            Project pp = new Project();
           
            pp.Name = VM.Name;
            pp.Description = VM.Description;
            pp.Email = VM.Email;
         
            myContext.Projects.Add(pp);
            myContext.SaveChanges();
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
