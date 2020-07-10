using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AmisMessengerApi.Models.Users
{
    public class AuthenticateModel
        // xác định các tham số cho yêu cầu POST đến database Users 
    {
        [Required] // yêu cầu bắt buộc phải có để xác thực
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
