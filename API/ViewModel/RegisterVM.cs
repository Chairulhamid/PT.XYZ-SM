using API.Models;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;/*
using System.Text.Json.Serialization;*/
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModel
{
    public class RegisterVM
    {
        [Required]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Degree { get; set; }
        public string Gpa { get; set; }
        public int UniversityId { get; set; }
        public int Role_Id { get; set; }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
