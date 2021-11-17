using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AmisMessengerApi.Models.Users
{
    public class AuthenticateModel
        // xác định các tham số cho yêu cầu POST đến database Usersystem 
    {
        [Required] // yêu cầu bắt buộc phải có để xác thực
        public string UserEmail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
