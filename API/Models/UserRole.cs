using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_M_UserRole")]
    public class UserRole
    {
       
        public string Email { get; set; }
        public int Role_Id { get; set; }
        [JsonIgnore]
        public virtual UsersAccount UsersAccount { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; }


        
    }
}
