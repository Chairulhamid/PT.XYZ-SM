using API.Models;
using API.ViewModel;
using Client.Base.Urls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class ProjectRepository : GeneralRepository<Project, string>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;

        public ProjectRepository(Address address, string request = "Projects/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
        public async Task<List<Project>> GetAllProject(string email)
        {
            List<Project> entities = new List<Project>();

            using (var response = await httpClient.GetAsync(request + "GetAllProject/" + email))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Project>>(apiResponse);
            }
            return entities;
        }
        public HttpStatusCode Postw(Project entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request + "Postw", content).Result;
            return result.StatusCode;
        }
        //public HttpStatusCode Post(Project entity)
        //{
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        //    var result = httpClient.PostAsync(address.link + request + "Post", content).Result;
        //    return result.StatusCode;
        //}
    }
}
