using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmisMessengerApi.Models.Users
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string UserAvatar { get; set; }
        public string Password { get; set; }
    }
}
