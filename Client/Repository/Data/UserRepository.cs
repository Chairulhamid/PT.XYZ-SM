﻿using API.Models;
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
        public async Task<List<User>> GetAllVendor()
        {
            List<User> entities = new List<User>();

            using (var response = await httpClient.GetAsync(request + "GetAllVendor"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var dsfsdf = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                entities = JsonConvert.DeserializeObject<List<User>>(apiResponse);
            }
            return entities;
        }
        //public async Task<List<RegisterVM>> GetNikEmployees(string id)
        //{
        //    List<RegisterVM> entities = new List<RegisterVM>();

        //    using (var response = await httpClient.GetAsync(request + "Profile/" + id))
        //    {
        //        string apiResponse = await response.Content.ReadAsStringAsync();
        //        entities = JsonConvert.DeserializeObject<List<RegisterVM>>(apiResponse);

        //    }
        //    return entities;
        //}
        public HttpStatusCode Post(UserVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request + "Register", content).Result;
            return result.StatusCode;
        }
        //Newed
        //public HttpStatusCode PostRegistration(User entity)
        //{
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        //    var result = httpClient.PostAsync(address.link + request + "RegisterNew", content).Result;
        //    return result.StatusCode;
        //}
        //public async Task<JWTokenVM> Auth(LoginVM loginVM)
        //{
        //    JWTokenVM token = null;
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
        //    var result = await httpClient.PostAsync(request + "Login", content);
        //    string apiResponse = await result.Content.ReadAsStringAsync();
        //    token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);

        //    return token;
        //}

    
    }
}
