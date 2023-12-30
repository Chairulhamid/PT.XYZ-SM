
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
/*using System.Text.Json.Serialization;*/
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_M_UsersAccount")]
    public class UsersAccount
    {
        
        [Key]
        public string Email { get; set; }

        public string Password { get; set; }
        [JsonIgnore]
        public virtual User User{ get; set; }
        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles{ get; set; }
    }
}
