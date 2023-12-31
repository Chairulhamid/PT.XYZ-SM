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
    public class UserRepository : GeneralRepository<User, string>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;

        public UserRepository(Address address, string request = "Users/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
        public async Task<List<User>> GetAllVendor(int id)
        {
            List<User> entities = new List<User>();

            using (var response = await httpClient.GetAsync(request + "GetAllVendor/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var dsfsdf = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                entities = JsonConvert.DeserializeObject<List<User>>(apiResponse);
            }
            return entities;
        }
        public async Task<List<User>> GetDataVendor(string email)
        {
            List<User> entities = new List<User>();

            using (var response = await httpClient.GetAsync(request + "GetDataVendor/" + email))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<User>>(apiResponse);

            }
            return entities;
        }
        public HttpStatusCode Post(UserVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request + "Register", content).Result;
            return result.StatusCode;
        }
    }
}
