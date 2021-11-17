using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmisMessengerApi.Models.Users
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        public int Role { get; set; }
        //[Required]
        //public string PhoneNumber { get; set; }
        
        //public string UserAvatar { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
