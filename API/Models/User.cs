using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_M_User")]

    public class User
    {
        public string CompanyName { get; set; }
        [Key]
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public string Photo { get; set; }
        public string TypeCompany { get; set; }
        public string Corporatebusiness { get; set; }
        public string Status { get; set; }
        [JsonIgnore]
        public virtual UsersAccount UsersAccount { get; set; }


    }

}
